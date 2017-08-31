using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using DocsVision.RegistrationLetters.DataAccess.Sql.Sql;
using DocsVision.RegistrationLetters.Log;
using DocsVision.RegistrationLetters.Model;
using Newtonsoft.Json;

namespace DocsVision.RegistrationLetters.DataAccess.Sql
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(string connectionString, ILogger logger) : base(connectionString, logger)
        {
        }

        public IEnumerable<string> GetInvalidUserEmails(string[] emails)
        {
            if (emails == null)
            {
                throw new ArgumentNullException(nameof(emails));
            }
            try
            {
                string jsonEmails = JsonConvert.SerializeObject(emails);
                return Query<string>("up_Check_user_emails", new { emails = jsonEmails }, CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                _logger.Error(e.StackTrace);
                return null;
            }
        }

        public User FindByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email));
            }
            try
            {
                return QueryFirstOrDefault<User>(SqlStrings.FindUserByEmail, new {email});
            }
            catch (Exception e)
            {
                _logger.Error(e.StackTrace);
                return null;
            }
        }

        public User FindById(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }
            try
            {
                return QueryFirstOrDefault<User>(SqlStrings.FindUserById, new {id});
            }
            catch (Exception e)
            {
                _logger.Error(e.StackTrace);
                return null;
            }
        }

        public void RegisterUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            try
            {
                Execute(StoreProcedures.CreateNewUser, new {user.Id, user.Name, user.Email, user.SecondName},
                    CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                _logger.Error(e.StackTrace);
            }
        }

        public void DeleteUser(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }
            try
            {
                Execute(StoreProcedures.DeleteUser, new { id }, CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                _logger.Error(e.StackTrace);
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            try
            {
                return Query<User>(SqlStrings.GetAllUsers, CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                _logger.Error(e.StackTrace);
                return null;
            }
        }
    }
}