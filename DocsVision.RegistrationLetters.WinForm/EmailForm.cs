using System;
using System.Windows.Forms;

namespace DocsVision.RegistrationLetters.WinForm
{
    public partial class EmailForm : Form
    {
        private ServiceClient _client;

        public EmailForm()
        {
            _client = new ServiceClient("http://localhost:58199/api/");
            InitializeComponent();
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            try
            {
                var email = textBoxEmail.Text;
                var user = _client.GetUserByEmail(email);
                MainForm mainForm = new MainForm(user.Id);
                mainForm.Show();
                this.Hide();
            }
            catch (Exception exception)
            {
                MessageBox.Show(
                    $"Не удалось найти пользователя, текст ошибки: {Environment.NewLine}{exception.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
