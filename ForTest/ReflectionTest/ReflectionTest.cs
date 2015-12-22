﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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

    public static class DataHelper
    {
        //public static IEnumerable<T> Get<T>(this IEnumerable<T> source, Expression<Func<T, bool>> expression)
        //{
        //    return source.Where(expression);
        //}
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
        public static void Main1()
        {
            var ipadCode = "ASDSF;AS;2=3";
            var reg = "[0-9A-Z]+";
            var ms = Regex.Matches(ipadCode, reg);
            var c = ms.Count;
            Console.WriteLine(c);
            Console.WriteLine(Regex.IsMatch(ipadCode, reg));
        }
    }

}
