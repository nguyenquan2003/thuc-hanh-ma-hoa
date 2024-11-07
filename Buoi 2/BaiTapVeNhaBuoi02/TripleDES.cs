using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO; // Thêm dòng này
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;



namespace BaiTapVeNhaBuoi02
{
    public partial class TripleDES : Form
    {
        public TripleDES()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtDuongDan_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonLoadDuongDan_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtDuongDan.Text = openFileDialog.FileName;
                }
            }
        }

        private void btnMaHoa_Click(object sender, EventArgs e)
        {
            string filePath = txtDuongDan.Text;
            string key = txtKey.Text;

            if (File.Exists(filePath) && !string.IsNullOrEmpty(key) && key.Length == 24)
            {
                byte[] encryptedData = EncryptFile(File.ReadAllBytes(filePath), key);
                File.WriteAllBytes(filePath + ".enc", encryptedData);
                MessageBox.Show("Tập tin đã được mã hóa thành công!");
            }
            else
            {
                MessageBox.Show("Đường dẫn tệp hoặc khóa không hợp lệ. Đảm bảo khóa dài 24 ký tự.");
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGiaiMa_Click(object sender, EventArgs e)
        {
            string encryptedFilePath = txtDuongDan.Text; // Đường dẫn tới FileText.txt.enc
            string key = txtKey.Text;

            if (File.Exists(encryptedFilePath) && !string.IsNullOrEmpty(key) && key.Length == 24)
            {
                try
                {
                    byte[] encryptedData = File.ReadAllBytes(encryptedFilePath);  // Đọc dữ liệu từ FileText.txt.enc

                    // Kiểm tra dữ liệu mã hóa có hợp lệ hay không
                    if (encryptedData.Length == 0)
                    {
                        MessageBox.Show("File mã hóa rỗng hoặc không hợp lệ.");
                        return;
                    }

                    byte[] decryptedData = DecryptFile(encryptedData, key);  // Giải mã dữ liệu

                    // Kiểm tra xem dữ liệu giải mã có rỗng không
                    if (decryptedData.Length == 0)
                    {
                        MessageBox.Show("Dữ liệu giải mã không hợp lệ.");
                        return;
                    }

                    // Lưu dữ liệu đã giải mã vào đường dẫn yêu cầu
                    string decryptedFilePath = @"D:\4.HocKi_7\TH_BaoMat\27_NguyenHuuSang_Buoi02\BaiTapVeNhaBuoi02\FileGiaiMa.dec";
                    File.WriteAllBytes(decryptedFilePath, decryptedData); // Lưu lại file đã giải mã

                    MessageBox.Show("Giải mã hoàn tất, dữ liệu được lưu vào " + decryptedFilePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Giải mã thất bại: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Đường dẫn tệp hoặc khóa không hợp lệ. Đảm bảo khóa dài 24 ký tự.");
            }

        }



        private byte[] EncryptFile(byte[] fileData, string key)
        {
            using (TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider())
            {
                tdes.Key = Encoding.UTF8.GetBytes(key);  // Thiết lập khóa (24 byte)
                tdes.Mode = CipherMode.ECB;              // Sử dụng chế độ ECB
                tdes.Padding = PaddingMode.PKCS7;        // Sử dụng Padding PKCS7

                using (ICryptoTransform transform = tdes.CreateEncryptor())
                {
                    return transform.TransformFinalBlock(fileData, 0, fileData.Length);
                }
            }
        }

        private byte[] DecryptFile(byte[] encryptedData, string key)
        {
            using (TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider())
            {
                tdes.Key = Encoding.UTF8.GetBytes(key);  // Thiết lập khóa (24 byte)
                tdes.Mode = CipherMode.ECB;              // Sử dụng chế độ ECB
                tdes.Padding = PaddingMode.PKCS7;        // Sử dụng Padding PKCS7

                using (ICryptoTransform transform = tdes.CreateDecryptor())
                {
                    return transform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
                }
            }
        }



        private void btnSinhKhoa_Click(object sender, EventArgs e)
        {
            txtKey.Text = GenerateRandomKey();
        }


        private string GenerateRandomKey()
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder key = new StringBuilder(24);
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] randomBytes = new byte[24];
                rng.GetBytes(randomBytes);
                foreach (byte b in randomBytes)
                {
                    key.Append(validChars[b % validChars.Length]);
                }
            }
            return key.ToString();
        }


        private bool IsValidKey(string key)
        {
            try
            {
                byte[] keyBytes = Convert.FromBase64String(key);
                return keyBytes.Length == 24;
            }
            catch
            {
                return false;
            }
        }
    }
}
