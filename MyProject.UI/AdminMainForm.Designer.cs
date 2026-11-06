namespace MyProject.UI
{
    partial class AdminMainForm
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
            lblBaslik = new Label();
            btnCikis = new Button();
            btnKullaniciYonetimi = new Button();
            SuspendLayout();
            // 
            // lblBaslik
            // 
            lblBaslik.AutoSize = true;
            lblBaslik.Location = new Point(12, 22);
            lblBaslik.Name = "lblBaslik";
            lblBaslik.Size = new Size(135, 20);
            lblBaslik.TabIndex = 0;
            lblBaslik.Text = "Yönetici Ana Ekranı";
            // 
            // btnCikis
            // 
            btnCikis.Location = new Point(684, 406);
            btnCikis.Name = "btnCikis";
            btnCikis.Size = new Size(94, 32);
            btnCikis.TabIndex = 1;
            btnCikis.Text = "Çıkış";
            btnCikis.UseVisualStyleBackColor = true;
            btnCikis.Click += btnCikis_Click;
            // 
            // btnKullaniciYonetimi
            // 
            btnKullaniciYonetimi.Location = new Point(514, 409);
            btnKullaniciYonetimi.Name = "btnKullaniciYonetimi";
            btnKullaniciYonetimi.Size = new Size(153, 29);
            btnKullaniciYonetimi.TabIndex = 2;
            btnKullaniciYonetimi.Text = "Kullanıcı Yönetimi";
            btnKullaniciYonetimi.UseVisualStyleBackColor = true;
            btnKullaniciYonetimi.Click += btnKullaniciYonetimi_Click;
            // 
            // AdminMainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnKullaniciYonetimi);
            Controls.Add(btnCikis);
            Controls.Add(lblBaslik);
            Name = "AdminMainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Admin Paneli";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblBaslik;
        private Button btnCikis;
        private Button btnKullaniciYonetimi;
    }
}