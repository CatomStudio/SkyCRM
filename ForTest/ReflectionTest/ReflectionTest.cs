using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;
using System.Text.RegularExpressions;

namespace ForTest.ReflectionTest
{
    // 工具对象
    class Book
    {
        public long Id;

        public string Name { get; set; }

        public string Author { get; set; }

        public Book()
        {
        }

        public bool SetBookName(string bookName)
        {
            this.Name = bookName;
            return true;
        }

    }

    class ReflectionContext
    {

    }

    // 测试体类
    public class DynObj : DynamicObject
    {
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return base.TryGetMember(binder, out result);
        }
    }

    public class Entry
    {
        public static void Main()
        {
            Console.WriteLine();
        }
    }

}
