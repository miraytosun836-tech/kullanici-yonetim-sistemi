using MyProject.BLL;

namespace MyProject.UI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            try
            {
                var userBll = new UserBLL();
                userBll.SeedInitialAdminIfNotExist();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Veritabanı bağlantısı kurulamadı:\n" + ex.Message,
                    "Bağlantı uyarısı",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            Application.Run(new Form1());
        }
    }
}