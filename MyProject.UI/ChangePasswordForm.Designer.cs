namespace MyProject.UI
{
    partial class ChangePasswordForm
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
            txtNew = new MaskedTextBox();
            txtConfirm = new MaskedTextBox();
            label1 = new Label();
            label2 = new Label();
            btnKaydet = new Button();
            btnIptal = new Button();
            lblOld = new Label();
            txtOld = new MaskedTextBox();
            SuspendLayout();
            // 
            // txtNew
            // 
            txtNew.Location = new Point(168, 106);
            txtNew.Name = "txtNew";
            txtNew.Size = new Size(262, 27);
            txtNew.TabIndex = 0;
            txtNew.UseSystemPasswordChar = true;
            // 
            // txtConfirm
            // 
            txtConfirm.Location = new Point(168, 178);
            txtConfirm.Name = "txtConfirm";
            txtConfirm.Size = new Size(262, 27);
            txtConfirm.TabIndex = 1;
            txtConfirm.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 106);
            label1.Name = "label1";
            label1.Size = new Size(84, 20);
            label1.TabIndex = 2;
            label1.Text = "Yeni Parola:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 185);
            label2.Name = "label2";
            label2.Size = new Size(128, 20);
            label2.TabIndex = 3;
            label2.Text = "Yeni Parola Tekrar:";
            // 
            // btnKaydet
            // 
            btnKaydet.Location = new Point(82, 261);
            btnKaydet.Name = "btnKaydet";
            btnKaydet.Size = new Size(114, 37);
            btnKaydet.TabIndex = 4;
            btnKaydet.Text = "Kaydet";
            btnKaydet.UseVisualStyleBackColor = true;
            btnKaydet.Click += btnKaydet_Click;
            // 
            // btnIptal
            // 
            btnIptal.Location = new Point(279, 261);
            btnIptal.Name = "btnIptal";
            btnIptal.Size = new Size(114, 37);
            btnIptal.TabIndex = 5;
            btnIptal.Text = "İptal";
            btnIptal.UseVisualStyleBackColor = true;
            btnIptal.Click += btnIptal_Click;
            // 
            // lblOld
            // 
            lblOld.AutoSize = true;
            lblOld.Location = new Point(14, 44);
            lblOld.Name = "lblOld";
            lblOld.Size = new Size(82, 20);
            lblOld.TabIndex = 6;
            lblOld.Text = "Eski Parola:";
            // 
            // txtOld
            // 
            txtOld.Location = new Point(168, 37);
            txtOld.Name = "txtOld";
            txtOld.Size = new Size(262, 27);
            txtOld.TabIndex = 7;
            txtOld.UseSystemPasswordChar = true;
            // 
            // ChangePasswordForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(484, 327);
            Controls.Add(txtOld);
            Controls.Add(lblOld);
            Controls.Add(btnIptal);
            Controls.Add(btnKaydet);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtConfirm);
            Controls.Add(txtNew);
            Name = "ChangePasswordForm";
            Text = "Parola Değiştir";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MaskedTextBox txtNew;
        private MaskedTextBox txtConfirm;
        private Label label1;
        private Label label2;
        private Button btnKaydet;
        private Button btnIptal;
        private Label lblOld;
        private MaskedTextBox txtOld;
    }
}