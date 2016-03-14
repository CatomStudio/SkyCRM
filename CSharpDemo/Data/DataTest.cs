using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Collections;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.SqlServer.Server;

namespace CSharpDemo.Data
{
    public class DataTest
    {
        public DataTest()
        {
            string connString = ConfigurationManager.ConnectionStrings["testConn"].ConnectionString;
            IDbConnection dbc = new SqlConnection();
        }
    }

    class Entry
    {
        public static void Main1()
        {
            var arr = new[] { 1, 23, 4 };
            var query =  from a in arr select a;
        }

    }
}
