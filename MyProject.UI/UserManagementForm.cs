using MyProject.BLL;
using MyProject.Entities;

namespace MyProject.UI
{
    public partial class UserManagementForm : Form
    {
        private readonly UserBLL _userBll = new UserBLL();

        public UserManagementForm()
        {
            InitializeComponent();
        }

        private void UserManagementForm_Load(object sender, EventArgs e)
        {
            dgvUsers.ReadOnly = true;
            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsers.DataSource = _userBll.GetAllUsers();
            cmbYeniRol.DisplayMember = "RoleName";
            cmbYeniRol.ValueMember = "RoleId";
            cmbYeniRol.DataSource = _userBll.GetAllRoles();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            using (CreateUserForm form = new CreateUserForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                    dgvUsers.DataSource = _userBll.GetAllUsers();
            }
        }

        private void btnDurum_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow?.DataBoundItem is not User user)
            {
                MessageBox.Show("Listeden bir kullanıcı seç.");
                return;
            }

            try
            {
                _userBll.SetUserActive(user.UserId, !user.IsActive);
                dgvUsers.DataSource = _userBll.GetAllUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "İşlem yapılamadı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnRolAta_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow?.DataBoundItem is not User user)
            {
                MessageBox.Show("Listeden bir kullanıcı seç.");
                return;
            }

            if (cmbYeniRol.SelectedValue == null)
            {
                MessageBox.Show("Rol seç.");
                return;
            }

            try
            {
                _userBll.UpdateUserRole(user.UserId, (int)cmbYeniRol.SelectedValue);
                dgvUsers.DataSource = _userBll.GetAllUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "İşlem yapılamadı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}