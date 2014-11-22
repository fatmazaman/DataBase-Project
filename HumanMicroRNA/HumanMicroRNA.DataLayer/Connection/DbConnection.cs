using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace HumanMicroRNA.DataLayer.Connection
{
    /// <summary>
    /// DbConnection will create the database connection.
    /// </summary>
    public class DbConnection
    {
        private SqlDataAdapter myAdapter;
        private SqlConnection conn;

        /// <summary>
        /// DbConnection constructor is designed to initialise
        /// the connection to the database.
        /// </summary>
        public DbConnection()
        {
            myAdapter = new SqlDataAdapter();
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["HumanMicroRNA"].ConnectionString);
        }
        /// <summary>
        /// openConnection will return an open SqlConnection.
        /// </summary>
        /// <returns></returns>
        public static SqlConnection openConnection()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["HumanMicroRNA"].ConnectionString);

            if (conn.State == ConnectionState.Closed || conn.State ==
                        ConnectionState.Broken)
            {
                conn.Open();
            }
            return conn;
        }
    }
}
