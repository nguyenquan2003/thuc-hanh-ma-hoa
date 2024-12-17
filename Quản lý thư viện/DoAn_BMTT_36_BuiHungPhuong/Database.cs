using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace DoAn_BMTT_36_BuiHungPhuong
{
    public class Database
    {
        public static OracleConnection Conn;

        public static string User;
        public static string Password;
        public static string Host;
        public static string Port;
        public static string ServiceName;

        public static void Set_Database(string user, string password, string host, string port, string serviceName)
        {
            Database.User = user;
            Database.Password = EncryptionHelper.Encrypt(password);
            Database.Host = host;
            Database.Port = port;
            Database.ServiceName = serviceName;
        }

        public static bool Connect()
        {
            try
            {
                string decryptedPassword = EncryptionHelper.Decrypt(Password);

                string connString = $"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={Host})(PORT={Port}))"
                                    + $"(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME={ServiceName})));"
                                    + $"User ID={User};Password={decryptedPassword};";

                Conn = new OracleConnection(connString);
                Conn.Open();

                Console.WriteLine("Kết nối thành công!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi kết nối: " + ex.Message);
                return false;
            }
        }

        public static OracleConnection Get_Connection()
        {
            if (Conn == null || Conn.State == ConnectionState.Closed)
            {
                Connect();
            }
            return Conn;
        }
    }
}
