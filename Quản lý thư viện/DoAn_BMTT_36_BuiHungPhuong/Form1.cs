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

namespace DoAn_BMTT_36_BuiHungPhuong
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            txt_password.UseSystemPasswordChar = true;
            if (Properties.Settings.Default.Username != "")
            {
                txt_user.Text = Properties.Settings.Default.Username;
                txt_password.Text = Properties.Settings.Default.Password;
                txt_host.Text = Properties.Settings.Default.Host;
                txt_port.Text = Properties.Settings.Default.Port;
                txt_sid.Text = Properties.Settings.Default.ServiceName;
                check_remeber.Checked = true; // Đánh dấu checkbox nếu đã ghi nhớ
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        bool check_textbox(string user, string password)
        {
            if(user == "")
            {
                MessageBox.Show("Vui lòng điền thông tin user");
                txt_user.Focus();
                return false;
            }
            else if (password == "")
            {
                MessageBox.Show("Vui lòng điền thông tin password");
                txt_password.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }

       
        private void btn_dangnhap_Click(object sender, EventArgs e)
        {
            string user = txt_user.Text;
            string password = txt_password.Text;
            string host = txt_host.Text;
            string port = txt_port.Text;
            string servicename = txt_sid.Text;

            if (check_textbox(user, password))
            {
                bool loginSuccess = false;
                OracleConnection connection = null;

                // Kiểm tra nếu user là "sys" thì dùng kết nối SYSDBA
                if (user.ToLower() == "sys")
                {
                    loginSuccess = Database_sys.ConnectAsSys(user, password);
                    if (loginSuccess)
                    {
                        connection = Database_sys.SysConn;
                        MessageBox.Show("Đăng nhập SYS thành công");
                    }
                    else
                    {
                        MessageBox.Show("Đăng nhập SYS thất bại");
                        return;
                    }
                }
                else
                {
                    // Sử dụng kết nối bình thường nếu không phải "sys"
                    Database.Set_Database(user, password, host, port, servicename);
                    loginSuccess = Database.Connect();

                    if (loginSuccess)
                    {
                        connection = Database.Get_Connection();
                        MessageBox.Show("Đăng nhập thành công");
                    }
                    else
                    {
                        MessageBox.Show("Đăng nhập thất bại");
                        return;
                    }
                }

                // Tạo sessionId mới
                string sessionId = Guid.NewGuid().ToString();
                SessionManager.SaveSession(user, sessionId);

                // Lưu thông tin đăng nhập nếu checkbox được chọn
                if (check_remeber.Checked)
                {
                    Properties.Settings.Default.Username = user;
                    Properties.Settings.Default.Password = password;
                    Properties.Settings.Default.Host = host;
                    Properties.Settings.Default.Port = port;                
                    Properties.Settings.Default.ServiceName = servicename;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    Properties.Settings.Default.Username = "";
                    Properties.Settings.Default.Password = "";
                    Properties.Settings.Default.Host = "";
                    Properties.Settings.Default.Port = "";
                    Properties.Settings.Default.ServiceName = "";
                    Properties.Settings.Default.Save();
                }

                // Mở form Menu
                Menu menu = new Menu();
                menu.ShowDialog();
                this.Hide();

            }
            else
                {
                    MessageBox.Show("Đăng nhập thất bại ");
                }
            
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Register dk = new Register();
            dk.ShowDialog();
            this.Close();

        }
    }
}
