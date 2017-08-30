using System;
using System.Collections.Generic;
using System.Data;
using DocsVision.RegistrationLetters.DataAccess.Sql.Sql;
using DocsVision.RegistrationLetters.Log;
using DocsVision.RegistrationLetters.Model;

namespace DocsVision.RegistrationLetters.DataAccess.Sql
{
    public class RoleRepository : BaseRepository, IRoleRepository
    {
        public RoleRepository(string connectionString, ILogger logger) : base(connectionString, logger)
        {
        }

        public void AddUserToRole(Guid userId, int roleId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentOutOfRangeException(nameof(userId));
            }
            if (roleId < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(roleId));
            }
            try
            {
                Execute(StoreProcedures.AddUserToRole, new { roleId, userId }, CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                _logger.Error(e.StackTrace);
            }
        }

        public void CreateRole(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            try
            {
                Execute(StoreProcedures.CreateRole, new {name}, CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                _logger.Error(e.StackTrace);
            }
        }

        public void DeleteRole(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }
            try
            {
                Execute(StoreProcedures.DeleteRole, new { id }, CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                _logger.Error(e.StackTrace);
            }
        }

        public IEnumerable<Roles> GetAllRoles()
        {
            try
            {
                return Query<Roles>(SqlStrings.GetRoles);
            }
            catch (Exception e)
            {
                _logger.Error(e.StackTrace);
                return null;
            }
        }
    }
}