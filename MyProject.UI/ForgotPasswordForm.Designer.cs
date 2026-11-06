namespace MyProject.UI
{
    partial class ForgotPasswordForm
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
            txtEmail = new TextBox();
            btnGonder = new Button();
            btnSifirla = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(40, 56);
            label1.Name = "label1";
            label1.Size = new Size(63, 20);
            label1.TabIndex = 0;
            label1.Text = "E-posta:";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(109, 53);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(239, 27);
            txtEmail.TabIndex = 1;
            // 
            // btnGonder
            // 
            btnGonder.Location = new Point(109, 121);
            btnGonder.Name = "btnGonder";
            btnGonder.Size = new Size(93, 39);
            btnGonder.TabIndex = 2;
            btnGonder.Text = "Kod oluştur";
            btnGonder.UseVisualStyleBackColor = true;
            btnGonder.Click += btnGonder_Click;
            // 
            // btnSifirla
            // 
            btnSifirla.Location = new Point(219, 121);
            btnSifirla.Name = "btnSifirla";
            btnSifirla.Size = new Size(100, 39);
            btnSifirla.TabIndex = 3;
            btnSifirla.Text = "Kodu kullan";
            btnSifirla.UseVisualStyleBackColor = true;
            btnSifirla.Click += btnSifirla_Click;
            // 
            // ForgotPasswordForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(423, 227);
            Controls.Add(btnSifirla);
            Controls.Add(btnGonder);
            Controls.Add(txtEmail);
            Controls.Add(label1);
            Name = "ForgotPasswordForm";
            Text = "Şifremi Unuttum";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtEmail;
        private Button btnGonder;
        private Button btnSifirla;
    }
}