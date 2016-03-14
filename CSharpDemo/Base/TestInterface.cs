using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDemo.Base
{
    public interface IA
    {
        void Todo1();
    }

    public class A1 : IA
    {
        public A1()
        {
            Console.WriteLine("A1 init.");
        }

        public void Todo1()
        {
            Console.WriteLine("This is the implements method in A1.");
        }

        public void Todo2()
        {
            Console.WriteLine("This is the common method in A1.");
        }
    }

    public class B1 : A1
    {
        public B1()
        {
            Console.WriteLine("B1 init.");
        }

        public void Todo3() { }

    }



    class Entry1
    {
        public static void Main1()
        {
            A1 a = new B1();
            a.Todo1();
        }
    }
}
