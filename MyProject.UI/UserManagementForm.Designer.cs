namespace MyProject.UI
{
    partial class UserManagementForm
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
            dgvUsers = new DataGridView();
            btnEkle = new Button();
            btnDurum = new Button();
            label1 = new Label();
            cmbYeniRol = new ComboBox();
            btnRolAta = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            SuspendLayout();
            // 
            // dgvUsers
            // 
            dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsers.Location = new Point(183, 22);
            dgvUsers.Name = "dgvUsers";
            dgvUsers.RowHeadersWidth = 51;
            dgvUsers.Size = new Size(605, 329);
            dgvUsers.TabIndex = 0;
            // 
            // btnEkle
            // 
            btnEkle.Location = new Point(624, 402);
            btnEkle.Name = "btnEkle";
            btnEkle.Size = new Size(145, 36);
            btnEkle.TabIndex = 1;
            btnEkle.Text = "Kullanıcı Ekle";
            btnEkle.UseVisualStyleBackColor = true;
            btnEkle.Click += btnEkle_Click;
            // 
            // btnDurum
            // 
            btnDurum.Location = new Point(469, 402);
            btnDurum.Name = "btnDurum";
            btnDurum.Size = new Size(141, 36);
            btnDurum.TabIndex = 2;
            btnDurum.Text = "Aktif / Pasif";
            btnDurum.UseVisualStyleBackColor = true;
            btnDurum.Click += btnDurum_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(48, 22);
            label1.Name = "label1";
            label1.Size = new Size(62, 20);
            label1.TabIndex = 3;
            label1.Text = "Yeni Rol";
            // 
            // cmbYeniRol
            // 
            cmbYeniRol.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbYeniRol.FormattingEnabled = true;
            cmbYeniRol.Location = new Point(12, 62);
            cmbYeniRol.Name = "cmbYeniRol";
            cmbYeniRol.Size = new Size(151, 28);
            cmbYeniRol.TabIndex = 4;
            // 
            // btnRolAta
            // 
            btnRolAta.Location = new Point(31, 107);
            btnRolAta.Name = "btnRolAta";
            btnRolAta.Size = new Size(105, 40);
            btnRolAta.TabIndex = 5;
            btnRolAta.Text = "Rolü Kaydet";
            btnRolAta.UseVisualStyleBackColor = true;
            btnRolAta.Click += btnRolAta_Click;
            // 
            // UserManagementForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnRolAta);
            Controls.Add(cmbYeniRol);
            Controls.Add(label1);
            Controls.Add(btnDurum);
            Controls.Add(btnEkle);
            Controls.Add(dgvUsers);
            Name = "UserManagementForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Kullanıcı Yönetimi";
            Load += UserManagementForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvUsers;
        private Button btnEkle;
        private Button btnDurum;
        private Label label1;
        private ComboBox cmbYeniRol;
        private Button btnRolAta;
    }
}