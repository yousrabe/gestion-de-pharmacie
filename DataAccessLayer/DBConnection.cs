using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessLayer
{

    internal static class DBConnection
    {
        private static readonly string connectionString = @"Data Source=localhost;Initial Catalog=pharmacydb_am;Integrated Security=True";

        // this is the only place in your app that
        // the database connection string should be
        // found and the only code that should create
        // database connections

        public static SqlConnection GetDbConnection()
        {
            // a connection needs a connection string
            var conn = new SqlConnection(connectionString);
            return conn;
        }
    }
}
