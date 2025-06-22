using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDBMS
{
    public static class GLOBAL
    {
        public static string username;
        public static string password;
    }
    public class ConnectDatabase

    {
        SqlConnection conNV = new SqlConnection("Data Source=DESKTOP-0ACF5A9\\QUOCKIET;Initial Catalog=DATABASE_PROJECT_DBMS;User ID=" + GLOBAL.username + ";Password=" +
        GLOBAL.password + ";Encrypt=False;");


        static string strcnn = "Data Source=DESKTOP-0ACF5A9\\QUOCKIET;Initial Catalog=DATABASE_PROJECT_DBMS;Integrated Security=True;";
        SqlConnection conAD = new SqlConnection(strcnn);



        public void openConnectionADmin()
        {
            if (conAD.State == ConnectionState.Closed)
            {
                conAD.Open();
            }
        }   

        public void closeConnectionAdmin()
        {
            if (conAD.State == ConnectionState.Open)
            {
                conAD.Close();
            }
        }
        public SqlConnection getConnection
        {
            get
            {
                return conNV;
            }
        }

        public SqlConnection getConnectionAdmin
        {
            get
            {
                return conAD;
            }
        }

        public void openConnection()
        {
            if (conNV.State == ConnectionState.Closed)
            {
                conNV.Open();
            }
        }

        public void closeConnection()
        {
            if (conNV.State == ConnectionState.Open)
            {
                conNV.Close();
            }
        }

        protected SqlConnection GetConnection()
        {
            return new SqlConnection(strcnn);
        }

        protected SqlConnection GetConnectionAdmin()
        {
            return new SqlConnection(strcnn);
        }

    }
}