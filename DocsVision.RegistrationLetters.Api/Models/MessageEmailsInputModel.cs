using System;
using System.Collections.Generic;
using DocsVision.RegistrationLetters.Model;

namespace DocsVision.RegistrationLetters.Api.Models
{
    /// <summary>
    /// Вспомогательный класс для передачи нескольких объектов (сообщения и списка email'ов) в теле запроса
    /// </summary>
    public class MessageEmailsInputModel
    {
        public Message Message { get; set; }
        public string[] Emails { get; set; }
    }
}