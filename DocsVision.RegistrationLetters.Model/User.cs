using System;

namespace DocsVision.RegistrationLetters.Model
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string SurName { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
    }
}
