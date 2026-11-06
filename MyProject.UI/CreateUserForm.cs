using MyProject.BLL;
using MyProject.Entities;

namespace MyProject.UI
{
    public partial class CreateUserForm : Form
    {
        private readonly UserBLL _userBll = new UserBLL();

        public CreateUserForm()
        {
            InitializeComponent();
        }

        private void CreateUserForm_Load(object sender, EventArgs e)
        {
            cmbRole.DisplayMember = "RoleName";
            cmbRole.ValueMember = "RoleId";
            cmbRole.DataSource = _userBll.GetAllRoles();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (cmbRole.SelectedValue == null)
            {
                MessageBox.Show("Rol seçiniz.");
                return;
            }

            int roleId = (int)cmbRole.SelectedValue;
            CreateUserResult result = _userBll.CreateUser(
                txtEmail.Text,
                txtFirstName.Text,
                txtLastName.Text,
                dtpBirthDate.Value.Date,
                roleId);

            if (!result.IsSuccess)
            {
                MessageBox.Show(result.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show(
                result.Message + "\n\nGeçici parola: " + result.GeneratedPassword +
                "\n\nBu parolayı not et; ilk girişte değiştirilecek.",
                "Kullanıcı oluşturuldu",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
