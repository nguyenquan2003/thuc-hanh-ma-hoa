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
    public partial class ChiTietHoaDon : Form
    {
        public ChiTietHoaDon()
        {
            InitializeComponent();
            LoadDataChiTietHoaDon();
            list_chitiethoadon.SelectedIndexChanged += list_hoadon_SelectedIndexChanged;

        }

        public void LoadDataChiTietHoaDon()
        {
            try
            {
                using (OracleConnection conn = Database.Get_Connection())
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }


                    using (OracleCommand cmd = new OracleCommand("BEGIN GetAllChiTietHoaDons(:data); END;", conn))
                    {
                        cmd.CommandType = CommandType.Text;


                        OracleParameter outputParam = new OracleParameter("data", OracleDbType.RefCursor);
                        outputParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(outputParam);


                        cmd.ExecuteNonQuery();


                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            list_chitiethoadon.Items.Clear();

                            while (reader.Read())
                            {
                                ListViewItem item = new ListViewItem(reader["MaHoaDon"].ToString());

                                item.SubItems.Add(reader["MaSach"].ToString());
                                item.SubItems.Add(reader["SoLuong"].ToString());
                                item.SubItems.Add(reader["DonGia"].ToString());
                                list_chitiethoadon.Items.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu từ bảng chi tiết hóa đơn: " + ex.Message);
            }
        }
        private void list_hoadon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list_chitiethoadon.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = list_chitiethoadon.SelectedItems[0];

                txt_mahd.Text = selectedItem.SubItems[0].Text;
                txt_masach.Text = selectedItem.SubItems[1].Text;
                txt_soluong.Text = selectedItem.SubItems[2].Text;

                // Chuyển đổi Đơn Giá từ chuỗi sang số nguyên
                int donGia = int.Parse(selectedItem.SubItems[3].Text);
                txt_dongia.Text = donGia.ToString();
            }

        }
    }
}
