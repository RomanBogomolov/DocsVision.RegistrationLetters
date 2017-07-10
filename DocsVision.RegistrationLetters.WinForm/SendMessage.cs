using System;
using System.Windows.Forms;
using DocsVision.RegistrationLetters.Api.Models;
using DocsVision.RegistrationLetters.Model;
using Message = DocsVision.RegistrationLetters.Model.Message;

namespace DocsVision.RegistrationLetters.WinForm
{
    public partial class SendMessage : Form
    {
        public Guid UserId { get; set; }
        private ServiceClient _client;

        public SendMessage(Guid userId)
        {
            UserId = userId;
            _client = new ServiceClient("http://localhost:58199/api/");
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                CompositeMessageEmails mesEmails = new CompositeMessageEmails
                {
                    Emails = textBoxSendTo.Text.Split(','),
                    Message = new Message
                    {
                        Text = textBoxText.Text,
                        Theme = textBoxTheme.Text,
                        User = new User
                        {
                            Id = UserId
                        }
                    }
                };

                _client.SendMessageToUsers(mesEmails);

                MessageBox.Show($"Сообщение успешно отправлено пользователям {textBoxSendTo.Text}", "Отправка Сообщения",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exception)
            {
                MessageBox.Show(
                    $"Не удалось отправить сообщение, текст ошибки: {Environment.NewLine}{exception.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Close();
            }
        }
    }
}
