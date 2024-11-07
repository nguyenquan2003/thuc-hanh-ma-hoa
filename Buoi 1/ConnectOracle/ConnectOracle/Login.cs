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

namespace ConnectOracle
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
            if(host == "")
            {
                MessageBox.Show("Chưa điền thông tin host");
                txt_host.Focus();
                return false;
            }
            else if (port == "")
            {
                MessageBox.Show("Chưa điền thông tin port");
                txt_port.Focus();
                return false;
            }
            else if (sid == "")
            {
                MessageBox.Show("Chưa điền thông tin sid");
                txt_sid.Focus();
                return false;
            }
            else if (user == "")
            {
                MessageBox.Show("Chưa điền thông tin user");
                txt_user.Focus();
                return false;
            }
            else if (pass == "")
            {
                MessageBox.Show("Chưa điền thông tin password");
                txt_pass.Focus();
                return false;
            }
            else
            {
                return false;
            }
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            string host = txt_host.Text;
            string port = txt_port.Text;
            string sid = txt_sid.Text;
            string user = txt_user.Text;
            string pass = txt_pass.Text;

            if (check_Textbox(host, port, sid, user, pass))
            {
                Database.Set_Database(host, port, sid, user, pass);
                if (Database.Connect())
                {
                    OracleConnection c = Database.Get_Connect();
                    MessageBox.Show("Đăng nhập thành công\nServerVersion: " + c.ServerVersion);
                }
                else
                {
                    MessageBox.Show("Đăng nhập thất bại");
                }
            }
        } 
    }
}
