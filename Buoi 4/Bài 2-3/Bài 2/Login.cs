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
namespace Bài_2
{
    public partial class Login : Form
    {
        public Login()
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
            else if (port == "")
            {
                MessageBox.Show("Chưa điền thông tin Port");
                txt_port.Focus();
                return false;
            }
            else if (sid == "")
            {
                MessageBox.Show("Chưa điền thông tin Sid");
                txt_sid.Focus();
                return false;
            }
            else if (user == "")
            {
                MessageBox.Show("Chưa điền thông tin User");
                txt_user.Focus();
                return false;
            }
            else if (pass == "")
            {
                MessageBox.Show("Chưa điền thông tin Host");
                txt_pass.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }
        void Check_Status(string user)
        {
            string status = Database.Get_Status(user);

            if (status.Equals("LOCKED") || status.Equals("LOCKED(TIMED)"))
            {
                MessageBox.Show("Tài khoản bị khóa");
            }
            else if (status.Equals("EXEXPIRED(GRACE)"))
            {
                MessageBox.Show("Tài khoản sắp hết hạn");
            }

            else if (status.Equals("EXPIRED & LOCKED(TIMED)"))
            {
                MessageBox.Show("Tài khoản bị khóa do hết bạn");
            }

            else if (status.Equals("EXPIRED"))
            {
                MessageBox.Show("Tài khoản hết hạn");
            }

            else if (status.Equals(" "))
            {
                MessageBox.Show("Tài khoản không tồn tại");
            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại!\nXem lại thông tin đăng nhập: UserName, Password");
            }
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            string host = txt_host.Text;
            string port = txt_port.Text;
            string sid = txt_sid.Text;
            string user = txt_user.Text;
            string pass = txt_pass.Text;

            if (Check_Textbox(host, port, sid, user, pass))
            {
                Database.Set_database(host, port, sid, user, pass);
                if (Database.Connect())
                {
                    OracleConnection c = Database.Get_Connect();
                    //MessageBox.Show("Đăng nhập thành công\n SeverVersion: " + c.ServerVersion);
                    //new Profile_GUI().Show();
                    //this.Hide();
                    MessageBox.Show("Đăng nhập thành công");
                }
                else
                {
                    //MessageBox.Show("Đăng nhập thất bại");
                    Check_Status(user);
                    return;
                }
            }
        }

        private void btn_CreateUser_Click(object sender, EventArgs e)
        {
            string host = txt_host.Text;
            string port = txt_port.Text;
            string sid = txt_sid.Text;
            Database.Set_database(host, port, sid, "", "");
            if (Database.ConnectSys())
            {
                new Create_User_GUI().Show();
            }
        }
    }
}
