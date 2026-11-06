namespace MyProject.UI
{
    partial class ResetPasswordForm
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            txtToken = new TextBox();
            txtNew = new TextBox();
            txtConfirm = new TextBox();
            btnKaydet = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(40, 41);
            label1.Name = "label1";
            label1.Size = new Size(110, 20);
            label1.TabIndex = 0;
            label1.Text = "Sıfırlama Kodu:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(40, 110);
            label2.Name = "label2";
            label2.Size = new Size(84, 20);
            label2.TabIndex = 1;
            label2.Text = "Yeni Parola:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(40, 181);
            label3.Name = "label3";
            label3.Size = new Size(128, 20);
            label3.TabIndex = 2;
            label3.Text = "Yeni Parola Tekrar:";
            // 
            // txtToken
            // 
            txtToken.Location = new Point(187, 34);
            txtToken.Name = "txtToken";
            txtToken.Size = new Size(261, 27);
            txtToken.TabIndex = 3;
            // 
            // txtNew
            // 
            txtNew.Location = new Point(187, 110);
            txtNew.Name = "txtNew";
            txtNew.Size = new Size(261, 27);
            txtNew.TabIndex = 4;
            txtNew.UseSystemPasswordChar = true;
            // 
            // txtConfirm
            // 
            txtConfirm.Location = new Point(187, 178);
            txtConfirm.Name = "txtConfirm";
            txtConfirm.Size = new Size(261, 27);
            txtConfirm.TabIndex = 5;
            txtConfirm.UseSystemPasswordChar = true;
            // 
            // btnKaydet
            // 
            btnKaydet.Location = new Point(245, 248);
            btnKaydet.Name = "btnKaydet";
            btnKaydet.Size = new Size(94, 41);
            btnKaydet.TabIndex = 6;
            btnKaydet.Text = "Kaydet";
            btnKaydet.UseVisualStyleBackColor = true;
            btnKaydet.Click += btnKaydet_Click;
            // 
            // ResetPasswordForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(515, 344);
            Controls.Add(btnKaydet);
            Controls.Add(txtConfirm);
            Controls.Add(txtNew);
            Controls.Add(txtToken);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "ResetPasswordForm";
            Text = "Parolayı Sıfırla";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox txtToken;
        private TextBox txtNew;
        private TextBox txtConfirm;
        private Button btnKaydet;
    }
}