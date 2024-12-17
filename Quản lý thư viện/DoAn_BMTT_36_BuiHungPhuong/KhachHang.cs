using ClosedXML.Excel;
using Oracle.ManagedDataAccess.Client;
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

namespace DoAn_BMTT_36_BuiHungPhuong
{
    public partial class KhachHang : Form
    {
        public KhachHang()
        {
            InitializeComponent();
            SetupListView();
            SetupListView1();
            LoadDataKH();
        }

        private void SetupListView()
        {
            // Xóa tất cả các cột cũ (nếu có)
            list_kh.Columns.Clear();

            // Thêm các cột mới
            list_kh.Columns.Add("Mã Khách Hàng", 100);
            list_kh.Columns.Add("Tên Khách Hàng", 150);
            list_kh.Columns.Add("Số Điện Thoại", 100);
            list_kh.Columns.Add("Email", 150);
            list_kh.Columns.Add("Địa Chỉ", 200);

            // Đặt chế độ hiển thị cho ListView
            list_kh.View = View.Details;


            list_des.Columns.Add("Mã Khách Hàng", 100);
            list_des.Columns.Add("Tên Khách Hàng", 150);
            list_des.Columns.Add("Số Điện Thoại", 100);
            list_des.Columns.Add("Email", 150);
            list_des.Columns.Add("Địa Chỉ", 200);
            list_des.View = View.Details;

        }

        private void SetupListView1()
        {
            // Xóa tất cả các cột cũ (nếu có)
            listView_KhachHang.Columns.Clear();

            // Thêm các cột mới
            listView_KhachHang.Columns.Add("Mã Khách Hàng", 100);
            listView_KhachHang.Columns.Add("Tên Khách Hàng", 150);
            listView_KhachHang.Columns.Add("Số Điện Thoại", 100);
            listView_KhachHang.Columns.Add("Email", 150);
            listView_KhachHang.Columns.Add("Địa Chỉ", 200);

            // Đặt chế độ hiển thị cho ListView
            listView_KhachHang.View = View.Details;
        }


        private static byte[] GetKey(string key)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            if (keyBytes.Length < 8)
            {
                Array.Resize(ref keyBytes, 8); 
            }
            return keyBytes;
        }

        public static string Encrypt(string plainText, string key)
        {
            try
            {
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    des.Key = GetKey(key);
                    des.IV = GetKey(key); // IV cũng sẽ được tạo từ khóa
                    byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(plainBytes, 0, plainBytes.Length);
                            cs.FlushFinalBlock();
                            return Convert.ToBase64String(ms.ToArray());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi mã hóa: " + ex.Message);
            }
        }

        public static string Decrypt(string cipherText, string key)
        {
            try
            {
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    des.Key = GetKey(key);
                    des.IV = GetKey(key); // IV cũng sẽ được tạo từ khóa
                    byte[] cipherBytes = Convert.FromBase64String(cipherText);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.FlushFinalBlock();
                            return Encoding.UTF8.GetString(ms.ToArray());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi giải mã: " + ex.Message);
            }
        }



        private string Encrypt(string plainText, int key)
        {
            StringBuilder encryptedText = new StringBuilder();
            foreach (char c in plainText)
            {

                int n = 256;
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

                int n = 256;
                char decryptedChar = (char)((c - key + n) % n);
                decryptedText.Append(decryptedChar);
            }
            return decryptedText.ToString();
        }

