using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTapVeNhaBuoi02
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void LoadPhoneNumbers()
        {
            try
            {
                using (OracleConnection connection = Database.Get_Connect())
                {
                    string query = "SELECT DIENTHOAI FROM GIAOVIEN";
                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            cbb_CSDL.Items.Clear();
                            while (reader.Read())
                            {
                                cbb_CSDL.Items.Add(reader["DIENTHOAI"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải số điện thoại: " + ex.Message);
            }
        }

        private string EncryptPhoneNumber(string phoneNumber)
        {
            string encrypted = "";
            foreach (char digit in phoneNumber)
            {
                if (char.IsDigit(digit))
                {
                    int num = (int)char.GetNumericValue(digit);
                    num = (num + 3) % 10; 
                    encrypted += num.ToString();
                }
                else
                {
                    encrypted += digit; 
                }
            }
            return encrypted;
        }

        private void btn_MaHoa_Click(object sender, EventArgs e)
        {
            if (cbb_CSDL.SelectedItem != null)
            {
                string phoneNumber = cbb_CSDL.SelectedItem.ToString();
                string encryptedPhoneNumber = EncryptPhoneNumber(phoneNumber);
                txt_KetQua.Text = encryptedPhoneNumber;
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một số điện thoại.");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
