using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForTest
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 1;
            int b = 1;
            var d = DateTime.Now;
            decimal dc = 123.456m;
            float f = 12.67f;
            Console.WriteLine(a.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("en-US")));
            Console.WriteLine((char)('a' +12 >> 2));
            Console.WriteLine(b.GetHashCode());
            Console.WriteLine(a.ToString("D"));
            Console.WriteLine(b.ToString("D"));
            //Console.WriteLine(dc.GetHashCode().ToString("X"));
            //Console.WriteLine(dc.ToString().GetHashCode().ToString("X"));
            //Console.WriteLine(f.ToString());
        }

    }
}
