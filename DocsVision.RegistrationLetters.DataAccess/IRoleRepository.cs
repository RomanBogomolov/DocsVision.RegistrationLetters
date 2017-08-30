using System;
using System.Collections.Generic;
using DocsVision.RegistrationLetters.Model;

namespace DocsVision.RegistrationLetters.DataAccess
{
    public interface IRoleRepository
    {
        void CreateRole(string name);
        void DeleteRole(int id);
        void AddUserToRole(Guid userId, int roleId);
        IEnumerable<Roles> GetAllRoles();
    }
}