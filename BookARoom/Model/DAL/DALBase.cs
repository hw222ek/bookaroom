using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace BookARoom.Model.DAL
{
    public class DALBase
    {
        //Fält som lagrar statisk anslutningssträng
        private static string _connectionString;

        /// <summary>
        /// Statisk konstruktor
        /// </summary>
        static DALBase()
        {
            _connectionString = WebConfigurationManager.ConnectionStrings["BookARoomConnectionString"].ConnectionString;
        }

        /// <summary>
        /// Initierar anslutningsobjekt
        /// </summary>
        /// <returns></returns>
        protected SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}