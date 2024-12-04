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
    public partial class Profile_GUI : Form
    {
        OracleConnection conn;
        profile Proc;
        public Profile_GUI()
        {
            InitializeComponent();
            CenterToScreen();
            conn = Database.Get_Connect();
            Proc = new profile(conn);
            Load_combobox();
        }
        void Load_combobox()
        {
            DataTable read = Proc.Getname_Profile();
            foreach (DataRow r in read.Rows)
            {
                cb1.Items.Add(r[0]);
            }
            cb1.SelectedIndex = 0;
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cb1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = cb1.SelectedIndex;
            string profile = cb1.SelectedItem.ToString();
            dataGridView1.DataSource = Proc.Get_Profile(profile);
        }
    }
}
