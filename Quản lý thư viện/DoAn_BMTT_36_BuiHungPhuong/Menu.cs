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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            


            Sach sach = new Sach();
            sach.ShowDialog();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            KhachHang kh = new KhachHang();
            kh.ShowDialog();
        }

        private void phânQuyềnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PhanQuyen pq = new PhanQuyen();
            pq.ShowDialog();
        }

        private void LogoutUser()
        {
            try
            {
                using (OracleConnection conn = Database.Get_Connection())
                {
                    using (OracleCommand cmd = new OracleCommand("kill_session_by_user", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = Database.User;

                        // Thực thi thủ tục kill_session_by_user
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Đăng xuất thành công và phiên đã bị hủy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    // Thực thi câu lệnh ALTER PROCEDURE
                    using (OracleCommand alterCmd = new OracleCommand("ALTER PROCEDURE kill_session_by_user COMPILE", conn))
                    {
                        alterCmd.CommandType = CommandType.Text;
                        alterCmd.ExecuteNonQuery();
                        MessageBox.Show("Thủ tục kill_session_by_user đã được biên dịch lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                // Đóng các form khác trừ Form1
                foreach (Form form in Application.OpenForms.Cast<Form>().ToList())
                {
                    if (form.Name != "Form1")
                    {
                        form.Close();
                    }
                }

                // Hiển thị lại form đăng nhập
                Form1 loginForm = new Form1();
                loginForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đăng xuất: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void profileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogoutUser();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            HoaDon pq = new HoaDon();
            pq.ShowDialog();
        }

        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NhanVien nv = new NhanVien();
            nv.ShowDialog();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            TacGia nv = new TacGia();
            nv.ShowDialog();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            ChiTietHoaDon ct = new ChiTietHoaDon();
            ct.ShowDialog();
        }

        private void quyềnTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuyenTable nv = new QuyenTable();
            nv.ShowDialog();
        }
    }
}
