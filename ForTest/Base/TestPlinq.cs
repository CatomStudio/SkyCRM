using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ForTest.Base
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


    public class PLinq
    {
        public static void DoTest()
        {
            // 统计时间
            Stopwatch watch = new Stopwatch();

            // 取数据
            watch.Start();
            var data = InitData(15000000); // 20W
            watch.Stop();

            watch.Start();
            var queryCommon = from p in data.Values where p.Age > 24 select p;
            watch.Stop();
            Console.WriteLine("串行执行时间：{0} 个，{1} 秒", queryCommon.Count(), watch.ElapsedMilliseconds);

            watch.Restart();
            var queryParallel = from p in data.Values.AsParallel() where p.Age > 24 select p;
            watch.Stop();
            Console.WriteLine("并行执行时间：{0} 个，{1} 秒", queryParallel.Count(), watch.ElapsedMilliseconds);

        }

        public static ConcurrentDictionary<int, Person> InitData(int count)
        {
            var dic = new ConcurrentDictionary<int, Person>();
            Parallel.For(0, count,
                (i) => dic.TryAdd(i, new Person
                {
                    Id = i,
                    Name = "Person_" + i,
                    Age = (i * i + 43) % 26
                })
            );
            return dic;
        }


    }

}
