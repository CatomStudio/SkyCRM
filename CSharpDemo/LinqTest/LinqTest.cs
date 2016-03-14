using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CSharpDemo.LinqTest
{
    // 测试实体类
    public class Person
    {
        public long Id { get; set; }

        public String Name { get; set; }

        public int Age { get; set; }

        public string Read()
        {
            return "Id:" + Id + "姓名:" + Name + "年龄:" + Age;
        }
    }

    public class Student
    {
        public long Id { set; get; }

        public string Name { get; set; }

        public int Age { get; set; }
    }


    /// <summary>
    ///  
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class LinqContext<T>
    {
        public Stopwatch Watch;

        public LinqContext()
        {
            this.Watch = new Stopwatch();
        }

        public static ConcurrentDictionary<int, Person> InitData(int count)
        {
            var dic = new ConcurrentDictionary<int, Person>();
            Parallel.For(0, count,
                (i) => dic.TryAdd(i, new Person
                {
                    Id = i,
                    Name = "Person_" + i,
                    Age = (i * i + i % 3) % 26
                })
            );
            return dic;
        }

        public delegate void RunMethod(IEnumerable<T> data);

        public delegate void Expression();

        /// <summary>
        ///  获取方法执行时间。
        ///  Usage:
        ///         context.RunningTime(method, data);
        /// </summary>
        /// <param name="invoke">执行的方法名</param>
        /// <param name="data">方法的参数</param>
        /// <returns></returns>
        public long RunningTime(RunMethod invoke, IEnumerable<T> data)
        {
            long runningTime = 0;
            if (this.Watch == null)
            {
                this.Watch = new Stopwatch();
            }
            Watch.Start();
            invoke(data);
            Watch.Stop();
            runningTime = Watch.ElapsedMilliseconds;
            Watch.Reset();
            return runningTime;
        }

        /// <summary>
        ///  表达式执行时间。
        /// </summary>
        /// <param name="expression">Lambda表达式</param>
        /// <returns></returns>
        public long RunningTime(Expression expression)
        {
            long runningTime = 0;
            if (this.Watch == null)
            {
                this.Watch = new Stopwatch();
            }
            Watch.Start();
            expression();
            Watch.Stop();
            runningTime = Watch.ElapsedMilliseconds;
            Watch.Reset();
            return runningTime;
        }

    }


    class Entry
    {
        public static void Run(IEnumerable<Person> data)
        {
            // 取数据作为新类型对象：查找 Id<10 0的 person 的姓名和年龄
            var result = (from p in data
                          select new { p.Name, p.Age }).ToList();
        }

        public static void Main1()
        {
            LinqContext<Person> c = new LinqContext<Person>();

            // 关联查询
            var stus = new List<Student>();
            for (var i = 0; i < 10000; i++)
            {
                stus.Add(new Student { Id = i, Name = "stu_" + i, Age = (i * i + i % 3) % 17 });
            }

            var data = LinqContext<Person>.InitData(10000).Values;

            // distinct
            var queryAgeType = (from p in data select p.Age).Distinct().Select(p => p > 0);

            // 执行时间
            var sec1 = c.RunningTime(Run, data);
            Console.WriteLine("方法执行时间：" + sec1);

            var sec2 = c.RunningTime(() =>
            {
                var query1 = (from p in data select p).ToList();
            });
            Console.WriteLine("lambda 执行时间：" + sec2);

            // 自然连接查询
            var relatedQuery1 = from p in data
                                from s in stus
                                where p.Id == s.Id && p.Id < 1000
                                select new { p.Id, s.Name, s.Age };

            // 关联查询：关联查询效率比自然查询高出近千倍！
            var relatedQuery2 = from p in data
                                join s in stus on p.Id equals s.Id into stu
                                from s in stu
                                where p.Id < 1000
                                orderby p.Age
                                select new { p.Id, s.Name, s.Age };

            var sec3 = c.RunningTime(() => { var a = relatedQuery1.Count(); });
            Console.WriteLine("自然连接查询时间：" + sec3);

            var sec4 = c.RunningTime(() => { var a = relatedQuery2.Count(); });
            Console.WriteLine("关联查询时间：" + sec4);

        }
    }
}
