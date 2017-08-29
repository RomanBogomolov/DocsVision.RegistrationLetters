namespace DocsVision.RegistrationLetters.Api.Models
{
    /// <summary>
    /// Вспомогательный класс для передачи нескольких объектов (сообщения и списка email'ов) в теле запроса
    /// </summary>
    public class MessageEmailsInputModel
    {
        public Model.Message Message { get; set; }
        public string[] Emails { get; set; }
    }
}