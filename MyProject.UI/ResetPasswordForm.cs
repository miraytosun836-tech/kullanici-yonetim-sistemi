using MyProject.BLL;

namespace MyProject.UI
{
    public partial class ResetPasswordForm : Form
    {
        private readonly PasswordResetBLL _resetBll = new PasswordResetBLL();

        public ResetPasswordForm()
        {
            InitializeComponent();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            string? error = _resetBll.ResetPassword(txtToken.Text, txtNew.Text, txtConfirm.Text);
            if (error != null)
            {
                MessageBox.Show(error, "Parola sıfırlanamadı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("Parola değiştirildi. Yeni parola ile giriş yap.",
                "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }
    }
}