        public void LoadDataKH()
        {
            try
            {
                using (OracleConnection conn = Database.Get_Connection())
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }


                    using (OracleCommand cmd = new OracleCommand("BEGIN LoadKhachHangData(:data); END;", conn))
                    {
                        cmd.CommandType = CommandType.Text;


                        OracleParameter outputParam = new OracleParameter("data", OracleDbType.RefCursor);
                        outputParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(outputParam);


                        cmd.ExecuteNonQuery();


                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            listView_KhachHang.Items.Clear();

                            while (reader.Read())
                            {
                                ListViewItem item = new ListViewItem(reader["MaKhachHang"].ToString());


                                item.SubItems.Add(reader["TenKhachHang"].ToString());
                                item.SubItems.Add(reader["SDT"].ToString());
                                item.SubItems.Add(reader["Email"].ToString());
                                item.SubItems.Add(reader["DiaChi"].ToString());


                                listView_KhachHang.Items.Add(item);
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

        public void LoadDataSach()
        {
            int key = (int)num_key.Value;
            try
            {
                using (OracleConnection conn = Database.Get_Connection())
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }


                    using (OracleCommand cmd = new OracleCommand("BEGIN LoadKhachHangData(:data); END;", conn))
                    {
                        cmd.CommandType = CommandType.Text;


                        OracleParameter outputParam = new OracleParameter("data", OracleDbType.RefCursor);
                        outputParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(outputParam);


                        cmd.ExecuteNonQuery();


                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            list_kh.Items.Clear();

                            while (reader.Read())
                            {
                                if (comboBox1.SelectedItem != null)
                                {
                                    string selectedOperation = comboBox1.SelectedItem.ToString();
                                    if (selectedOperation == "Phép Cộng")
                                    {
                                        // Lấy dữ liệu từ DataReader
                                        string maKhachHang = reader["MaKhachHang"].ToString();
                                        string tenKhachHang = reader["TenKhachHang"].ToString();
                                        string sdt = reader["SDT"].ToString();
                                        string email = reader["Email"].ToString();
                                        string diaChi = reader["DiaChi"].ToString();

                                        // Mã hóa tất cả các trường cần thiết
                                        string encryptedMaKhachHang = Encrypt(maKhachHang, key);
                                        string encryptedTenKhachHang = Encrypt(tenKhachHang, key);
                                        string encryptedSDT = Encrypt(sdt, key);
                                        string encryptedEmail = Encrypt(email, key);
                                        string encryptedDiaChi = Encrypt(diaChi, key);

                                        // Tạo ListViewItem với mã hóa
                                        ListViewItem item = new ListViewItem(encryptedMaKhachHang);
                                        item.SubItems.Add(encryptedTenKhachHang);
                                        item.SubItems.Add(encryptedSDT);
                                        item.SubItems.Add(encryptedEmail);
                                        item.SubItems.Add(encryptedDiaChi);

                                        list_kh.Items.Add(item);
                                    }
                                    else
                                    {

                                        // Lấy dữ liệu từ DataReader
                                        string maKhachHang = reader["MaKhachHang"].ToString();
                                        string tenKhachHang = reader["TenKhachHang"].ToString();
                                        string sdt = reader["SDT"].ToString();
                                        string email = reader["Email"].ToString();
                                        string diaChi = reader["DiaChi"].ToString();

                                        // Mã hóa tất cả các trường cần thiết
                                        string encryptedMaKhachHang = EncryptWithMultiplication(maKhachHang, key);
                                        string encryptedTenKhachHang = EncryptWithMultiplication(tenKhachHang, key);
                                        string encryptedSDT = EncryptWithMultiplication(sdt, key);
                                        string encryptedEmail = EncryptWithMultiplication(email, key);
                                        string encryptedDiaChi = EncryptWithMultiplication(diaChi, key);

                                        // Tạo ListViewItem với mã hóa
                                        ListViewItem item = new ListViewItem(encryptedMaKhachHang);
                                        item.SubItems.Add(encryptedTenKhachHang);
                                        item.SubItems.Add(encryptedSDT);
                                        item.SubItems.Add(encryptedEmail);
                                        item.SubItems.Add(encryptedDiaChi);

                                        list_kh.Items.Add(item);
                                    }
                                }


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

        private void KhachHang_Load(object sender, EventArgs e)
        {

            LoadDataSach();
            comboBox1.Items.Add("Phép Cộng");
            comboBox1.Items.Add("Phép Nhân");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Checked = false;
                button1.Text = "Encry Message";
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Checked = false;
                button1.Text = "Decry Message";
            }
        }



        private void list_kh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list_kh.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = list_kh.SelectedItems[0];
                txt_makh.Text = selectedItem.SubItems[0].Text;

                // Giải mã khi hiển thị
                txt_tenkh.Text = selectedItem.SubItems[1].Text;
                txt_sdt.Text = selectedItem.SubItems[2].Text;
                txt_email.Text = selectedItem.SubItems[3].Text;
                txt_diachi.Text = selectedItem.SubItems[4].Text;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int key = (int)num_key.Value;
            if (comboBox1.SelectedItem != null)
            {
                string selectedOperation = comboBox1.SelectedItem.ToString();
                if (selectedOperation == "Phép Cộng")
                {
                    if (checkBox2.Checked)
                    {
                        if (!string.IsNullOrWhiteSpace(txt_makh.Text))
                        {
                            txt_makh.Text = Decrypt(txt_makh.Text, key);
                        }

                        if (!string.IsNullOrWhiteSpace(txt_tenkh.Text))
                        {
                            txt_tenkh.Text = Decrypt(txt_tenkh.Text, key);
                        }

                        if (!string.IsNullOrWhiteSpace(txt_sdt.Text))
                        {
                            txt_sdt.Text = Decrypt(txt_sdt.Text, key);
                        }

                        if (!string.IsNullOrWhiteSpace(txt_email.Text))
                        {
                            txt_email.Text = Decrypt(txt_email.Text, key);
                        }

                        if (!string.IsNullOrWhiteSpace(txt_diachi.Text))
                        {
                            txt_diachi.Text = Decrypt(txt_diachi.Text, key);
                        }



                        MessageBox.Show("Giải mã thành công!");
                    }
                }
                else
                {
                    if (checkBox2.Checked)
                    {
                        if (!string.IsNullOrWhiteSpace(txt_makh.Text))
                        {
                            txt_makh.Text = DecryptWithMultiplication(txt_makh.Text, key);
                        }

                        if (!string.IsNullOrWhiteSpace(txt_tenkh.Text))
                        {
                            txt_tenkh.Text = DecryptWithMultiplication(txt_tenkh.Text, key);
                        }

                        if (!string.IsNullOrWhiteSpace(txt_sdt.Text))
                        {
                            txt_sdt.Text = DecryptWithMultiplication(txt_sdt.Text, key);
                        }

                        if (!string.IsNullOrWhiteSpace(txt_email.Text))
                        {
                            txt_email.Text = DecryptWithMultiplication(txt_email.Text, key);
                        }

                        if (!string.IsNullOrWhiteSpace(txt_diachi.Text))
                        {
                            txt_diachi.Text = DecryptWithMultiplication(txt_diachi.Text, key);
                        }



                        MessageBox.Show("Giải mã thành công!");
                    }
                }

            }
        }

        private string EncryptWithMultiplication(string plainText, int key)
        {
            StringBuilder encryptedText = new StringBuilder();
            foreach (char c in plainText)
            {
                // Mã hóa bằng phép nhân
                int n = 256; // Số ký tự trong bảng mã ASCII
                char encryptedChar = (char)((c * key) % n);
                encryptedText.Append(encryptedChar);
            }
            return encryptedText.ToString();
        }

        private string DecryptWithMultiplication(string encryptedText, int key)
        {
            StringBuilder decryptedText = new StringBuilder();
            foreach (char c in encryptedText)
            {
                // Giải mã bằng phép chia
                int n = 256; // Số ký tự trong bảng mã ASCII
                             // Để giải mã, cần sử dụng phép nhân với modulo, nhưng không thể lấy được giá trị gốc
                             // Tùy thuộc vào key, chúng ta cần sử dụng một hàm mod inverse để giải mã.
                             // Đây chỉ là một ví dụ đơn giản. Trong thực tế, cần cẩn thận với cách mã hóa này.
                char decryptedChar = (char)((c * ModInverse(key, n)) % n);
                decryptedText.Append(decryptedChar);
            }
            return decryptedText.ToString();
        }

        // Hàm để tìm số nghịch đảo mod
        private int ModInverse(int a, int m)
        {
            a = a % m;
            for (int x = 1; x < m; x++)
            {
                if ((a * x) % m == 1)
                    return x;
            }
            return 1; // Trả về 1 nếu không tìm thấy
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataSach();




        }

        private void btn_excel_mahoa_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Excel Files|*.xlsx;*.xls"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                string encryptedFilePath = Path.Combine(Path.GetDirectoryName(filePath), "Encrypted_" + Path.GetFileName(filePath));
                int key = (int)num_key.Value;

                try
                {
                    using (var workbook = new XLWorkbook(filePath))
                    {
                        var worksheet = workbook.Worksheet(1); // Lấy sheet đầu tiên
                        foreach (var row in worksheet.RowsUsed())
                        {
                            foreach (var cell in row.CellsUsed())
                            {
                                cell.Value = Encrypt(cell.GetValue<string>(), key);
                            }
                        }
                        workbook.SaveAs(encryptedFilePath);
                    }
                    MessageBox.Show("Mã hóa thành công! File lưu tại: " + encryptedFilePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi mã hóa file Excel: " + ex.Message);
                }
            }
        }

        private void btn_excel_giaima_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Excel Files|*.xlsx;*.xls"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                int key = (int)num_key.Value;

                try
                {
                    using (var workbook = new XLWorkbook(filePath))
                    {
                        var worksheet = workbook.Worksheet(1);
                        using (OracleConnection conn = Database.Get_Connection())
                        {
                            try
                            {
                                if (conn.State == ConnectionState.Closed)
                                    conn.Open();

                                foreach (var row in worksheet.RowsUsed())
                                {
                                    string maKhachHangDecrypted = Decrypt(row.Cell(1).GetValue<string>(), key);
                                    int maKhachHang = int.Parse(maKhachHangDecrypted);
                                    string tenKhachHang = Decrypt(row.Cell(2).GetValue<string>(), key);
                                    string sdt = Decrypt(row.Cell(3).GetValue<string>(), key);
                                    string email = Decrypt(row.Cell(4).GetValue<string>(), key);
                                    string diaChi = Decrypt(row.Cell(5).GetValue<string>(), key);

                                    string query = "INSERT INTO KhachHang (MaKhachHang, TenKhachHang, SDT, Email, DiaChi) VALUES (:MaKhachHang, :TenKhachHang, :SDT, :Email, :DiaChi)";
                                    using (OracleCommand cmd = new OracleCommand(query, conn))
                                    {
                                        cmd.Parameters.Add(":MaKhachHang", maKhachHang);
                                        cmd.Parameters.Add(":TenKhachHang", tenKhachHang);
                                        cmd.Parameters.Add(":SDT", sdt);
                                        cmd.Parameters.Add(":Email", email);
                                        cmd.Parameters.Add(":DiaChi", diaChi);
                                        cmd.ExecuteNonQuery();
                                    }

                                }
                                LoadDataSach();
                                MessageBox.Show("Giải mã thành công và đã lưu vào trong cơ sở dữ liệu");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Lỗi khi giải mã và lưu dữ liệu: " + ex.Message);
                            }
                            finally
                            {
                                if (conn.State == ConnectionState.Open)
                                    conn.Close();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi giải mã file Excel: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text Files|*.txt"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                string encryptedFilePath = Path.Combine(Path.GetDirectoryName(filePath), "Encrypted_" + Path.GetFileName(filePath));
                int key = (int)num_key.Value;

                try
                {
                   
                    string content = File.ReadAllText(filePath);

                  
                    string encryptedContent = Encrypt(content, key);

                    
                    File.WriteAllText(encryptedFilePath, encryptedContent);

                    MessageBox.Show("Mã hóa thành công! File đã lưu tại: " + encryptedFilePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi mã hóa file TXT: " + ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text Files|*.txt"
            };
           

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                string decryptedFilePath = Path.Combine(Path.GetDirectoryName(filePath), "Decrypted_" + Path.GetFileName(filePath));
                int key = (int)num_key.Value;

                try
                {
                    // Mở và đọc file văn bản
                    string content = File.ReadAllText(filePath);

                    // Giải mã nội dung
                    string decryptedContent = Decrypt(content, key);

                    // Lưu vào file mới
                    File.WriteAllText(decryptedFilePath, decryptedContent);

                    MessageBox.Show("Giải mã thành công! File đã lưu tại: " + decryptedFilePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi giải mã file TXT: " + ex.Message);
                }
            }
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            try
            {
                
                using (OracleConnection conn = Database.Get_Connection())
                {
                    conn.Open();

                    
                    using (OracleCommand cmd = new OracleCommand("InsertKhachHang", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        
                        cmd.Parameters.Add("p_MaKhachHang", OracleDbType.Int32).Value = Convert.ToInt32(txt_makh.Text);
                        cmd.Parameters.Add("p_TenKhachHang", OracleDbType.NVarchar2).Value = txt_tenkh.Text;
                        cmd.Parameters.Add("p_SDT", OracleDbType.Varchar2).Value = txt_sdt.Text;
                        cmd.Parameters.Add("p_Email", OracleDbType.Varchar2).Value = txt_email.Text;
                        cmd.Parameters.Add("p_DiaChi", OracleDbType.NVarchar2).Value = txt_diachi.Text;

                        
                        cmd.ExecuteNonQuery();

                       
                        MessageBox.Show("Thêm khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {

                using (OracleConnection conn = Database.Get_Connection())
                {
                    conn.Open();                  
                    using (OracleCommand cmd = new OracleCommand("UpdateKhachHang", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("p_MaKhachHang", OracleDbType.Int32).Value = Convert.ToInt32(txt_makh.Text);
                        cmd.Parameters.Add("p_TenKhachHang", OracleDbType.NVarchar2).Value = string.IsNullOrEmpty(txt_tenkh.Text) ? (object)DBNull.Value : txt_tenkh.Text;
                        cmd.Parameters.Add("p_SDT", OracleDbType.Varchar2).Value = string.IsNullOrEmpty(txt_sdt.Text) ? (object)DBNull.Value : txt_sdt.Text;
                        cmd.Parameters.Add("p_Email", OracleDbType.Varchar2).Value = string.IsNullOrEmpty(txt_email.Text) ? (object)DBNull.Value : txt_email.Text;
                        cmd.Parameters.Add("p_DiaChi", OracleDbType.NVarchar2).Value = string.IsNullOrEmpty(txt_diachi.Text) ? (object)DBNull.Value : txt_diachi.Text;

                     
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Cập nhật khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                // Hiển thị lỗi nếu có
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {

  
                using (OracleConnection conn = Database.Get_Connection())
                {
                    conn.Open();
                  
                    using (OracleCommand cmd = new OracleCommand("DeleteKhachHang", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;                     
                        cmd.Parameters.Add("p_MaKhachHang", OracleDbType.Int32).Value = Convert.ToInt32(txt_makh.Text);
                        DialogResult result = MessageBox.Show(
                            "Bạn có chắc chắn muốn xóa khách hàng này không?",
                            "Xác nhận xóa",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning
                        );

                        if (result == DialogResult.Yes)
                        {                          
                            cmd.ExecuteNonQuery();                           
                            MessageBox.Show("Xóa khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listView_KhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem có mục nào được chọn hay không
                if (listView_KhachHang.SelectedItems.Count > 0)
                {
                    // Lấy mục được chọn
                    ListViewItem selectedItem = listView_KhachHang.SelectedItems[0];

                    // Tạo một ListViewItem mới cho bảng list_des
                    ListViewItem newItem = new ListViewItem(selectedItem.Text); // MaKhachHang

                    // Thêm các cột con
                    for (int i = 1; i < selectedItem.SubItems.Count; i++)
                    {
                        newItem.SubItems.Add(selectedItem.SubItems[i].Text);
                    }

                    // Thêm vào bảng list_des
                    list_des.Items.Add(newItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chuyển dữ liệu: " + ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                string key = txt_key_des.Text;// Khóa sử dụng để mã hóa
                foreach (ListViewItem item in list_des.Items)
                {
                    // Duyệt qua từng cột trong một hàng
                    for (int i = 0; i < item.SubItems.Count; i++)
                    {
                        // Mã hóa giá trị cột
                        string plainText = item.SubItems[i].Text;
                        string encryptedText = Encrypt(plainText, key);

                        // Cập nhật lại giá trị đã mã hóa
                        item.SubItems[i].Text = encryptedText;
                    }
                }

                MessageBox.Show("Dữ liệu đã được mã hóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mã hóa dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string key = txt_key_des.Text; // Khóa sử dụng để giải mã
                foreach (ListViewItem item in list_des.Items)
                {
                    // Duyệt qua từng cột trong một hàng
                    for (int i = 0; i < item.SubItems.Count; i++)
                    {
                        // Giải mã giá trị cột
                        string encryptedText = item.SubItems[i].Text;
                        string decryptedText = Decrypt(encryptedText, key);

                        // Cập nhật lại giá trị đã giải mã
                        item.SubItems[i].Text = decryptedText;
                    }
                }

                MessageBox.Show("Dữ liệu đã được giải mã thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi giải mã dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
