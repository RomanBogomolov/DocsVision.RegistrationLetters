using System;
using System.Windows.Forms;

namespace DocsVision.RegistrationLetters.WinForm
{
    public partial class MainForm : Form
    {
        public Guid UserId { get; set; }
        
        /// <summary>
        /// Хард код
        /// </summary>
        //private readonly Guid _userId = new Guid("737AF1C2-F090-49CC-B7E3-737774D99B7E");

        private ServiceClient _client;
        public MainForm(Guid userId)
        {
            InitializeComponent();
            UserId = userId;
            _client = new ServiceClient("http://localhost:58199/api/", UserId);
            dataGridView.MultiSelect = false;
            dataGridView.ReadOnly = true;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void GetData()
        {
            try
            {
                dataGridView.Rows.Clear();
                var result = _client.GetUserMessages();
                foreach (var res in result)
                {
                    dataGridView.Rows.Add(res.Theme, res.Text, res.Date, res.Url);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Не удалось загрузить сообщение, текст ошибки: {Environment.NewLine}{exception.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView.SelectedRows.Count != 0)
                {
                    DataGridViewRow row = dataGridView.SelectedRows[0];
                    var url = row.Cells["colUrl"].Value.ToString();
                    var result = _client.GetMessageInfo(url);

                    textBoxTheme.Text = result.Theme;
                    textBoxMes.Text = result.Text;
                    textBoxEmail.Text = result.SenderEmail;
                    textBoxFullname.Text = result.Fullname;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Не удалось загрузить сообщение, текст ошибки: {Environment.NewLine}{exception.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void sendMes_Click(object sender, EventArgs e)
        {
            SendMessage sendMes = new SendMessage(UserId);
            sendMes.ShowDialog(this);
           
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void btn_ReLogin_Click(object sender, EventArgs e)
        {
            this.Close();
            EmailForm email = new EmailForm();
            email.ShowDialog();
        }
    }
}
