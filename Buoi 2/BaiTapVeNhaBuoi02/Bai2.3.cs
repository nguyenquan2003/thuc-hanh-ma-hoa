using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTapVeNhaBuoi02
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            CenterToScreen();
        }
        public static string EncryptTinhTrang(string input)
        {
            int factor = 3;  
            StringBuilder encryptedResult = new StringBuilder();

            foreach (char c in input)
            {
                if (char.IsLetter(c)) 
                {
                    char encryptedChar;

                    if (char.IsUpper(c)) 
                    {
                        encryptedChar = (char)((((c - 'A') * factor) % 26) + 'A');
                    }
                    else 
                    {
                        encryptedChar = (char)((((c - 'a') * factor) % 26) + 'a');
                    }

                    encryptedResult.Append(encryptedChar); 
                }
                else
                {
                    encryptedResult.Append(c); 
                }
            }

            return encryptedResult.ToString(); 
        }

        private void LoadTinhTrang()
        {
                try
                {
                    using (OracleConnection connection = Database.Get_Connect())
                    {
                        string query = "SELECT TINHTRANG FROM HOCVIEN";
                        using (OracleCommand command = new OracleCommand(query, connection))
                        {
                            using (OracleDataReader reader = command.ExecuteReader())
                            {
                                cbb_CSDL.Items.Clear();
                                while (reader.Read())
                                {
                                    string tinhTrang = reader["TINHTRANG"].ToString();
                                    string encryptedTinhTrang = EncryptTinhTrang(tinhTrang);
                                    cbb_CSDL.Items.Add(encryptedTinhTrang);
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
        private void btn_MaHoa_Click(object sender, EventArgs e)
        {
            if (cbb_CSDL.SelectedItem != null)
            {
                string tinhTrang = cbb_CSDL.SelectedItem.ToString(); 

                string encryptedTinhTrang = EncryptTinhTrang(tinhTrang);

                txt_KetQua.Text = encryptedTinhTrang;
            }
            else
            {
                MessageBox.Show("Vui lòng chọn tình trạng cần mã hóa.");
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
          
        }
    }
}
