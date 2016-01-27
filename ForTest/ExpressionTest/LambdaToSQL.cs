using System;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.ProviderBase;
using System.Text;
using System.Linq.Expressions;
using System.Linq;
using System.Collections.Generic;
using System.Configuration;
using MySql.Data;
using MySql.Data.Common;
using MySql.Data.MySqlClient;
using System.IO;
using System.Net;
using System.Net.WebSockets;

namespace ForTest.ExpressionTest
{


    /// <summary>
    ///  实体类。
    /// </summary>
    public class Person
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string Address { get; set; }
    }


    /// <summary>
    ///  数据库连接处理类。
    /// </summary>
    public class DBUtil
    {
        public IDbConnection connection;
        public string connString = string.Empty;

        public DBUtil()
        {
            var connString = ConfigurationManager.ConnectionStrings["mysqlConn"].ConnectionString;
            connection = new MySqlConnection(connString);
        }

        public DBUtil(string connString)
        {
            connection = new MySqlConnection(connString);
        }

        public static IDbConnection GetConn()
        {
            var connString = ConfigurationManager.ConnectionStrings["testConn"].ConnectionString;
            var conn = new MySqlConnection(connString);
            conn.Open();
            return conn;
        }


        public bool Open()
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }


    }


    /// <summary>
    ///  静态类，功能目标类似 Dapper，提供 Ado.Net + MySql 的数据库操作细节。
    ///  目标功能：
    ///     提供CRUD的底层实现，如：
    ///         OrmContext.Query<T>(queryString);
    ///         OrmContext.Get<T>(expression);
    ///         Expression<Func<T, bool>>
    /// </summary>
    public static class OrmContext
    {
        public static IEnumerable<T> Get<T>(this IDbConnection connection, Expression<Func<T, bool>> expression)
        {
            IDbCommand cmd = null;
            IDataReader reader = null;
            bool currClosed = connection.State == ConnectionState.Closed;

            try
            {
                // 1. 通过 Expression 解析工厂，解析出 Lambda 表达式对应的 SQL 语句；
                var queryString = ExpressionParseFactory.ExpressionParser(expression.Body);

                // 2. 创建 Ado.Net 处理数据库的相关对象；
                if (currClosed)
                    connection.Open();
                cmd = connection.CreateCommand();
                cmd.CommandText = queryString;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    // TODO 循环返回 reader 中读取的每行数据

                    break;
                    //yield return default(T);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Dispose();
                }
                if (cmd != null)
                {
                    cmd.Dispose();
                }
                if (currClosed)
                {
                    connection.Close();
                }
            }

            var arr = new T[] { default(T), default(T) };
            return arr;
        }


    }

    /// <summary>
    ///  静态类：
    ///  查询条件表达式解析工厂，提供完整的表达式解析方法。
    /// </summary>
    internal static class ExpressionParseFactory
    {

        #region ** 注：暂时不用

        #region SQL Where 内关键字
        public static bool In<T>(this T obj, T[] array)
        {
            // TODO
            return true;
        }

        public static bool NotIn<T>(this T obj, T[] array)
        {
            // TODO
            return true;
        }

        public static bool Like(this string str, string likeStr)
        {
            // TODO
            return true;
        }

        public static bool NotLike(this string str, string likeStr)
        {
            // TODO
            return true;
        }
        #endregion

        #region SQL SentenceUtil
        //public static void Where<T>(this T entity, Expression<Func<T, bool>> func) // where T : BaseEntity
        //{
        //    if (!(func.Body is BinaryExpression))
        //        return;
        //    var exp = func.Body;
        //    entity.WhereStr = ExpressionParser(exp);
        //}

        public static void GroupBy<T>(this T entity) { }

        public static void OrderBy<T>(this T entity) { }

        public static void Limit<T>(this IDbConnection connection)
        {
            IDbCommand cmd = connection.CreateCommand();
            cmd.CommandText = "";
            cmd.ExecuteNonQuery();
        }
        #endregion

        #endregion


        #region Factory

        /// <summary>
        ///  自递归表达式解离器。<br/>
        ///  将非叶子表达式解离成叶子表达式，然后可以取到可以取到Lambda表达式的节点变量名和变量值，用以拼接成SQL语句。<br/>
        ///  
        ///  * 注： 
        ///  支持的 Expression 类型有：
        ///  >>> 非叶子表达式：
        ///     1. BinaryExpression（二元表达式，一般的根表达式类型）
        ///     2. UnaryExpression（一元表达式）
        ///     3. NewArrayExpression（数组表达式）
        ///     4. MethodCallExpression（方法调用表达式）
        ///  >>> 叶子表达式：
        ///     1. MemberExpression/PropertyExpression（成员变量/属性表达式）
        ///     2. ConstantExpression（常量表达式）
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static string ExpressionParser(Expression exp)
        {
            #region 非叶子表达式
            if (exp is BinaryExpression)
            {
                var be = (BinaryExpression)exp;
                return BinarExpressionProvider(be);
            }
            if (exp is UnaryExpression)
            {
                var ue = (UnaryExpression)exp;
                return ExpressionParser(ue.Operand);
            }
            if (exp is NewArrayExpression)
            {
                var ae = (NewArrayExpression)exp;
                var tmpstr = new StringBuilder();
                foreach (Expression ex in ae.Expressions)
                {
                    tmpstr.Append(ExpressionParser(ex));
                    tmpstr.Append(",");
                }
                return tmpstr.ToString(0, tmpstr.Length - 1);
            }
            if (exp is MethodCallExpression)
            {
                var mce = (MethodCallExpression)exp;
                switch (mce.Method.Name)
                {
                    case "Like":
                        return string.Format("({0} like {1})", ExpressionParser(mce.Arguments[0]), ExpressionParser(mce.Arguments[1]));
                    case "NotLike":
                        return string.Format("({0} Not like {1})", ExpressionParser(mce.Arguments[0]), ExpressionParser(mce.Arguments[1]));
                    case "In":
                        return string.Format("{0} In {1}", ExpressionParser(mce.Arguments[0]), ExpressionParser(mce.Arguments[1]));
                    case "NotIn":
                        return string.Format("{0} Not In {1}", ExpressionParser(mce.Arguments[0]), ExpressionParser(mce.Arguments[1]));
                    default:
                        return string.Empty;
                }
            }
            #endregion

            #region 叶子表达式
            if (exp is MemberExpression)
            {
                var me = (MemberExpression)exp;
                return me.Member.Name;
            }
            if (exp is ConstantExpression)
            {
                var ce = (ConstantExpression)exp;
                if (ce.Value == null)
                    return "null";
                if (ce.Value is ValueType)
                    return ce.Value.ToString();
                if (ce.Value is string || ce.Value is DateTime || ce.Value is char)
                    return string.Format("'{0}'", ce.Value);
            }
            #endregion


            if (exp is Expression)
            {

            }

            // 未考虑到的表达式 或者 格式不正确的表达式，以异常的形式提示
            throw new Exception("error：表达式格式不支持！");
        }

        private static string BinarExpressionProvider(BinaryExpression exp)
        {
            var leftExp = exp.Left;
            var rightExp = exp.Right;
            var expOpeType = exp.NodeType;

            var expToSql = "(";

            // 先处理左边
            expToSql += ExpressionParser(leftExp);
            expToSql += ExpressionTypeCast(expOpeType);

            // 再处理右边
            var tempStr = ExpressionParser(rightExp);
            if (tempStr == "null")
            {
                if (expToSql.EndsWith(" ="))
                    expToSql = expToSql.Substring(0, expToSql.Length - 2) + " is null";
                else if (expToSql.EndsWith("<>"))
                    expToSql = expToSql.Substring(0, expToSql.Length - 2) + " is not null";
            }
            else
                expToSql += tempStr;
            return expToSql + ")";
        }

        /// <summary>
        ///  操作符转换
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string ExpressionTypeCast(ExpressionType type)
        {
            switch (type)
            {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    return " AND ";
                case ExpressionType.Equal:
                    return " = ";
                case ExpressionType.GreaterThan:
                    return " >";
                case ExpressionType.GreaterThanOrEqual:
                    return " >= ";
                case ExpressionType.LessThan:
                    return " < ";
                case ExpressionType.LessThanOrEqual:
                    return " <= ";
                case ExpressionType.NotEqual:
                    return " <> ";
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    return " Or ";
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                    return " + ";
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                    return " - ";
                case ExpressionType.Divide:
                    return " / ";
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                    return " * ";
                default:
                    return null;
            }
        }

        #endregion

    }


    internal static class Program
    {

        /// <summary>
        ///  读取网站文件
        /// </summary>
        /// <param name="ossUrl"></param>
        /// <returns></returns>
        public static bool ReadFromUri(string ossUrl)
        {
            try
            {
                // 读取 Oss 文件
                HttpWebRequest request = (HttpWebRequest)System.Net.WebRequest.Create(ossUrl);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                var stream = response.GetResponseStream();

                // 写文件
                FileStream fileStream = File.Create("D://a.mp3");
                byte[] buffer = new byte[1024];
                int numReadByte = 0;
                while ((numReadByte = stream.Read(buffer, 0, 1024)) != 0)
                {
                    fileStream.Write(buffer, 0, numReadByte);
                }
                fileStream.Close();
                stream.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }



        public static void Main()
        {
            // 测试
            var conn = DBUtil.GetConn();
            try
            {
                var data = conn.Get<Person>(p => p.Id == 1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }


}

