using System;
using System.Linq;
using System.IO;
using System.Net;

namespace CSharpDemo.IOTest
{
    public static class FileHelper
    {
        /// <summary>
        ///  读取网站文件
        /// </summary>
        /// <param name="ossUrl"></param>
        /// <returns></returns>
        public static bool ReadUri(string ossUrl)
        {
            try
            {
                // 读取 Oss 文件
                HttpWebRequest request = (HttpWebRequest)System.Net.WebRequest.Create(ossUrl);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                var stream = response.GetResponseStream();

                // 写文件
                FileStream fileStream = File.Create("D://a.mp3");
                byte[] buffer = new byte[1024];
                int numReadByte = 0;
                while ((numReadByte = stream.Read(buffer, 0, 1024)) != 0)
                {
                    fileStream.Write(buffer, 0, numReadByte);
                }
                fileStream.Close();
                stream.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        ///  按字节读取文件
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool ReadFileBytes(string url)
        {
            try
            {

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }


    }
}
