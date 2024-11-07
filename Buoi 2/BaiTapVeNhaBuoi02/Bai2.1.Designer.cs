namespace BaiTapVeNhaBuoi02
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbb_CSDL = new System.Windows.Forms.ComboBox();
            this.txt_KetQua = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_MaHoa = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbb_CSDL
            // 
            this.cbb_CSDL.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F);
            this.cbb_CSDL.FormattingEnabled = true;
            this.cbb_CSDL.Items.AddRange(new object[] {
            "0977318754",
            "0223416754",
            "0988076099",
            "0913997979",
            "0974321754",
            "0977896754",
            "0654396754",
            "0977893484",
            "0984578905",
            "0977753254"});
            this.cbb_CSDL.Location = new System.Drawing.Point(388, 156);
            this.cbb_CSDL.Name = "cbb_CSDL";
            this.cbb_CSDL.Size = new System.Drawing.Size(437, 37);
            this.cbb_CSDL.TabIndex = 0;
            // 
            // txt_KetQua
            // 
            this.txt_KetQua.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F);
            this.txt_KetQua.Location = new System.Drawing.Point(388, 266);
            this.txt_KetQua.Name = "txt_KetQua";
            this.txt_KetQua.Size = new System.Drawing.Size(437, 34);
            this.txt_KetQua.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(206, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(772, 51);
            this.label1.TabIndex = 2;
            this.label1.Text = "Ứng dụng mã hóa thay thế phép cộng";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(44, 164);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(278, 29);
            this.label2.TabIndex = 3;
            this.label2.Text = "Số Điện Thoại Giáo Viên";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(133, 271);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(189, 29);
            this.label3.TabIndex = 4;
            this.label3.Text = "Kết Quả Mã Hóa";
            // 
            // btn_MaHoa
            // 
            this.btn_MaHoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F);
            this.btn_MaHoa.Location = new System.Drawing.Point(423, 377);
            this.btn_MaHoa.Name = "btn_MaHoa";
            this.btn_MaHoa.Size = new System.Drawing.Size(234, 72);
            this.btn_MaHoa.TabIndex = 5;
            this.btn_MaHoa.Text = "Mã Hóa";
            this.btn_MaHoa.UseVisualStyleBackColor = true;
            this.btn_MaHoa.Click += new System.EventHandler(this.btn_MaHoa_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1136, 666);
            this.Controls.Add(this.btn_MaHoa);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_KetQua);
            this.Controls.Add(this.cbb_CSDL);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbb_CSDL;
        private System.Windows.Forms.TextBox txt_KetQua;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_MaHoa;
    }
}