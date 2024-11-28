using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;
using System.Windows.Forms;

namespace Bai3
{
    public class RSAOracle
    {
        OracleConnection conn;

        public RSAOracle(OracleConnection conn)
        {
            this.conn = conn;
        }

        //ham tao khoa chung
        public string generateKey(int keySize)
        {
            try
            {
                string Function = "CRYPTO.RSA_GENERATE_KEYS";

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = Function;
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter resultParam = new OracleParameter();
                resultParam.ParameterName = "@Result";
                resultParam.OracleDbType = OracleDbType.Varchar2;
                resultParam.Size = 10000;
                resultParam.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(resultParam);

                OracleParameter size = new OracleParameter();
                size.ParameterName = "@Key_Size";
                size.OracleDbType = OracleDbType.Varchar2;
                size.Value = keySize;
                size.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(size);

                cmd.ExecuteNonQuery();
                if (resultParam.Value != DBNull.Value)
                {
                    OracleString ret = (OracleString)resultParam.Value;
                    return ret.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().ToString());
            }

            return null;
        }

        //Ham ma hoa
        public string encrypt(string plainText, string publicKey)
        {
            try
            {
                string Function = "CRYPTO.RSA_ENCRYPT";

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = Function;
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter resultParam = new OracleParameter();
                resultParam.ParameterName = "@Result";
                resultParam.OracleDbType = OracleDbType.Varchar2;
                resultParam.Size = 10000;
                resultParam.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(resultParam);

                OracleParameter pltext = new OracleParameter();
                pltext.ParameterName = "@PLAIN_TEXT";
                pltext.OracleDbType = OracleDbType.Varchar2;
                pltext.Value = plainText;
                pltext.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(pltext);

                OracleParameter key = new OracleParameter();
                key.ParameterName = "@PRIVATE_KEY";
                key.OracleDbType = OracleDbType.Varchar2;
                key.Value = publicKey;
                key.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(key);

                cmd.ExecuteNonQuery();
                if (resultParam.Value != DBNull.Value)
                {
                    OracleString ret = (OracleString)resultParam.Value;
                    return ret.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().ToString());
            }

            return null;
        }

        //Chạy hàm giải mã
        public string decrypt(string encrypted, string privateKey)
        {
            try
            {
                string Function = "CRYPTO.RSA_DECRYPT";

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = Function;
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter resultParam = new OracleParameter();
                resultParam.ParameterName = "@Result";
                resultParam.OracleDbType = OracleDbType.Varchar2;
                resultParam.Size = 10000;
                resultParam.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(resultParam);

                OracleParameter pltext = new OracleParameter();
                pltext.ParameterName = "@ENCRYPTED_TEXT";
                pltext.OracleDbType = OracleDbType.Varchar2;
                pltext.Value = encrypted;
                pltext.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(pltext);

                OracleParameter key = new OracleParameter();
                key.ParameterName = "@PRIVATE_KEY";
                key.OracleDbType = OracleDbType.Varchar2;
                key.Value = privateKey;
                key.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(key);

                cmd.ExecuteNonQuery();
                if (resultParam.Value != DBNull.Value)
                {
                    OracleString ret = (OracleString)resultParam.Value;
                    return ret.ToString();
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
