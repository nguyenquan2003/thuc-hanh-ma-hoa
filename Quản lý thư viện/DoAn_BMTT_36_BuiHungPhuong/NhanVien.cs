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

namespace DoAn_BMTT_36_BuiHungPhuong
{
    public partial class NhanVien : Form
    {
        public NhanVien()
        {
            InitializeComponent();
            CenterToScreen();
            //checkBox1.Checked = true;
            list_nhanvien.View = View.Details;
            list_nhanvien.Columns.Add("Mã Nhân Viên", 100);
            list_nhanvien.Columns.Add("Họ Và Tên", 100);
            list_nhanvien.Columns.Add("Chức Vụ", 100);
            list_nhanvien.Columns.Add("Ngày Sinh", 100);
            list_nhanvien.Columns.Add("SDT", 100);
            list_nhanvien.Columns.Add("Email", 100);
            list_nhanvien.Columns.Add("Dịa Chỉ", 100);
            LoadDataNhanVien();
            list_nhanvien.SelectedIndexChanged += list_tacgia_SelectedIndexChanged;
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


        public void LoadDataNhanVien()
        {
            try
            {
                using (OracleConnection conn = Database.Get_Connection())
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }


                    using (OracleCommand cmd = new OracleCommand("BEGIN GetAllNhanViens(:data); END;", conn))
                    {
                        cmd.CommandType = CommandType.Text;


                        OracleParameter outputParam = new OracleParameter("data", OracleDbType.RefCursor);
                        outputParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(outputParam);


                        cmd.ExecuteNonQuery();


                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            list_nhanvien.Items.Clear();

                            while (reader.Read())
                            {
                                ListViewItem item = new ListViewItem(reader["MaNhanVien"].ToString());

                                item.SubItems.Add(reader["TenNhanVien"].ToString());
                                item.SubItems.Add(reader["ChucVu"].ToString());
                                item.SubItems.Add(reader["NgaySinh"].ToString());
                                item.SubItems.Add(reader["SDT"].ToString());
                                item.SubItems.Add(reader["Email"].ToString());
                                item.SubItems.Add(reader["DiaChi"].ToString());


                                list_nhanvien.Items.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu từ bảng tác giả: " + ex.Message);
            }
        }


        private void list_tacgia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list_nhanvien.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = list_nhanvien.SelectedItems[0];
                txt_manv.Text = selectedItem.SubItems[0].Text;

                // Giải mã khi hiển thị
                txt_tennv.Text = selectedItem.SubItems[1].Text;
                txt_chucvu.Text = selectedItem.SubItems[2].Text;
                txt_email.Text = selectedItem.SubItems[3].Text;
                txt_diachi.Text = selectedItem.SubItems[4].Text;
                txt_sdt.Text = selectedItem.SubItems[5].Text;
                DT_ngaysinh.Text = selectedItem.SubItems[6].Text;

            }
        }


        private string GenerateTacGiaId()
        {
            return Guid.NewGuid().ToString();
        }





        //private void btnSaveBook_Click(object sender, EventArgs e)
        //{

        //}

        //private void btn_mahoa_Click(object sender, EventArgs e)
        //{
        //    //// Khóa mã hóa
        //    //int key = 5;

        //    //if (checkBox1.Checked) // Mã hóa
        //    //{
        //    //    if (!string.IsNullOrWhiteSpace(txt_manv.Text))
        //    //    {
        //    //        txt_manv.Text = Encrypt(txt_manv.Text, key);
        //    //    }

        //    //    if (!string.IsNullOrWhiteSpace(txt_tennv.Text))
        //    //    {
        //    //        txt_tennv.Text = Encrypt(txt_tennv.Text, key);
        //    //    }

        //    //    if (!string.IsNullOrWhiteSpace(txt_chucvu.Text))
        //    //    {
        //    //        txt_chucvu.Text = Encrypt(txt_chucvu.Text, key);
        //    //    }

        //    //    if (!string.IsNullOrWhiteSpace(txt_email.Text))
        //    //    {
        //    //        txt_email.Text = Encrypt(txt_email.Text, key);
        //    //    }

        //    //    if (!string.IsNullOrWhiteSpace(txt_diachi.Text))
        //    //    {
        //    //        txt_diachi.Text = Encrypt(txt_diachi.Text, key);
        //    //    }

        //    //    if (!string.IsNullOrWhiteSpace(DT_ngaysinh.Text))
        //    //    {
        //    //        DT_ngaysinh.Text = Encrypt(DT_ngaysinh.Text, key);
        //    //    }
        //    //    if (!string.IsNullOrWhiteSpace(txt_sdt.Text))
        //    //    {
        //    //        txt_sdt.Text = Encrypt(txt_sdt.Text, key);
        //    //    }


        //    //    MessageBox.Show("Mã hóa thành công!");
        //    }
        //    //else if (checkBox2.Checked) // Giải mã
        //    //{

        //    //    if (!string.IsNullOrWhiteSpace(txt_manv.Text))
        //    //    {
        //    //        txt_manv.Text = Encrypt(txt_manv.Text, key);
        //    //    }

        //    //    if (!string.IsNullOrWhiteSpace(txt_tennv.Text))
        //    //    {
        //    //        txt_tennv.Text = Encrypt(txt_tennv.Text, key);
        //    //    }

        //    //    if (!string.IsNullOrWhiteSpace(txt_chucvu.Text))
        //    //    {
        //    //        txt_chucvu.Text = Encrypt(txt_chucvu.Text, key);
        //    //    }

        //    //    if (!string.IsNullOrWhiteSpace(txt_email.Text))
        //    //    {
        //    //        txt_email.Text = Encrypt(txt_email.Text, key);
        //    //    }

        //    //    if (!string.IsNullOrWhiteSpace(txt_diachi.Text))
        //    //    {
        //    //        txt_diachi.Text = Encrypt(txt_diachi.Text, key);
        //    //    }

        //    //    if (!string.IsNullOrWhiteSpace(DT_ngaysinh.Text))
        //    //    {
        //    //        DT_ngaysinh.Text = Encrypt(DT_ngaysinh.Text, key);
        //    //    }
        //    //    if (!string.IsNullOrWhiteSpace(txt_sdt.Text))
        //    //    {
        //    //        txt_sdt.Text = Encrypt(txt_sdt.Text, key);
        //    //    }

        //    //    MessageBox.Show("Giải mã thành công!");
        //    //}
        //}

        ////private void checkBox1_CheckedChanged(object sender, EventArgs e)
        ////{
        ////    if (checkBox1.Checked)
        ////    {
        ////        checkBox2.Checked = false;
        ////        btn_mahoa.Text = "Encry Message";
        ////    }
        ////}

        ////private void checkBox2_CheckedChanged(object sender, EventArgs e)
        ////{
        ////    if (checkBox2.Checked)
        ////    {
        ////        checkBox1.Checked = false;
        ////        btn_mahoa.Text = "Decry Message";
        ////    }
        ////}

        private void NhanVien_Load(object sender, EventArgs e)
        {
            LoadDataNhanVien();

        }
    }
}
