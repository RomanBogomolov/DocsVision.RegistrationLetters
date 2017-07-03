using System;

namespace DocsVision.RegistrationLetters.Model
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Theme { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public User User { get; set; }
    }
}