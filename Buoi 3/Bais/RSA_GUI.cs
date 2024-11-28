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

namespace Bai3
{
    public partial class RSA_GUI : Form
    {
        OracleConnection conn;
        RSAOracle RSA;

        public RSA_GUI()
        {
            InitializeComponent();
            CenterToParent();
            conn = Database.Get_Connect();
            RSA = new RSAOracle(conn);
        }

        
        private void btn_generate_Click(object sender, EventArgs e)
        {
            try
            {
                string key = RSA.generateKey(1024);

                string[] keyPair = key.Split(new string[] {
                    "****publicKey start*****",
                    "****publicKey end**** ****privateKey start****",
                    "****privateKey end**** do not copy asteric(*) ****"}, StringSplitOptions.RemoveEmptyEntries);
                txt_publickey.Text = keyPair[0].Trim();
                txt_privatekey.Text = keyPair[1].Trim();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_encrypt_Click(object sender, EventArgs e)
        {
            try
            {
                string plainText = txt_plaintext.Text;
                string pubKey = txt_publickey.Text;

                string encrypted= RSA.encrypt(plainText, pubKey);

                txt_encrypt.Text = encrypted;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_decrypt_Click(object sender, EventArgs e)
        {
            try
            {
                string encrypted = txt_encrypt.Text;
                string priKey = txt_privatekey.Text;

                string decrypted = RSA.decrypt(encrypted, priKey);

                txt_decrypt.Text = decrypted;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
