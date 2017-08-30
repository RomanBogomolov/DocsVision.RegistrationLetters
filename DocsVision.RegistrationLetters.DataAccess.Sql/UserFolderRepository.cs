using System;
using System.Collections.Generic;
using System.Data;
using DocsVision.RegistrationLetters.DataAccess.Sql.Sql;
using DocsVision.RegistrationLetters.Log;
using DocsVision.RegistrationLetters.Model;

namespace DocsVision.RegistrationLetters.DataAccess.Sql
{
    public class UserFolderRepository : BaseRepository, IUserFolderRepository
    {
        public UserFolderRepository(string connectionString, ILogger logger) : base(connectionString, logger) { }

        public void CreateFolder(Guid userId, string name, int? parentId = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (userId == Guid.Empty)
            {
                throw new ArgumentOutOfRangeException(nameof(userId));
            }
            try
            {
                Execute(StoreProcedures.CreateUserFolder, new {userId, name, parentId}, CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                _logger.Error(e.StackTrace);
            }
        }

        public IEnumerable<UserFolders> GetUserFolders(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentOutOfRangeException(nameof(userId));
            }
            try
            {
                return Query<UserFolders>(SqlStrings.GetUserFolders,
                    new {userId});
            }
            catch (Exception e)
            {
                _logger.Error(e.StackTrace);
                return null;
            }
        }

        public void DeleteFolder(int folderId, Guid userId)
        {
            if (folderId < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userId));
            }
            try
            {
                Execute(StoreProcedures.DeleteUserFolderById, new {folderId, userId}, CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                _logger.Error(e.StackTrace);
            }
        }
    }
}