namespace DocsVision.RegistrationLetters.WinForm
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.colTheme = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUrl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnGet = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRead = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxTheme = new System.Windows.Forms.TextBox();
            this.textBoxEmail = new System.Windows.Forms.TextBox();
            this.textBoxFullname = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxMes = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.sendMes = new System.Windows.Forms.Button();
            this.btn_ReLogin = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTheme,
            this.colMes,
            this.colDate,
            this.colUrl});
            this.dataGridView.Location = new System.Drawing.Point(12, 57);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.Size = new System.Drawing.Size(648, 183);
            this.dataGridView.TabIndex = 4;
            // 
            // colTheme
            // 
            this.colTheme.HeaderText = "Тема";
            this.colTheme.Name = "colTheme";
            this.colTheme.Width = 200;
            // 
            // colMes
            // 
            this.colMes.HeaderText = "Сообщение";
            this.colMes.Name = "colMes";
            this.colMes.Width = 300;
            // 
            // colDate
            // 
            this.colDate.HeaderText = "Дата";
            this.colDate.Name = "colDate";
            // 
            // colUrl
            // 
            this.colUrl.HeaderText = "Url";
            this.colUrl.Name = "colUrl";
            this.colUrl.Visible = false;
            // 
            // btnGet
            // 
            this.btnGet.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnGet.Location = new System.Drawing.Point(666, 57);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(142, 53);
            this.btnGet.TabIndex = 5;
            this.btnGet.Text = "Получить";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "Список сообщений";
            // 
            // btnRead
            // 
            this.btnRead.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnRead.Location = new System.Drawing.Point(666, 116);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(142, 57);
            this.btnRead.TabIndex = 7;
            this.btnRead.Text = "Прочитать";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 263);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(264, 25);
            this.label2.TabIndex = 8;
            this.label2.Text = "Информация о сообщении";
            // 
            // textBoxTheme
            // 
            this.textBoxTheme.Location = new System.Drawing.Point(20, 338);
            this.textBoxTheme.Name = "textBoxTheme";
            this.textBoxTheme.ReadOnly = true;
            this.textBoxTheme.Size = new System.Drawing.Size(617, 20);
            this.textBoxTheme.TabIndex = 9;
            // 
            // textBoxEmail
            // 
            this.textBoxEmail.Location = new System.Drawing.Point(20, 511);
            this.textBoxEmail.Name = "textBoxEmail";
            this.textBoxEmail.ReadOnly = true;
            this.textBoxEmail.Size = new System.Drawing.Size(163, 20);
            this.textBoxEmail.TabIndex = 11;
            // 
            // textBoxFullname
            // 
            this.textBoxFullname.Location = new System.Drawing.Point(207, 511);
            this.textBoxFullname.Name = "textBoxFullname";
            this.textBoxFullname.ReadOnly = true;
            this.textBoxFullname.Size = new System.Drawing.Size(166, 20);
            this.textBoxFullname.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 311);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Тема сообщения";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 373);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Содержание";
            // 
            // textBoxMes
            // 
            this.textBoxMes.Location = new System.Drawing.Point(20, 399);
            this.textBoxMes.Multiline = true;
            this.textBoxMes.Name = "textBoxMes";
            this.textBoxMes.ReadOnly = true;
            this.textBoxMes.Size = new System.Drawing.Size(617, 76);
            this.textBoxMes.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 489);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Отправитель";
            // 
            // sendMes
            // 
            this.sendMes.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.sendMes.Location = new System.Drawing.Point(666, 183);
            this.sendMes.Name = "sendMes";
            this.sendMes.Size = new System.Drawing.Size(142, 57);
            this.sendMes.TabIndex = 16;
            this.sendMes.Text = "Отправить";
            this.sendMes.UseVisualStyleBackColor = true;
            this.sendMes.Click += new System.EventHandler(this.sendMes_Click);
            // 
            // btn_ReLogin
            // 
            this.btn_ReLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_ReLogin.Location = new System.Drawing.Point(666, 489);
            this.btn_ReLogin.Name = "btn_ReLogin";
            this.btn_ReLogin.Size = new System.Drawing.Size(142, 45);
            this.btn_ReLogin.TabIndex = 17;
            this.btn_ReLogin.Text = "Перезайти";
            this.btn_ReLogin.UseVisualStyleBackColor = true;
            this.btn_ReLogin.Click += new System.EventHandler(this.btn_ReLogin_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 552);
            this.Controls.Add(this.btn_ReLogin);
            this.Controls.Add(this.sendMes);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxFullname);
            this.Controls.Add(this.textBoxEmail);
            this.Controls.Add(this.textBoxMes);
            this.Controls.Add(this.textBoxTheme);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.dataGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Список сообщений";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button btnGet;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxTheme;
        private System.Windows.Forms.TextBox textBoxEmail;
        private System.Windows.Forms.TextBox textBoxFullname;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxMes;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button sendMes;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTheme;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMes;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUrl;
        private System.Windows.Forms.Button btn_ReLogin;
    }
}

