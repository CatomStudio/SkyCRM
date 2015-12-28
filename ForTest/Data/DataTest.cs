using System;
using System.Data;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ForTest.Data
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
        public static void Main()
        {

        }

    }
}
