using System;
using System.Collections.Generic;
using DocsVision.RegistrationLetters.Model;

namespace DocsVision.RegistrationLetters.DataAccess
{
    public interface IUserFolderRepository
    {
        /// <summary>
        /// Создать личную папку пользователя
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <param name="name">Наименование папки</param>
        /// <param name="parentId">Id корневой дирректории</param>
        void CreateFolder(Guid userId, string name, int? parentId = null);
        /// <summary>
        /// Получить дирректории пользователя
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <returns></returns>
        IEnumerable<UserFolders> GetUserFolders(Guid userId);
        /// <summary>
        /// Удалить папку со всем содержимым
        /// </summary>
        /// <param name="folderId">Id папки</param>        
        /// /// <param name="userId">Id пользователя</param>
        void DeleteFolder(int folderId, Guid userId);
    }
}