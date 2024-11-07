using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTapVeNhaBuoi02
{
    internal class MaHoaPhepNhan
    {
        private OracleConnection conn;
        public MaHoaPhepNhan(OracleConnection conn)
        {
            this.conn = conn;
        }
        public string EncryptMultiplicative_Func(string PlainText, int key)
        {
            try
            {
                string Function = "encryptMultiplicativeCipher"; 

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = Function;
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter resultParam = new OracleParameter();
                resultParam.ParameterName = "@Result";
                resultParam.OracleDbType = OracleDbType.Varchar2;
                resultParam.Size = 100;
                resultParam.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(resultParam);

                OracleParameter str = new OracleParameter();
                str.ParameterName = "@str";
                str.OracleDbType = OracleDbType.Varchar2;
                str.Value = PlainText;
                str.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(str);

                OracleParameter k = new OracleParameter();
                k.ParameterName = "@k";
                k.OracleDbType = OracleDbType.Int32;
                k.Value = key;
                k.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(k);

                cmd.ExecuteNonQuery();
                string s = "null";
                if (resultParam.Value != DBNull.Value)
                {
                    OracleString ret = (OracleString)resultParam.Value;
                    s = ret.ToString();
                }
                return s;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().ToString());
            }
            return null;
        }
        public string DecryptMultiplicative_Func(string EncryptedText, int key)
        {
            try
            {
                string Function = "decryptMultiplicativeCipher"; 
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = Function;
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter resultParam = new OracleParameter();
                resultParam.ParameterName = "@Result";
                resultParam.OracleDbType = OracleDbType.Varchar2;
                resultParam.Size = 100;
                resultParam.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(resultParam);

                OracleParameter str = new OracleParameter();
                str.ParameterName = "@str";
                str.OracleDbType = OracleDbType.Varchar2;
                str.Value = EncryptedText;
                str.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(str);

                OracleParameter k = new OracleParameter();
                k.ParameterName = "@k";
                k.OracleDbType = OracleDbType.Int32;
                k.Value = key;
                k.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(k);

                cmd.ExecuteNonQuery();

                string s = "null";
                if (resultParam.Value != DBNull.Value)
                {
                    OracleString ret = (OracleString)resultParam.Value;
                    s = ret.ToString();
                }
                return s;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().ToString());
            }
            return null;
        }
        public string EncryptMultiplicative_Proc(string PlainText, int key)
        {
            try
            {
                string Procedure = "encryptMultiplicativeCipher_Proc"; 
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = Procedure;
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter str = new OracleParameter();
                str.ParameterName = "@str";
                str.OracleDbType = OracleDbType.Varchar2;
                str.Value = PlainText;
                str.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(str);

                OracleParameter k = new OracleParameter();
                k.ParameterName = "@k";
                k.OracleDbType = OracleDbType.Int32;
                k.Value = key;
                k.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(k);

                OracleParameter kq = new OracleParameter();
                kq.ParameterName = "@enc";
                kq.OracleDbType = OracleDbType.Varchar2;
                kq.Size = 100;
                kq.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(kq);

                cmd.ExecuteNonQuery();

                return kq.Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().ToString());
            }
            return null;
        }

        public string DecryptMultiplicative_Proc(string EncryptedText, int key)
        {
            try
            {
                string Procedure = "decryptMultiplicativeCipher_Proc";
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = Procedure;
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter str = new OracleParameter();
                str.ParameterName = "@dec";
                str.OracleDbType = OracleDbType.Varchar2;
                str.Value = EncryptedText;
                str.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(str);

                OracleParameter k = new OracleParameter();
                k.ParameterName = "@k";
                k.OracleDbType = OracleDbType.Int32;
                k.Value = key;
                k.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(k);

                OracleParameter kq = new OracleParameter();
                kq.ParameterName = "@dec";
                kq.OracleDbType = OracleDbType.Varchar2;
                kq.Size = 100;
                kq.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(kq);

                cmd.ExecuteNonQuery();

                return kq.Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().ToString());
            }
            return null;
        }
    }
}
