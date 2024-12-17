using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace DoAn_BMTT_36_BuiHungPhuong
{
    public partial class Sach : Form
    {
        public Sach()
        {
            InitializeComponent();
            CenterToScreen();
            checkBox1.Checked = true;
        }

        private void Sach_Load(object sender, EventArgs e)
        {
            LoadDataSach();
        }

        private string Encrypt(string plainText, int key)
        {
            StringBuilder encryptedText = new StringBuilder();
            foreach (char c in plainText)
            {
                // Mã hóa bằng công thức C = (P + k) mod n
                int n = 128; // Tổng số ký tự (ví dụ dùng ASCII)
                char encryptedChar = (char)((c + key) % n);
                encryptedText.Append(encryptedChar);
            }
            return encryptedText.ToString();
        }

        private string Decrypt(string encryptedText, int key)
        {
            StringBuilder decryptedText = new StringBuilder();
            foreach (char c in encryptedText)
            {
                // Giải mã bằng công thức P = (C - k + n) mod n
                int n = 128; // Tổng số ký tự (ví dụ dùng ASCII)
                char decryptedChar = (char)((c - key + n) % n);
                decryptedText.Append(decryptedChar);
            }
            return decryptedText.ToString();
        }


        public void LoadDataSach()
        {
            try
            {
                using (OracleConnection conn = Database.Get_Connection())
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                  
                    using (OracleCommand cmd = new OracleCommand("BEGIN LoadSachData(:data); END;", conn))
                    {
                        cmd.CommandType = CommandType.Text;

                    
                        OracleParameter outputParam = new OracleParameter("data", OracleDbType.RefCursor);
                        outputParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(outputParam);

                   
                        cmd.ExecuteNonQuery();

                 
                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            list_sach.Items.Clear();

                            while (reader.Read())
                            {
                                ListViewItem item = new ListViewItem(reader["MaSach"].ToString());

                                item.SubItems.Add(reader["TenSach"].ToString());
                                item.SubItems.Add(reader["TheLoai"].ToString());
                                item.SubItems.Add(reader["GiaTien"].ToString());
                                item.SubItems.Add(reader["SoLuong"].ToString());
                                item.SubItems.Add(reader["NamXuatBan"].ToString());
                                item.SubItems.Add(reader["MaTacGia"].ToString());
                                item.SubItems.Add(reader["NhaXuatBan"].ToString());

                                list_sach.Items.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu từ bảng sách: " + ex.Message);
            }
        }


        private void list_sach_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list_sach.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = list_sach.SelectedItems[0];
                txt_masach.Text = selectedItem.SubItems[0].Text;

                // Giải mã khi hiển thị
                txt_tensach.Text = selectedItem.SubItems[1].Text;
                txt_theloai.Text = selectedItem.SubItems[2].Text;
                txt_giatien.Text = selectedItem.SubItems[3].Text;
                txt_soluong.Text = selectedItem.SubItems[4].Text;
                txt_namxuatban.Text = selectedItem.SubItems[5].Text;
                txt_matacgia.Text = selectedItem.SubItems[6].Text;
                nhaxuatban.Text = selectedItem.SubItems[7].Text;
            }
        }

       
        private string GenerateBookId()
        {
            return Guid.NewGuid().ToString();
        }
       

        


        private void btnSaveBook_Click(object sender, EventArgs e)
        {
          
        }

        private void btn_mahoa_Click(object sender, EventArgs e)
        {
            // Khóa mã hóa
            int key = 5;

            if (checkBox1.Checked) // Mã hóa
            {
                if (!string.IsNullOrWhiteSpace(txt_tensach.Text))
                {
                    txt_tensach.Text = Encrypt(txt_tensach.Text, key);
                }

                if (!string.IsNullOrWhiteSpace(txt_theloai.Text))
                {
                    txt_theloai.Text = Encrypt(txt_theloai.Text, key);
                }

                if (!string.IsNullOrWhiteSpace(txt_giatien.Text))
                {
                    txt_giatien.Text = Encrypt(txt_giatien.Text, key);
                }

                if (!string.IsNullOrWhiteSpace(txt_soluong.Text))
                {
                    txt_soluong.Text = Encrypt(txt_soluong.Text, key);
                }

                if (!string.IsNullOrWhiteSpace(txt_namxuatban.Text))
                {
                    txt_namxuatban.Text = Encrypt(txt_namxuatban.Text, key);
                }

                if (!string.IsNullOrWhiteSpace(txt_matacgia.Text))
                {
                    txt_matacgia.Text = Encrypt(txt_matacgia.Text, key);
                }

                if (!string.IsNullOrWhiteSpace(nhaxuatban.Text))
                {
                    nhaxuatban.Text = Encrypt(nhaxuatban.Text, key);
                }

                MessageBox.Show("Mã hóa thành công!");
            }
            else if (checkBox2.Checked) // Giải mã
            {
                if (!string.IsNullOrWhiteSpace(txt_tensach.Text))
                {
                    txt_tensach.Text = Decrypt(txt_tensach.Text, key);
                }

                if (!string.IsNullOrWhiteSpace(txt_theloai.Text))
                {
                    txt_theloai.Text = Decrypt(txt_theloai.Text, key);
                }

                if (!string.IsNullOrWhiteSpace(txt_giatien.Text))
                {
                    txt_giatien.Text = Decrypt(txt_giatien.Text, key);
                }

                if (!string.IsNullOrWhiteSpace(txt_soluong.Text))
                {
                    txt_soluong.Text = Decrypt(txt_soluong.Text, key);
                }

                if (!string.IsNullOrWhiteSpace(txt_namxuatban.Text))
                {
                    txt_namxuatban.Text = Decrypt(txt_namxuatban.Text, key);
                }

                if (!string.IsNullOrWhiteSpace(txt_matacgia.Text))
                {
                    txt_matacgia.Text = Decrypt(txt_matacgia.Text, key);
                }

                if (!string.IsNullOrWhiteSpace(nhaxuatban.Text))
                {
                    nhaxuatban.Text = Decrypt(nhaxuatban.Text, key);
                }

                MessageBox.Show("Giải mã thành công!");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Checked = false; 
                btn_mahoa.Text = "Encry Message"; 
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Checked = false; 
                btn_mahoa.Text = "Decry Message"; 
            }
        }
    }
}
