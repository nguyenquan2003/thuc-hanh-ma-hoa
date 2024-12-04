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
namespace Bai_3
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
        private void btn_createuser_Click(object sender, EventArgs e)
        {
            string host = txt_host.Text;
            string port = txt_port.Text;
            string sid = txt_sid.Text;
            database.Set_database(host, port, sid, "", "");
            if (database.Connect())
            {
                new Create_User_GUI().Show();
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
                database.Set_database(host, port, sid, user, pass);
                if (database.Connect())
                {
                    OracleConnection c = database.Get_Connect();
                    MessageBox.Show("Đăng nhập thành công \n severVersion: " + c.ServerVersion);
                }
                else
                {
                    MessageBox.Show("Đăng nhập thất bại");
                }
            }
        }
    }
}
