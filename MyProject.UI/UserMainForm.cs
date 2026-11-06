using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyProject.UI
{
    public partial class UserMainForm : Form
    {
        public UserMainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnParolaDegistir_Click(object sender, EventArgs e)
        {
            using (ChangePasswordForm form = new ChangePasswordForm(true))
            {
                form.ShowDialog();
            }
        }

        private void UserMainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
