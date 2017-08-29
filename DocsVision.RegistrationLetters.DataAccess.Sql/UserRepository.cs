using System;
using System.Data;
using System.Data.SqlClient;
using DocsVision.RegistrationLetters.DataAccess.Sql.Exceptions;
using DocsVision.RegistrationLetters.DataAccess.Sql.SQLHelper;
using DocsVision.RegistrationLetters.Log;
using DocsVision.RegistrationLetters.Model;
using Newtonsoft.Json;

namespace DocsVision.RegistrationLetters.DataAccess.Sql
{
    public class UserRepository : IUserRepository
    {
        public object[] GetInvalidUserEmails(string[] emails)
        {
            using (var logger = new LogWrapper())
            {
                if (emails == null)
                {
                    logger.Error(ExceptionDescribed.EmailIsNull);
                    return null;
                }

                try
                {
                    string invalidEmails = string.Empty;
                    SqlParameter[] param =
                    {
                        new SqlParameter("@Emails", JsonConvert.SerializeObject(emails)),
                        new SqlParameter("@invalidEmails", SqlDbType.NVarChar, 4000)
                        {
                            Direction = ParameterDirection.Output
                        },
                    };

                    using (var connection = new SqlConnection(SqlHelper.GetConnectionString()))
                    {
                        SqlHelper.ExecuteNonQuery(
                            connection, 
                            CommandType.StoredProcedure, 
                            "up_Check_user_emails",
                            param);

                        return param[1].Value as object[];
                    }
                }
                catch (Exception e)
                {
                    logger.Error(e.StackTrace);
                    return null;
                }
            }
        }

        public User FindByEmail(string email)
        {
            using (var logger = new LogWrapper())
            {
                if (string.IsNullOrEmpty(email))
                {
                    logger.Error(ExceptionDescribed.EmailIsNull);
                    return null;
                }
                try
                {
                    SqlParameter[] param = {new SqlParameter("@email", email)};
                    string query = "SELECT id, name, secondname, email FROM uf_Select_user_info_by_email(@email)";

                    using (var connection = new SqlConnection(SqlHelper.GetConnectionString()))
                    {
                        SqlDataReader data = SqlHelper.ExecuteReader(connection, CommandType.Text, query, param);
                        if (data.Read())
                        {
                            User userInfo = new User
                            {
                                Id = (Guid) data["id"],
                                Name = data["name"].ToString(),
                                SecondName = data["secondname"].ToString(),
                                Email = data["email"].ToString()
                            };
                            return userInfo;
                        }
                        logger.Error(ExceptionDescribed.StringIsUnavailable(email));
                        return null;
                    }
                }
                catch (Exception e)
                {
                    logger.Error(e.StackTrace);
                    return null;
                }
            }
        }

        public User FindById(Guid id)
        {
            using (var logger = new LogWrapper())
            {
                if (id == Guid.Empty)
                {
                    logger.Error(ExceptionDescribed.GuidIsEmpty);
                    return null;
                }
                try
                {
                    SqlParameter[] param = {new SqlParameter("@id", id)};
                    string query = "SELECT id, name, secondname, email FROM uf_Select_user_info_by_id(@id)";

                    using (var connection = new SqlConnection(SqlHelper.GetConnectionString()))
                    {
                        SqlDataReader data = SqlHelper.ExecuteReader(connection, CommandType.Text, query, param);
                        if (data.Read())
                        {
                            User userInfo = new User
                            {
                                Id = (Guid) data["id"],
                                Name = data["name"].ToString(),
                                SecondName = data["secondname"].ToString(),
                                Email = data["email"].ToString()
                            };
                            return userInfo;
                        }
                        logger.Error(ExceptionDescribed.StringIsUnavailable(id.ToString()));
                        return null;
                    }
                }
                catch (Exception e)
                {
                    logger.Error(e.StackTrace);
                    return null;
                }
            }
        }
    }
}