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
    public partial class HoaDon : Form
    {
        public HoaDon()
        {
            InitializeComponent();
            CenterToScreen();
            //checkBox1.Checked = true;
            list_hoadon.View = View.Details;
            LoadDataHoaDon();
            list_hoadon.SelectedIndexChanged += list_hoadon_SelectedIndexChanged;
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
        public void LoadDataHoaDon()
        {
            try
            {
                using (OracleConnection conn = Database.Get_Connection())
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }


                    using (OracleCommand cmd = new OracleCommand("BEGIN GetAllHoaDons(:data); END;", conn))
                    {
                        cmd.CommandType = CommandType.Text;


                        OracleParameter outputParam = new OracleParameter("data", OracleDbType.RefCursor);
                        outputParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(outputParam);


                        cmd.ExecuteNonQuery();


                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            list_hoadon.Items.Clear();

                            while (reader.Read())
                            {
                                ListViewItem item = new ListViewItem(reader["MaHoaDon"].ToString());

                                item.SubItems.Add(reader["NgayLap"].ToString());
                                item.SubItems.Add(reader["MaKhachHang"].ToString());
                                item.SubItems.Add(reader["MaNhanVien"].ToString());
                                item.SubItems.Add(reader["TongTien"].ToString());
                                list_hoadon.Items.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu từ bảng hóa đơn: " + ex.Message);
            }
        }

        private void list_hoadon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list_hoadon.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = list_hoadon.SelectedItems[0];
                txt_manv.Text = selectedItem.SubItems[0].Text;

                // Giải mã khi hiển thị
                txt_mahd.Text = selectedItem.SubItems[1].Text;
                DT_ngaylap.Text = selectedItem.SubItems[2].Text;
                txt_makh.Text = selectedItem.SubItems[3].Text;
                txt_manv.Text = selectedItem.SubItems[4].Text;
                txt_tongtien.Text = selectedItem.SubItems[5].Text;

            }

        }
        private string GenerateHoaDonId()
        {
            return Guid.NewGuid().ToString();
        }


        //private void btn_mahoa_Click(object sender, EventArgs e)
        //{
        //    // Khóa mã hóa
        //    int key = 5;

        //    if (checkBox1.Checked) // Mã hóa
        //    {
        //        if (!string.IsNullOrWhiteSpace(txt_mahd.Text))
        //        {
        //            txt_mahd.Text = Encrypt(txt_mahd.Text, key);
        //        }

        //        if (!string.IsNullOrWhiteSpace(DT_ngaylap.Text))
        //        {
        //            DT_ngaylap.Text = Encrypt(DT_ngaylap.Text, key);
        //        }

        //        if (!string.IsNullOrWhiteSpace(txt_makh.Text))
        //        {
        //            txt_makh.Text = Encrypt(txt_makh.Text, key);
        //        }

        //        if (!string.IsNullOrWhiteSpace(txt_manv.Text))
        //        {
        //            txt_manv.Text = Encrypt(txt_manv.Text, key);
        //        }

        //        if (!string.IsNullOrWhiteSpace(txt_tongtien.Text))
        //        {
        //            txt_tongtien.Text = Encrypt(txt_tongtien.Text, key);
        //        }

        //        MessageBox.Show("Mã hóa thành công!");
        //    }
        //    else if (checkBox2.Checked) // Giải mã
        //    {

        //        if (!string.IsNullOrWhiteSpace(txt_mahd.Text))
        //        {
        //            txt_mahd.Text = Encrypt(txt_mahd.Text, key);
        //        }

        //        if (!string.IsNullOrWhiteSpace(DT_ngaylap.Text))
        //        {
        //            DT_ngaylap.Text = Encrypt(DT_ngaylap.Text, key);
        //        }

        //        if (!string.IsNullOrWhiteSpace(txt_makh.Text))
        //        {
        //            txt_makh.Text = Encrypt(txt_makh.Text, key);
        //        }

        //        if (!string.IsNullOrWhiteSpace(txt_manv.Text))
        //        {
        //            txt_manv.Text = Encrypt(txt_manv.Text, key);
        //        }

        //        if (!string.IsNullOrWhiteSpace(txt_tongtien.Text))
        //        {
        //            txt_tongtien.Text = Encrypt(txt_tongtien.Text, key);
        //        }

        //        MessageBox.Show("Giải mã thành công!");
        //    }
        //}

        //private void checkBox1_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (checkBox1.Checked)
        //    {
        //        checkBox2.Checked = false;
        //        btn_mahoa.Text = "Encry Message";
        //    }
        //}

        //private void checkBox2_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (checkBox2.Checked)
        //    {
        //        checkBox1.Checked = false;
        //        btn_mahoa.Text = "Decry Message";
        //    }
        //}

        private void NhanVien_Load(object sender, EventArgs e)
        {
            LoadDataHoaDon();

        }
    }
}
