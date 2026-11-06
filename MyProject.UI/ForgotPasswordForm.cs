using MyProject.BLL;

namespace MyProject.UI
{
    public partial class ForgotPasswordForm : Form
    {
        private readonly PasswordResetBLL _resetBll = new PasswordResetBLL();

        public ForgotPasswordForm()
        {
            InitializeComponent();
        }

        private void btnGonder_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("E-posta yaz.");
                return;
            }

            ResetRequestResult result = _resetBll.RequestReset(txtEmail.Text);
            string text = result.Message;

            if (!string.IsNullOrEmpty(result.TokenForTest))
            {
                text += "\n\nE-posta henüz bağlı değil. Bu kodu bir yere yaz:\n"
                    + result.TokenForTest;
            }

            MessageBox.Show(text, "Şifremi unuttum",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSifirla_Click(object sender, EventArgs e)
        {
            using (ResetPasswordForm form = new ResetPasswordForm())
            {
                form.ShowDialog();
            }
        }
    }
}