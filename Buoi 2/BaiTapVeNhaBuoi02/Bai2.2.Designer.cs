namespace BaiTapVeNhaBuoi02
{
    partial class Form2
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lb_pro_fun = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_PlainText = new System.Windows.Forms.TextBox();
            this.nb_keyencrypt = new System.Windows.Forms.NumericUpDown();
            this.btn_Encrypt = new System.Windows.Forms.Button();
            this.btn_Decrypt = new System.Windows.Forms.Button();
            this.cb_usefunction = new System.Windows.Forms.CheckBox();
            this.txt_Encrypted = new System.Windows.Forms.TextBox();
            this.txt_Decrypted = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.nb_keyencrypt)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(229, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(469, 51);
            this.label1.TabIndex = 0;
            this.label1.Text = "MÃ HÓA PHÉP NHÂN";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F);
            this.label2.Location = new System.Drawing.Point(74, 232);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 29);
            this.label2.TabIndex = 1;
            this.label2.Text = "Key Encrypt";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F);
            this.label3.Location = new System.Drawing.Point(74, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 29);
            this.label3.TabIndex = 2;
            this.label3.Text = "Plain Text";
            // 
            // lb_pro_fun
            // 
            this.lb_pro_fun.AutoSize = true;
            this.lb_pro_fun.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F);
            this.lb_pro_fun.Location = new System.Drawing.Point(74, 432);
            this.lb_pro_fun.Name = "lb_pro_fun";
            this.lb_pro_fun.Size = new System.Drawing.Size(312, 29);
            this.lb_pro_fun.TabIndex = 3;
            this.lb_pro_fun.Text = "Sử dụng thủ tục (Procedure)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F);
            this.label5.Location = new System.Drawing.Point(74, 503);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 29);
            this.label5.TabIndex = 4;
            this.label5.Text = "Encrypted";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F);
            this.label6.Location = new System.Drawing.Point(74, 624);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(123, 29);
            this.label6.TabIndex = 5;
            this.label6.Text = "Decrypted";
            // 
            // txt_PlainText
            // 
            this.txt_PlainText.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txt_PlainText.Location = new System.Drawing.Point(79, 135);
            this.txt_PlainText.Multiline = true;
            this.txt_PlainText.Name = "txt_PlainText";
            this.txt_PlainText.Size = new System.Drawing.Size(815, 84);
            this.txt_PlainText.TabIndex = 6;
            // 
            // nb_keyencrypt
            // 
            this.nb_keyencrypt.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.nb_keyencrypt.Location = new System.Drawing.Point(79, 274);
            this.nb_keyencrypt.Name = "nb_keyencrypt";
            this.nb_keyencrypt.Size = new System.Drawing.Size(815, 34);
            this.nb_keyencrypt.TabIndex = 7;
            // 
            // btn_Encrypt
            // 
            this.btn_Encrypt.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F);
            this.btn_Encrypt.Location = new System.Drawing.Point(79, 331);
            this.btn_Encrypt.Name = "btn_Encrypt";
            this.btn_Encrypt.Size = new System.Drawing.Size(369, 61);
            this.btn_Encrypt.TabIndex = 8;
            this.btn_Encrypt.Text = "Encrypt";
            this.btn_Encrypt.UseVisualStyleBackColor = true;
            this.btn_Encrypt.Click += new System.EventHandler(this.btn_Encrypt_Click);
            // 
            // btn_Decrypt
            // 
            this.btn_Decrypt.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F);
            this.btn_Decrypt.Location = new System.Drawing.Point(549, 331);
            this.btn_Decrypt.Name = "btn_Decrypt";
            this.btn_Decrypt.Size = new System.Drawing.Size(345, 61);
            this.btn_Decrypt.TabIndex = 9;
            this.btn_Decrypt.Text = "Decrypt";
            this.btn_Decrypt.UseVisualStyleBackColor = true;
            this.btn_Decrypt.Click += new System.EventHandler(this.btn_Decrypt_Click);
            // 
            // cb_usefunction
            // 
            this.cb_usefunction.AutoSize = true;
            this.cb_usefunction.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cb_usefunction.Location = new System.Drawing.Point(592, 428);
            this.cb_usefunction.Name = "cb_usefunction";
            this.cb_usefunction.Size = new System.Drawing.Size(176, 33);
            this.cb_usefunction.TabIndex = 10;
            this.cb_usefunction.Text = "Use Function";
            this.cb_usefunction.UseVisualStyleBackColor = true;
            this.cb_usefunction.CheckedChanged += new System.EventHandler(this.cb_usefunction_CheckedChanged);
            // 
            // txt_Encrypted
            // 
            this.txt_Encrypted.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txt_Encrypted.Location = new System.Drawing.Point(79, 535);
            this.txt_Encrypted.Multiline = true;
            this.txt_Encrypted.Name = "txt_Encrypted";
            this.txt_Encrypted.Size = new System.Drawing.Size(815, 84);
            this.txt_Encrypted.TabIndex = 11;
            // 
            // txt_Decrypted
            // 
            this.txt_Decrypted.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txt_Decrypted.Location = new System.Drawing.Point(79, 669);
            this.txt_Decrypted.Multiline = true;
            this.txt_Decrypted.Name = "txt_Decrypted";
            this.txt_Decrypted.Size = new System.Drawing.Size(815, 84);
            this.txt_Decrypted.TabIndex = 12;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 791);
            this.Controls.Add(this.txt_Decrypted);
            this.Controls.Add(this.txt_Encrypted);
            this.Controls.Add(this.cb_usefunction);
            this.Controls.Add(this.btn_Decrypt);
            this.Controls.Add(this.btn_Encrypt);
            this.Controls.Add(this.nb_keyencrypt);
            this.Controls.Add(this.txt_PlainText);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lb_pro_fun);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nb_keyencrypt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lb_pro_fun;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_PlainText;
        private System.Windows.Forms.NumericUpDown nb_keyencrypt;
        private System.Windows.Forms.Button btn_Encrypt;
        private System.Windows.Forms.Button btn_Decrypt;
        private System.Windows.Forms.CheckBox cb_usefunction;
        private System.Windows.Forms.TextBox txt_Encrypted;
        private System.Windows.Forms.TextBox txt_Decrypted;
    }
}