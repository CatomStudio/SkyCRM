using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml.Resolvers;

namespace ForTest.LinqTest
{
    class LinqContext
    {
        public LinqContext()
        {
            //XmlElement 
        }

        public bool Build(string fileName)
        {
            try
            {
                XDocument root = new XDocument(
                    new XComment("This is a comment."),
                    new XProcessingInstruction("xml-stylesheet", "href='mystyle.css' title='Compact' type='text/css'"),
                    new XElement("BookStore",
                        new XElement("Book",
                            new XElement("Title", "Artifacts of Roman Civilization"),
                            new XElement("Author", "Moreno, Jordao")
                        ),
                       new XElement("Book",
                           new XElement("Title", "Midieval Tools and Implements"),
                           new XElement("Author", "Gazit, Inbar")
                       )
                    ),
                    new XComment("This is another comment.")
                );
                root.Declaration = new XDeclaration("1.0", "utf-8", "true");
                Console.WriteLine(root);
                root.Save(fileName);
                return true;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        ///  读取一个 XML 文件。
        /// </summary>
        /// <param name="fileName">文件绝对路径</param>
        /// <returns></returns>
        public XDocument Read(string fileName)
        {
            if (string.IsNullOrEmpty(fileName) || string.IsNullOrWhiteSpace(fileName))
                return null;
            if (!fileName.ToLower().EndsWith(".xml"))
            {
                fileName = fileName + ".xml";
            }
            try
            {
                if (!File.Exists(fileName))
                {
                    Console.WriteLine(string.Format("文件 {0} 不存在！", fileName));
                    return null;
                }
                return XDocument.Load(fileName);
            }
            catch
            {
                return null;
            }
        }

    }

    class Entry2
    {
        public static void Main1()
        {
            var ctx = new LinqContext();
            var fileName = "D:/Test.xml";
            ctx.Build(fileName);
            var doc = ctx.Read(fileName);
            var query = from e in doc.Elements() select e;
            foreach (var e in query)
            {
                Console.WriteLine(e.Name + " - " + e.Value + "\n");
            }

        }
    }
}
