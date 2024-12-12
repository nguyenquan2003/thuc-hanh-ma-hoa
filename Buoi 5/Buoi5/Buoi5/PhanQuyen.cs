using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;
using System.Windows.Forms;

namespace Buoi5
{
    public class PhanQuyen
    {
        OracleConnection conn;

        public PhanQuyen(OracleConnection conn)
        {
            this.conn = conn;
        }
        public OracleDataReader Get_User()
        {
            try
            {
                string Procedure = "sys.pkg_PhanQuyen.pro_select_user";

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = Procedure;
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter resultParam = new OracleParameter();
                resultParam.ParameterName = "@Result";
                resultParam.OracleDbType = OracleDbType.RefCursor;
                resultParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(resultParam);

                cmd.ExecuteNonQuery();
                if(resultParam.Value != DBNull.Value)
                {
                    OracleDataReader ret = ((OracleRefCursor)resultParam.Value).GetDataReader();
                    return ret;
                }
            }
            catch
            {
                MessageBox.Show("Lỗi gọi chạy thủ tục: pro_select_user");
                return null;
            }
            return null;
        }
        public OracleDataReader Get_Role()
        {
            try
            {
                string Procedure = "sys.pkg_PhanQuyen.pro_select_roles";
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = Procedure;
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter resultParam = new OracleParameter();
                resultParam.ParameterName = "@Result";
                resultParam.OracleDbType = OracleDbType.RefCursor;
                resultParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(resultParam);

                cmd.ExecuteNonQuery();
                if (resultParam.Value != DBNull.Value)
                {
                    OracleDataReader ret = ((OracleRefCursor)resultParam.Value).GetDataReader();
                    return ret;
                }
            }
            catch
            {
                MessageBox.Show("Lỗi gọi chạy thủ tục: pro_select_user");
                return null;
            }
            return null;
        }


        public OracleDataReader Get_Proceduce;

        public OracleDataReader Get_Table_User(string userowner)
        {
            try
            {
                string Procedure = "sys.pkg_PhanQuyen.pro_select_table";

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = Procedure;
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter UserOwner = new OracleParameter();
                UserOwner.ParameterName = "@userowner";
                UserOwner.OracleDbType = OracleDbType.Varchar2;
                UserOwner.Value = userowner.ToUpper();
                UserOwner.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(UserOwner);

                OracleParameter resultParam = new OracleParameter();
                resultParam.ParameterName = "@Result";
                resultParam.OracleDbType = OracleDbType.RefCursor;
                resultParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(resultParam);

                cmd.ExecuteNonQuery();
                if(resultParam.Value != DBNull.Value)
                {
                    OracleDataReader ret = ((OracleRefCursor)resultParam.Value).GetDataReader();
                    return null;
                }
                return null;
            }
            catch
            {
                MessageBox.Show("Loi goi chay tuc tuc: pro_select_table");
            }

        }
        public DataTable Get_Roles_User(string username)
        {
            try
            {
                string Procedure = "sys.pkg_PhanQuyen.pro_user_roles";
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = Procedure;
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter UserName = new OracleParameter();
                UserName.ParameterName = "@Username";
                UserName.OracleDbType = OracleDbType.Varchar2;
                UserName.Value = username.ToUpper();
                UserName.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(UserName);

                OracleParameter resultParam = new OracleParameter();
                resultParam.ParameterName = "@Result";
                resultParam.OracleDbType = OracleDbType.RefCursor;
                resultParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(resultParam);

                cmd.ExecuteNonQuery();

                if (resultParam.Value != DBNull.Value)
                {
                    OracleDataReader ret = ((OracleRefCursor)resultParam.Value).GetDataReader();
                    DataTable data = new DataTable();
                    data.Load(ret);
                    return data;
                }
            }
            catch
            {
                MessageBox.Show("Lỗi Gọi chạy thủ tục: pro_user_roles");
                return null;
            }
            return null;
        }









        public int Get_Roles_User_Check(string username, string roles)
        {
            try
            {
                string Procedure = "sys.pkg_PhanQuyen.pro_user_roles_check";

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = Procedure;
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter UserName = new OracleParameter();
                UserName.ParameterName = "@username";
                UserName.OracleDbType = OracleDbType.Varchar2;
                UserName.Value = username.ToUpper();
                UserName.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(UserName);

                OracleParameter Roles = new OracleParameter();
                Roles.ParameterName = "@roles";
                Roles.OracleDbType = OracleDbType.Varchar2;
                Roles.Value = roles.ToUpper();
                Roles.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(Roles);

                OracleParameter resultParam = new OracleParameter();
                resultParam.ParameterName = "@Result";
                resultParam.OracleDbType = OracleDbType.Int16;
                resultParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(resultParam);

                cmd.ExecuteNonQuery();

                if (resultParam.Value != DBNull.Value)
                {
                    return int.Parse(resultParam.Value.ToString());
                }
            }
            catch
            {
                MessageBox.Show("Lỗi Gọi chạy thủ tục: pro_user_roles");
                return -1;
            }
            return -1;
        }


        public DataTable Get_Grant_User(string username)
        {
            try
            {
                string Procedure = "sys.pkg_PhanQuyen.pro_select_grant_user";

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = Procedure;
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter UserName = new OracleParameter();
                UserName.ParameterName = "@username";
                UserName.OracleDbType = OracleDbType.Varchar2;
                UserName.Value = username.ToUpper();
                UserName.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(UserName);

                OracleParameter resultParam = new OracleParameter();
                resultParam.ParameterName = "@Result";
                resultParam.OracleDbType = OracleDbType.RefCursor;
                resultParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(resultParam);

                cmd.ExecuteNonQuery();

                if (resultParam.Value != DBNull.Value)
                {
                    OracleDataReader ret = ((OracleRefCursor)resultParam.Value).GetDataReader();
                    DataTable data = new DataTable();
                    data.Load(ret);
                    return data;
                }
            }
            catch
            {
                MessageBox.Show("Lỗi Gọi chạy thủ tục: pro_select_grant_user");
                return null;
            }
            return null;
        }










    }
}
