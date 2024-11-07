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
    public partial class Form2 : Form
    {
        private OracleConnection conn;
        private MaHoaPhepNhan co;
        public Form2()
        {
            InitializeComponent();
            CenterToScreen();
            conn = Database.Get_Connect();
            co = new MaHoaPhepNhan(conn);
            cb_usefunction.Checked = true;
        }

        private void btn_Encrypt_Click(object sender, EventArgs e)
        {
            if (!txt_PlainText.Text.Equals(""))
            {
                string rs;
                string plaintext = txt_PlainText.Text;
                int key = (int)nb_keyencrypt.Value;
                if (cb_usefunction.Checked)
                {
                    rs = co.EncryptMultiplicative_Func(plaintext, key);
                }
                else
                {
                    rs = co.EncryptMultiplicative_Proc(plaintext, key);
                }
                txt_Encrypted.Text = rs;
            }
            else
            {
                MessageBox.Show("Vui lòng nhập thông điệp cần mã hóa!");
                txt_PlainText.Focus();
            }
        }

        private void btn_Decrypt_Click(object sender, EventArgs e)
        {
            if (!txt_Encrypted.Text.Equals(""))
            {
                string rs;
                string encryptText = txt_Encrypted.Text;
                int key = (int)nb_keyencrypt.Value;
                if (cb_usefunction.Checked)
                {
                    rs = co.DecryptMultiplicative_Func(encryptText, key);
                }
                else
                {
                    rs = co.DecryptMultiplicative_Proc(encryptText, key);
                }
                txt_Decrypted.Text = rs;
            }
            else
            {
                MessageBox.Show("Vui lòng nhập thông điệp cần giải mã!");
                txt_Encrypted.Focus();
            }
        }

        private void cb_usefunction_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_usefunction.Checked)
            {
                lb_pro_fun.Text = "Sử dụng hàm (Function)";
            }
            else
            {
                lb_pro_fun.Text = "Sử dụng thủ tục (Procedure)";
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
