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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            CenterToScreen();
        }
        bool check_Textbox(string host, string port, string sid, string user, string pass)
        {
            if (string.IsNullOrWhiteSpace(host))
            {
                MessageBox.Show("Chưa điền thông tin host");
                txt_host.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(port))
            {
                MessageBox.Show("Chưa điền thông tin port");
                txt_port.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(sid))
            {
                MessageBox.Show("Chưa điền thông tin sid");
                txt_sid.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(user))
            {
                MessageBox.Show("Chưa điền thông tin user");
                txt_user.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Chưa điền thông tin password");
                txt_password.Focus();
                return false;
            }
            return true;
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            //string host = txt_host.Text;
            //string port = txt_port.Text;
            //string sid = txt_sid.Text;
            //string user = txt_user.Text;
            //string pass = txt_password.Text;
            //if (check_Textbox(host, port, sid, user, pass))
            //{
            //    Database.Set_Database(host, port, sid, user, pass);
            //    if (Database.Connect())
            //    {
            //        MessageBox.Show("Đăng nhập thành công");
            //        new Form2().Show();
            //        this.Hide();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Đăng nhập thất bại");
            //    }
            //}

            //string host = txt_host.Text;
            //string port = txt_port.Text;
            //string sid = txt_sid.Text;
            //string user = txt_user.Text;
            //string pass = txt_password.Text;
            //if (check_Textbox(host, port, sid, user, pass))
            //{
            //    Database.Set_Database(host, port, sid, user, pass);
            //    if (Database.Connect())
            //    {
            //        MessageBox.Show("Đăng nhập thành công");
            //        new Form1().Show();
            //        this.Hide();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Đăng nhập thất bại");
            //    }
            //}

            string host = txt_host.Text;
            string port = txt_port.Text;
            string sid = txt_sid.Text;
            string user = txt_user.Text;
            string pass = txt_password.Text;
            if (check_Textbox(host, port, sid, user, pass))
            {
                Database.Set_Database(host, port, sid, user, pass);
                if (Database.Connect())
                {
                    MessageBox.Show("Đăng nhập thành công");
                    new Form3().Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Đăng nhập thất bại");
                }
            }
        }
    }
}
