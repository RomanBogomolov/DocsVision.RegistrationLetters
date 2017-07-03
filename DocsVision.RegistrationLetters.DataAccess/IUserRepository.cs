using System;
using DocsVision.RegistrationLetters.Model;

namespace DocsVision.RegistrationLetters.DataAccess
{
    public interface IUserRepository
    {
        User FindById(Guid id);
        User FindByEmail(string email);
    }
}