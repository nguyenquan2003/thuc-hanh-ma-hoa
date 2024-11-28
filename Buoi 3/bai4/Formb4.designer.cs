namespace Bai4
{
    partial class Formb4
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
            this.txt_decrypt = new System.Windows.Forms.TextBox();
            this.txt_encrypt = new System.Windows.Forms.TextBox();
            this.txt_plaintext = new System.Windows.Forms.TextBox();
            this.txt_privateKey = new System.Windows.Forms.TextBox();
            this.txt_publicKey = new System.Windows.Forms.TextBox();
            this.btn_decrypt = new System.Windows.Forms.Button();
            this.btn_encrypt = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_OpenPublicKey = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.btn_Generate = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txt_decrypt
            // 
            this.txt_decrypt.Location = new System.Drawing.Point(322, 333);
            this.txt_decrypt.Multiline = true;
            this.txt_decrypt.Name = "txt_decrypt";
            this.txt_decrypt.Size = new System.Drawing.Size(267, 80);
            this.txt_decrypt.TabIndex = 27;
            // 
            // txt_encrypt
            // 
            this.txt_encrypt.Location = new System.Drawing.Point(34, 333);
            this.txt_encrypt.Multiline = true;
            this.txt_encrypt.Name = "txt_encrypt";
            this.txt_encrypt.Size = new System.Drawing.Size(267, 80);
            this.txt_encrypt.TabIndex = 26;
            // 
            // txt_plaintext
            // 
            this.txt_plaintext.Location = new System.Drawing.Point(34, 242);
            this.txt_plaintext.Multiline = true;
            this.txt_plaintext.Name = "txt_plaintext";
            this.txt_plaintext.Size = new System.Drawing.Size(555, 50);
            this.txt_plaintext.TabIndex = 25;
            // 
            // txt_privateKey
            // 
            this.txt_privateKey.Location = new System.Drawing.Point(34, 152);
            this.txt_privateKey.Multiline = true;
            this.txt_privateKey.Name = "txt_privateKey";
            this.txt_privateKey.Size = new System.Drawing.Size(555, 50);
            this.txt_privateKey.TabIndex = 24;
            // 
            // txt_publicKey
            // 
            this.txt_publicKey.Location = new System.Drawing.Point(34, 70);
            this.txt_publicKey.Multiline = true;
            this.txt_publicKey.Name = "txt_publicKey";
            this.txt_publicKey.Size = new System.Drawing.Size(555, 50);
            this.txt_publicKey.TabIndex = 23;
            // 
            // btn_decrypt
            // 
            this.btn_decrypt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_decrypt.Location = new System.Drawing.Point(265, 435);
            this.btn_decrypt.Name = "btn_decrypt";
            this.btn_decrypt.Size = new System.Drawing.Size(83, 23);
            this.btn_decrypt.TabIndex = 22;
            this.btn_decrypt.Text = "Decrypt";
            this.btn_decrypt.UseVisualStyleBackColor = true;
            this.btn_decrypt.Click += new System.EventHandler(this.btn_decrypt_Click);
            // 
            // btn_encrypt
            // 
            this.btn_encrypt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_encrypt.Location = new System.Drawing.Point(162, 435);
            this.btn_encrypt.Name = "btn_encrypt";
            this.btn_encrypt.Size = new System.Drawing.Size(86, 23);
            this.btn_encrypt.TabIndex = 21;
            this.btn_encrypt.Text = "Encrypt";
            this.btn_encrypt.UseVisualStyleBackColor = true;
            this.btn_encrypt.Click += new System.EventHandler(this.btn_encrypt_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(319, 316);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 16);
            this.label6.TabIndex = 19;
            this.label6.Text = "Text Decrypt";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(31, 314);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 16);
            this.label5.TabIndex = 18;
            this.label5.Text = "Text Encrypt";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(31, 226);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 16);
            this.label4.TabIndex = 17;
            this.label4.Text = "PlainText";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(31, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 16);
            this.label3.TabIndex = 16;
            this.label3.Text = "Private Key";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(31, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 16);
            this.label2.TabIndex = 15;
            this.label2.Text = "Public Key";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(99, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(448, 25);
            this.label1.TabIndex = 14;
            this.label1.Text = "BAI TAP THUC HANH 4 - GIAO DIEN RSA";
            // 
            // btn_OpenPublicKey
            // 
            this.btn_OpenPublicKey.Location = new System.Drawing.Point(119, 47);
            this.btn_OpenPublicKey.Name = "btn_OpenPublicKey";
            this.btn_OpenPublicKey.Size = new System.Drawing.Size(113, 23);
            this.btn_OpenPublicKey.TabIndex = 28;
            this.btn_OpenPublicKey.Text = "Open Public Key";
            this.btn_OpenPublicKey.UseVisualStyleBackColor = true;
            this.btn_OpenPublicKey.Click += new System.EventHandler(this.btn_OpenPublicKey_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(119, 129);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(113, 23);
            this.button2.TabIndex = 29;
            this.button2.Text = "Open Private Key";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(119, 219);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(113, 23);
            this.button3.TabIndex = 30;
            this.button3.Text = "Open Plain Text";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(119, 307);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(113, 23);
            this.button4.TabIndex = 31;
            this.button4.Text = "Open Encrypt";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // btn_Generate
            // 
            this.btn_Generate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Generate.Location = new System.Drawing.Point(33, 435);
            this.btn_Generate.Name = "btn_Generate";
            this.btn_Generate.Size = new System.Drawing.Size(111, 23);
            this.btn_Generate.TabIndex = 32;
            this.btn_Generate.Text = "Generate Key";
            this.btn_Generate.UseVisualStyleBackColor = true;
            this.btn_Generate.Click += new System.EventHandler(this.btn_Generate_Click);
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.Location = new System.Drawing.Point(368, 435);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(87, 23);
            this.button6.TabIndex = 33;
            this.button6.Text = "Save Key";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.Location = new System.Drawing.Point(474, 435);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(114, 23);
            this.button7.TabIndex = 34;
            this.button7.Text = "Save Encrypt";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // Formb4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 470);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.btn_Generate);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btn_OpenPublicKey);
            this.Controls.Add(this.txt_decrypt);
            this.Controls.Add(this.txt_encrypt);
            this.Controls.Add(this.txt_plaintext);
            this.Controls.Add(this.txt_privateKey);
            this.Controls.Add(this.txt_publicKey);
            this.Controls.Add(this.btn_decrypt);
            this.Controls.Add(this.btn_encrypt);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Formb4";
            this.Text = "Formb4";
            this.Load += new System.EventHandler(this.Formb4_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_decrypt;
        private System.Windows.Forms.TextBox txt_encrypt;
        private System.Windows.Forms.TextBox txt_plaintext;
        private System.Windows.Forms.TextBox txt_privateKey;
        private System.Windows.Forms.TextBox txt_publicKey;
        private System.Windows.Forms.Button btn_decrypt;
        private System.Windows.Forms.Button btn_encrypt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_OpenPublicKey;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button btn_Generate;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
    }
}

