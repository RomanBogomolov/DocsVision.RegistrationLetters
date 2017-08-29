using System;

namespace DocsVision.RegistrationLetters.Model
{
    /// <summary>
    /// Папки пользователя
    /// </summary>
    public class UserFolders
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string FolderName { get; set; }
        public int Level { get; set; }
    }
}