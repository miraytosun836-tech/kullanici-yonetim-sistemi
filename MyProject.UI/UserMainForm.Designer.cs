namespace MyProject.UI
{
    partial class UserMainForm
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
            btnCikis = new Button();
            btnParolaDegistir = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(139, 20);
            label1.TabIndex = 0;
            label1.Text = "Kullanıcı Ana Ekranı";
            // 
            // btnCikis
            // 
            btnCikis.Location = new Point(683, 397);
            btnCikis.Name = "btnCikis";
            btnCikis.Size = new Size(94, 29);
            btnCikis.TabIndex = 1;
            btnCikis.Text = "Çıkış";
            btnCikis.UseVisualStyleBackColor = true;
            btnCikis.Click += button1_Click;
            // 
            // btnParolaDegistir
            // 
            btnParolaDegistir.Location = new Point(466, 397);
            btnParolaDegistir.Name = "btnParolaDegistir";
            btnParolaDegistir.Size = new Size(189, 29);
            btnParolaDegistir.TabIndex = 2;
            btnParolaDegistir.Text = "Parola Değiştir";
            btnParolaDegistir.UseVisualStyleBackColor = true;
            btnParolaDegistir.Click += btnParolaDegistir_Click;
            // 
            // UserMainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnParolaDegistir);
            Controls.Add(btnCikis);
            Controls.Add(label1);
            Name = "UserMainForm";
            Text = "Kullanıcı Paneli";
            Load += UserMainForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button btnCikis;
        private Button btnParolaDegistir;
    }
}