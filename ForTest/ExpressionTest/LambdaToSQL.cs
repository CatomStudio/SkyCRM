using System;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.ProviderBase;
using System.Text;
using System.Linq.Expressions;

namespace ForTest.ExpressionTest
{
    internal static class Program
    {
        public static void Main()
        {
            var uid = new User();
            uid.Where(userId => (userId.Id == "8" && userId.LoginCount > 5)
                || userId.Pws != null
                || userId.Id.Like("%aa")
                && userId.LoginCount.In(new int?[] { 4, 6, 8, 9 })
                && userId.Id.NotIn(new[] { "a", "b", "c", "d" })
            );
        }
    }

    internal class BaseEntity
    {
        internal string WhereStr;
    }

    internal class User : BaseEntity
    {
        public string Id { get; set; }

        public string Pws { get; set; }

        public int? LoginCount { get; set; }
    }

    /// <summary>
    ///  SQL 条件关键字对应的方法。
    /// </summary>
    internal static class ExpressionParseFactory
    {
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
        public static void Where<T>(this T entity, Expression<Func<T, bool>> func) where T : BaseEntity
        {
            if (!(func.Body is BinaryExpression))
                return;
            var exp = func.Body;
            entity.WhereStr = ExpressionParser(exp);
        }

        public static void GroupBy<T>(this T entity) { }

        public static void OrderBy<T>(this T entity) { }

        public static void Limit<T>(this IDbConnection connection)
        {
            IDbCommand cmd = connection.CreateCommand();
            cmd.CommandText = "";
            cmd.ExecuteNonQuery();
        }
        #endregion

        #region Factory
        static string BinarExpressionProvider(BinaryExpression exp)
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
        ///  表达式解离器。<br/>
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
                        return null;
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
            return null;
        }

        public static string ExpressionTypeCast(ExpressionType type)
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


}

