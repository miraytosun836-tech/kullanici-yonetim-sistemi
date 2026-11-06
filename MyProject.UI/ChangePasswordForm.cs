using MyProject.BLL;

namespace MyProject.UI
{
    public partial class ChangePasswordForm : Form
    {
        private readonly UserBLL _userBll = new UserBLL();
        private readonly bool _askOldPassword;

        public ChangePasswordForm(bool askOldPassword)
        {
            InitializeComponent();
            _askOldPassword = askOldPassword;
            lblOld.Visible = askOldPassword;
            txtOld.Visible = askOldPassword;
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            string? oldPassword = _askOldPassword ? txtOld.Text : null;
            string? error = _userBll.ChangePassword(
                txtNew.Text,
                txtConfirm.Text,
                oldPassword,
                _askOldPassword);

            if (error != null)
            {
                MessageBox.Show(error, "Parola uygun değil",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("Parola değiştirildi.", "Bilgi",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}