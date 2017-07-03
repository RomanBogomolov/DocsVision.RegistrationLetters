using System;
using System.Data.SqlClient;
using DocsVision.RegistrationLetters.Log;
using DocsVision.RegistrationLetters.Model;

namespace DocsVision.RegistrationLetters.DataAccess.Sql
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public User FindByEmail(string email)
        {
            using (var logger = new LogWrapper())
            {
                if (string.IsNullOrEmpty(email))
                {
                    logger.Error("Не указан email пользователя.");
                    throw new ArgumentNullException(nameof(email), "Не указан email пользователя");
                }

                try
                {
                    using (var connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();

                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = "SELECT " +
                                                  "id," +
                                                  "name," +
                                                  "surname," +
                                                  "department," +
                                                  "position," +
                                                  "email " +
                                                  "FROM uf_Select_user_info_by_email(@email)";

                            command.Parameters.AddWithValue("@email", email);

                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var userInfo = new User
                                    {
                                        Id = reader.GetGuid(reader.GetOrdinal("id")),
                                        Name = reader.GetString(reader.GetOrdinal("name")),
                                        SurName = reader.GetString(reader.GetOrdinal("surname")),
                                        Department = reader.GetString(reader.GetOrdinal("department")),
                                        Position = reader.GetString(reader.GetOrdinal("position")),
                                        Email = reader.GetString(reader.GetOrdinal("email")),
                                    };

                                    return userInfo;
                                }
                                logger.Error($"Пользователь c: {email} недоступен");
                                throw new ArgumentException($"Пользователь c: {email} недоступен.");
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    logger.Error(e.Message);
                    throw;
                }
            }
        }

        public User FindById(Guid id)
        {
            using (var logger = new LogWrapper())
            {
                if (id == Guid.Empty)
                {
                    logger.Error("Не указан id пользователя");
                    throw new ArgumentNullException(nameof(id), "Не указан id пользователя");
                }

                try
                {
                    using (var connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();

                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = "SELECT " +
                                                  "id," +
                                                  "name," +
                                                  "surname," +
                                                  "department," +
                                                  "position," +
                                                  "email " +
                                                  "FROM uf_Select_user_info_by_id(@id)";

                            command.Parameters.AddWithValue("@id", id);

                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var userInfo = new User
                                    {
                                        Id = reader.GetGuid(reader.GetOrdinal("id")),
                                        Name = reader.GetString(reader.GetOrdinal("name")),
                                        SurName = reader.GetString(reader.GetOrdinal("surname")),
                                        Department = reader.GetString(reader.GetOrdinal("department")),
                                        Position = reader.GetString(reader.GetOrdinal("position")),
                                        Email = reader.GetString(reader.GetOrdinal("email")),
                                    };

                                    return userInfo;
                                }
                                logger.Error($"Пользователь c: {id} недоступен");
                                throw new ArgumentException($"Пользователь c: {id} недоступен.");
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    logger.Error(e.Message);
                    throw;
                }
            }
        }
    }
}