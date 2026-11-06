using MyProject.BLL;

namespace MyProject.UI
{
    public partial class Form1 : Form
    {
        private readonly UserBLL _userBll = new UserBLL();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Lütfen e-posta ve şifrenizi giriniz.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LoginResult result = _userBll.Authenticate(email, password);

            if (!result.IsSuccess)
            {
                MessageBox.Show(result.Message, "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            txtPassword.Text = "";
            if (result.MustChangePassword)
            {
                using (ChangePasswordForm changeForm = new ChangePasswordForm(false))
                {
                    if (changeForm.ShowDialog() != DialogResult.OK)
                    {
                        _userBll.Logout();
                        return;
                    }
                }
            }
            this.Hide();

            Form sonraki;
            if (result.RoleName.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                sonraki = new AdminMainForm();
            else
                sonraki = new UserMainForm();

            sonraki.FormClosed += (s, args) =>
            {
                _userBll.Logout();
                this.Show();
            };
            sonraki.Show();

        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }

        private void lnkSifremiUnuttum_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (ForgotPasswordForm form = new ForgotPasswordForm())
            {
                form.ShowDialog();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
