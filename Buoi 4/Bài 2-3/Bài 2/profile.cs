using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Types;
namespace Bài_2
{
    class profile
    {
        OracleConnection conn;

        public profile(OracleConnection conn)
        {
            this.conn = conn;
        }
        public DataTable Getname_Profile()
        {
            try
            {
                string Procedure = "pro_profile_Name";

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
                    DataTable data = new DataTable();
                    data.Load(ret);
                    return data;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().ToString());
            }
            return null;
        }
        public DataTable Get_Profile(string NameProfile)
        {
            try
            {
                string Proceduce = "pro_profile";
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = Proceduce;
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter ressultParam = new OracleParameter();
                ressultParam.ParameterName = "@Result";
                ressultParam.OracleDbType = OracleDbType.RefCursor;
                ressultParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(ressultParam);

                OracleParameter pro_name = new OracleParameter();
                pro_name.ParameterName = "@profile_name";
                pro_name.OracleDbType = OracleDbType.Varchar2;
                pro_name.Value = NameProfile;
                pro_name.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(pro_name);

                cmd.ExecuteNonQuery();
                if (ressultParam.Value != DBNull.Value)
                {
                    OracleDataReader ret = ((OracleRefCursor)ressultParam.Value).GetDataReader();
                    DataTable data = new DataTable();
                    data.Load(ret);
                    return data;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().ToString());
            }
            return null;
        }
    }
}
