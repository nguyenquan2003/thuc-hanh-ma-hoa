using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace DoAn_BMTT_36_BuiHungPhuong
{
    public class Database_sys
    {
        public static OracleConnection SysConn;

        // Phương thức kết nối dưới quyền SYSDBA
        public static bool ConnectAsSys(string sysUser, string sysPassword)
        {
            try
            {
                string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))"
                                    + "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=orcl)));"
                                    + "User ID=sys; Password=sys;DBA Privilege=SYSDBA";

                SysConn = new OracleConnection(connString);
                SysConn.Open();

                Console.WriteLine("Kết nối SYS thành công!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi kết nối SYS: " + ex.Message);
                return false;
            }
        }

       
    }

}
