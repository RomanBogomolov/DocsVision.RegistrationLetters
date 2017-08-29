using System;
using System.Collections.Generic;
using DocsVision.RegistrationLetters.Model;

namespace DocsVision.RegistrationLetters.DataAccess
{
    public interface IUserRepository
    {
        User FindById(Guid id);
        User FindByEmail(string email);

        /// <summary>
        /// Проверим существование email-ов пользователей, которым отправляем сообщения
        /// </summary>
        /// <param name="emails">Массив email-ов</param>
        /// <returns>Невалидные email-ы</returns>
        object[] GetInvalidUserEmails(string[] emails);
    }
}