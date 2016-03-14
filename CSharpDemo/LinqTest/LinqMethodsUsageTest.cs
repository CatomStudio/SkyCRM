using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

/// 注：IQueryable 接口继承自 IEnumerable 接口
/// 
/// IQueryable 和 IEnumerable 的区别：
///     IEnumerable 是集合框架的基本接口，标识【类型为集合】；
///     IQueryable 是 Linq 模块的可查询的基本接口，标识【类型为可查询的集合】；
///     Linq 对集合（Enumerable）的操作方法进行了扩展，这也是 Queryable 的基类；
///     Queryable 本质是 Enumerable 的，是它的拓展。
/// 继承顺序：
///     IEnumerable<T> 继承 IEnumerable、调用 IEnumerator<T> 继承 IEnumerator
///     
namespace CSharpDemo.LinqTest
{
    public class IEnumerableTest
    {
        // 初始化集合数据
        public static IEnumerable<string> DataInit()
        {
            var str = new[] { "AA", "BB", "CC", "DD", "EE", "FF", "GG", "HH" };
            return str.AsEnumerable();
        }
    }

    public class IQueryableTest
    {
        // 初始化可查询集合数据
        public static IQueryable<string> DataInit()
        {
            var str = new[] { "AA", "BB", "CC", "DD", "EE", "FF", "GG", "HH" };
            return str.AsQueryable();
        }

        // test for Where
    }

    public class MethodsContext
    {
        // select 

        // aggregate 

        // all

        // any 

        // average

        // cast

        // concat 

        // contains

        // defaultIfEmpty

        // Distinct

        // elementAt

        // Except

        // Intersect

        // join

        // groupBy

        // groupJoin

        // single

        // Max

        // ofType


    }

    class Program
    {
        public static void Main(string arg)
        {
            var data1 = IEnumerableTest.DataInit();
            var data2 = IQueryableTest.DataInit();
            var list = data1.Select(s => new String((s + ",,,,").ToCharArray()));
            foreach (var i in list)
            {
                Console.WriteLine(i);
            }
        }

    }
}
