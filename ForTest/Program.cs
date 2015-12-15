using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Text;
using System.Linq.Expressions;

namespace ForTest
{
    public class MabLib
    {
        public int x;
        public int y;
        public int Sum(int x, int y)
        {
            return x + y;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var hidden = new { Id = 1, Name = "Catom", Args = new { Id = 2 } };

            List<Person> ps = new List<Person>()
            {
                //new {Id = 1, Name = "实得分", Age = 23},
            };

            Expression<Func<string, string>> exp = (name) => string.Format("my name is :{0}", name);
            var fun = exp.Compile();

            Console.WriteLine(fun("catom"));



            Console.WriteLine(hidden);
        }

    }

}
