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
    public partial class TacGia : Form
    {
        public TacGia()
        {
            InitializeComponent();
            LoadDataTacGia();
            list_tacgia.SelectedIndexChanged += list_tacgia_SelectedIndexChanged;
           
        }
        public void LoadDataTacGia()
        {
            try
            {
                using (OracleConnection conn = Database.Get_Connection())
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }


                    using (OracleCommand cmd = new OracleCommand("BEGIN GetAllTacGias(:data); END;", conn))
                    {
                        cmd.CommandType = CommandType.Text;


                        OracleParameter outputParam = new OracleParameter("data", OracleDbType.RefCursor);
                        outputParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(outputParam);


                        cmd.ExecuteNonQuery();


                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            list_tacgia.Items.Clear();

                            while (reader.Read())
                            {
                                ListViewItem item = new ListViewItem(reader["MaTacGia"].ToString());

                                item.SubItems.Add(reader["TenTacGia"].ToString());
                                item.SubItems.Add(reader["QuocTich"].ToString());


                                list_tacgia.Items.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu từ bảng tác giả: " + ex.Message);
            }
        }


        private void list_tacgia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list_tacgia.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = list_tacgia.SelectedItems[0];
                txt_tacgia.Text = selectedItem.SubItems[0].Text;

                // Giải mã khi hiển thị
                txt_tentg.Text = selectedItem.SubItems[1].Text;
                txt_quoctich.Text = selectedItem.SubItems[2].Text;

            }
        }
    }
}
