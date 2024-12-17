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

namespace DoAn_BMTT_36_BuiHungPhuong
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }

        // Phương thức tạo user mới
  

        private void btn_dangky_Click_1(object sender, EventArgs e)
        {
            string userName = txt_user_dk.Text.Trim();
            string password = txt_password_dk.Text.Trim();
            string confirmPassword = txt_pass_dknhaplai.Text.Trim();

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Kết nối Oracle dưới quyền SYSDBA
                if (Database_sys.ConnectAsSys("sys", "sys"))  // Thay 'sys_password' bằng mật khẩu của SYS
                {
                    using (OracleCommand cmd = Database_sys.SysConn.CreateCommand())
                    {
                        cmd.CommandText = $"CREATE USER {userName} IDENTIFIED BY {password}";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = $"GRANT CREATE SESSION TO {userName}";
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Tạo người dùng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Form1 dn = new Form1();
                        dn.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo người dùng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (Database_sys.SysConn != null && Database_sys.SysConn.State == ConnectionState.Open)
                    Database_sys.SysConn.Close();
            }
        }
    }
}
