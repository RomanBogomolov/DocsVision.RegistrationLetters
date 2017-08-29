using System;

namespace DocsVision.RegistrationLetters.Api.Models
{
    public class CreateFolderInputModel
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
}