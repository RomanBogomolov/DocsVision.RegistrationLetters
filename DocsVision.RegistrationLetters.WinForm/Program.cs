﻿using System;
using System.Windows.Forms;

namespace DocsVision.RegistrationLetters.WinForm
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new EmailForm());
        }
    }
}
