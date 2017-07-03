using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocsVision.RegistrationLetters.WinForm
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Хард код
        /// </summary>
        private readonly Guid _userId = new Guid("737AF1C2-F090-49CC-B7E3-737774D99B7E");

        private ServiceClient _client;
        public MainForm()
        {
            InitializeComponent();
            _client = new ServiceClient("http://localhost:58199/api/", _userId);
            dataGridView.MultiSelect = false;
            dataGridView.ReadOnly = true;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void btnGet_Click(object sender, EventArgs e)
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
    }
}
