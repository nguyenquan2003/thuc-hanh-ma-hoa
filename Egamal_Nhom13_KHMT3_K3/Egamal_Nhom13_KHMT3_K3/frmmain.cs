using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Egamal_Nhom13_KHMT3_K3
{
    public partial class frmmain : Form
    {
        public frmmain()
        {
            InitializeComponent();
        }

        // Biến cục bộ
        private int p, a, x, k, y, c1, c2, K, banro, banma, K2, m, tk;

        // Hàm lấy mod  vd m^e mod n
        #region "Hàm lấy mod"
        public int mod(int m, int e, int n)
        {


            //Sử dụng thuật toán "bình phương nhân"
            //Chuyển e sang hệ nhị phân
            int[] a = new int[100];
            int k = 0;
            do
            {
                a[k] = e % 2;
                k++;
                e = e / 2;
            }
            while (e != 0);
            //Quá trình lấy dư
            int kq = 1;
            for (int i = k - 1; i >= 0; i--)
            {
                kq = (kq * kq) % n;
                if (a[i] == 1)
                    kq = (kq * m) % n;
            }
            return kq;



            /* cách khác
             * int kq = 1;
             * while ( e > 0)
             * {
             *     if((e & 1) == 1)
             *     {
             *         kq = (kq + m)%n;
             *     }
             *     e = e >> 1;
             *     m = (m * m) % n;
             * }
             * return kq
             * */
        }
        #endregion

        // Hàm mã  hóa Elgamal
        private void MaHoa()
        {
            tk = int.Parse(txtbanro.Text);
            k = int.Parse(txtk.Text);
            K = mod(y, k, p);
            c1 = mod(a, k, p);
            c2 = mod((K * int.Parse(txtbanro.Text)), 1, p);
            //   c2 = (k * int.Parse(txtbanro.Text)) / p;
            txtbanma.Text = c1.ToString() + " - " + c2.ToString();
            txtbanma1.Text = c1.ToString() + " - " + c2.ToString();
        }
        // Hàm giải mã Elgamal
       private void Giaima()
        {
            K2 = mod(c1, x, p);
            int tg =(int) (c2 * Math.Pow(K2, -1));
            m = c2 * mod(tg,1, p);
            txtbanro1.Text = tk.ToString();
        }
        private void cmd_taokhoa_Click(object sender, EventArgs e)
        {
            try{
                if (txtp.Text != "" && txta.Text != "" && txtx.Text != "" && txtk.Text!="")
                {
                        p = int.Parse(txtp.Text);
                        a = int.Parse(txta.Text);
                        x = int.Parse(txtx.Text);
                        k = int.Parse(txtk.Text);
                        y = mod(a, x, p);
                        txtcongkhai.Text = p.ToString() + " - " + a.ToString() + " - " + y.ToString();
                        txtcongkhai1.Text = p.ToString() + " - " + a.ToString() + " - " + y.ToString();
                        txtbimat.Text = x.ToString();
                        txtbimat1.Text = x.ToString();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }

        private void cmd_lamlai_Click(object sender, EventArgs e)
        {
            txtp.Text = "";
            txta.Text = "";
            txtx.Text = "";
            txtk.Text = "";
            txtcongkhai.Text = "";
            txtbimat.Text = "";
            txtbanro.Text = "";
            txtbanro1.Text = "";
            txtbanma.Text = "";
            txtbanma1.Text = "";
            txtbimat1.Text = "";
            txtp.Focus();

        }

        private void cmd_tudong_Click(object sender, EventArgs e)
        {
            if(txtp.Text!="" && txta.Text !="" && txtx.Text !="")
            {
                Random rd=new Random();
                txtk.Text = rd.Next(5,int.Parse(txtp.Text)).ToString();
            }
            else
            {
                MessageBox.Show("Làm ơn nhập đầy đủ đầu vào p,a,x","Thông báo");
            }
        }

        private void cmd_mahoa_Click(object sender, EventArgs e)
        {
            if(txtbanro.Text!=""){
            MaHoa();
            }
            else
            {
                MessageBox.Show("Làm ơn nhập bản rõ", "Thông báo");
            }
        }

        private void cmd_giaima_Click(object sender, EventArgs e)
        {
            Giaima();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabControl1.SelectedIndex==1)
            {
                txtp.Focus();
            }
        }
    }
}
