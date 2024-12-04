using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
namespace Bai_3
{
    public class database
    {
        public static OracleConnection Conn;
        public static string Host;
        public static string Port;
        public static string Sid;
        public static string User;
        public static string Password;
        public static void Set_database(string host, string port, string sid, string user, string pass)
        {
            database.Host = host;
            database.Port = port;
            database.Sid = sid;
            database.User = user;
            database.Password = pass;
        }
        public static bool Connect()
        {
            string consys = "";
            try
            {
                if (User.ToUpper().Equals("SYS"))
                {
                    consys = ";DBA Privilege = SYSDBA";
                }
                string connString = "Data Source = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA = (SID = orcl))); " + "User Id=" + User + ";" + "Password=" + Password + consys;
                Conn = new OracleConnection();
                Conn.ConnectionString = connString;
                Conn.Open();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static OracleConnection Get_Connect()
        {
            if (Conn == null)
            {
                Connect();
            }
            return Conn;
        }
    }
}
