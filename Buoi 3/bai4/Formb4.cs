using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

using System.Data;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Bai4
{
    public partial class Formb4 : Form
    {
        private RSACryptoServiceProvider rsa;
        public Formb4()
        {
            InitializeComponent();
        }

        private void btn_OpenPublicKey_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txt_publicKey.Text = System.IO.File.ReadAllText(dlg.FileName);
            }
        }

        private void btn_Generate_Click(object sender, EventArgs e)
        {
            rsa = new RSACryptoServiceProvider();
            string publicKey = Convert.ToBase64String(rsa.ExportCspBlob(false));
            txt_publicKey.Text = publicKey;

            string privateKey = Convert.ToBase64String(rsa.ExportCspBlob(true));
            txt_privateKey.Text = privateKey;
        }

        private void Formb4_Load(object sender, EventArgs e)
        {

        }

        private void btn_encrypt_Click(object sender, EventArgs e)
        {
            string plaintext = txt_plaintext.Text;
            if (plaintext == "")
            {
                MessageBox.Show("Vui long nhap vao plaintext.");
                return;
            }

            if (txt_publicKey.Text == "")
            {
                MessageBox.Show("Vui long tao khoa va nhap khoa cong khai.");
                return;
            }

            rsa = new RSACryptoServiceProvider();
            rsa.ImportCspBlob(Convert.FromBase64String(txt_publicKey.Text));

            byte[] data = Encoding.UTF8.GetBytes(plaintext);
            int maxBlockSize = rsa.KeySize / 8 - 42;
            List<byte> encryptedData = new List<byte>();

            for (int i = 0; i < data.Length; i += maxBlockSize)
            {
                int currentBlockSize = Math.Min(maxBlockSize, data.Length - i);
                byte[] dataBlock = new byte[currentBlockSize];
                Array.Copy(data, i, dataBlock, 0, currentBlockSize);

                byte[] encryptedBlock = rsa.Encrypt(dataBlock, true);
                encryptedData.AddRange(encryptedBlock);
            }

            txt_encrypt.Text = Convert.ToBase64String(encryptedData.ToArray());
        }
        private void btn_decrypt_Click(object sender, EventArgs e)
        {
            string ciphertext = txt_encrypt.Text;
            if(string.IsNullOrEmpty(ciphertext))
            {
                MessageBox.Show("Vui long nhap vao ciphertext.");
                return;
            }

            if (string.IsNullOrEmpty(txt_privateKey.Text))
            {
                MessageBox.Show("Vui long tap khoa va nhap khoa bi mat.");
                return;
            }
            try
            {
                rsa = new RSACryptoServiceProvider();
                rsa.ImportCspBlob(Convert.FromBase64String(txt_privateKey.Text));

                byte[] encryptedData = Convert.FromBase64String(ciphertext);

                int blockSize = rsa.KeySize / 8;

                List<byte> decryptedData = new List<byte>();
                for (int i = 0; i < encryptedData.Length; i+= blockSize)
                {
                    byte[] encryptedBlock = new byte[blockSize];
                    Array.Copy(encryptedData, i, encryptedData, 0, blockSize);

                    byte[] decryptedBlock = rsa.Decrypt(encryptedBlock, true);
                    decryptedData.AddRange(decryptedBlock);
                }

                string decryptedText = Encoding.UTF8.GetString(decryptedData.ToArray());

                txt_decrypt.Text = decryptedText;              
            }
            catch (CryptographicException ex)
            {
                MessageBox.Show("Loi giai ma: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi: " + ex.Message);
            }
        }
    }
}
