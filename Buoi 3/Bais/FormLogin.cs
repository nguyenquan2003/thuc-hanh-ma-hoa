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
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
            CenterToScreen();
        }

        bool Check_Textbox(string host, string port, string sid, string user, string pass)
        {
            if (host == "")
            {
                MessageBox.Show("Chưa điền thông tin Host");
                txt_host.Focus();
                return false;
            }
            if (port == "")
            {
                MessageBox.Show("Chưa điền thông tin Port");
                txt_port.Focus();
                return false;
            }
            if (sid == "")
            {
                MessageBox.Show("Chưa điền thông tin Sid");
                txt_sid.Focus();
                return false;
            }
            if (user == "")
            {
                MessageBox.Show("Chưa điền thông tin User");
                txt_user.Focus();
                return false;
            }
            if (pass == "")
            {
                MessageBox.Show("Chưa điền thông tin Password");
                txt_password.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            string host = txt_host.Text;
            string port = txt_port.Text;
            string sid = txt_sid.Text;
            string user = txt_user.Text;
            string pass = txt_password.Text;

            if (Check_Textbox(host, port, sid, user, pass))
            {
                try
                {
                    Database.Set_Database(host, port, sid, user, pass);
                    if (Database.Connect())
                    {
                        OracleConnection c = Database.Get_Connect();
                        MessageBox.Show("Đăng nhập thành công");
                        new RSA_GUI().Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Đăng nhập thất bại");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

    }
}
