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
    public partial class Create_User_GUI : Form
    {
        OracleConnection conn;
        Create_User u;
        public Create_User_GUI()
        {
            InitializeComponent();
            CenterToScreen();
            conn = database.Get_Connect();
            u = new Create_User(conn);
        }

        private void btn_create_Click(object sender, EventArgs e)
        {
            if (txt_username.Text.Equals(""))
            {
                MessageBox.Show("Chưa điền tên đăng nhập!");
                txt_username.Focus();
                return;
            }
            if (txt_password.Text.Equals(""))
            {
                MessageBox.Show("Chưa điền mật khẩu!");
                txt_password.Focus();
                return;
            }
            int kq = u.Pro_CheckUser(txt_username.Text);
            if (kq == 0)
            {
                if (u.Pro_CreateUser(txt_username.Text,txt_password.Text))
                {
                    MessageBox.Show("Tạo tài khoản: " + txt_username.Text + "thành công");
                }
                else
                {
                    MessageBox.Show("Tạo tài khoản: " + txt_username.Text + "thất bại!");
                }
            }
            else if (kq == 1)
            {
                DialogResult res = MessageBox.Show("Bạn có muốn thay đổi mật khẩu User: " + txt_username.Text, "Thông báo", MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes)
                {
                    if (u.Pro_CreateUser(txt_username.Text,txt_password.Text))
                    {
                        MessageBox.Show("Đổi mật khẩu tài khoản: " + txt_username.Text + "thành công");
                        database.Set_database(txt_username.Text, txt_password.Text,"","","");
                        database.Connect();
                    }
                    else
                    {
                        MessageBox.Show("Đổi mật khẩu tài khoản: " + txt_username.Text + "thất bại");
                    }
                }
            }
        }

        private void Create_User_GUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            database.Conn.Close();
        }

        private void txt_username_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_password_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
