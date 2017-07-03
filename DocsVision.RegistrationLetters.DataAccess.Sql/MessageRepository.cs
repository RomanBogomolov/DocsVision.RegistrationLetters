using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DocsVision.RegistrationLetters.Log;
using DocsVision.RegistrationLetters.Model;
using Newtonsoft.Json;

namespace DocsVision.RegistrationLetters.DataAccess.Sql
{
    public class MessageRepository : IMessageRepository
    {
        private readonly string _connectionString;
        private readonly IUserRepository _userRepository;

        public MessageRepository(string connectionString, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _connectionString = connectionString;
        }

        public void DeleteMessages(Guid userId, IEnumerable<Guid> messageIds)
        {
            using (var logger = new LogWrapper())
            {
                if (userId == Guid.Empty)
                {
                    logger.Error("Не указан Id сообщения.");
                    throw new ArgumentNullException(nameof(userId), "Не указан id пользователя");
                }

                if (messageIds == null)
                {
                    logger.Error("Не указаны удаляемые сообщения");
                    throw new ArgumentNullException(nameof(userId), "Не указаны удаляемые сообщения");
                }

                try
                {
                    using (var connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();

                        using (var command = new SqlCommand("up_Delete_user_messages", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            var jsonIds = JsonConvert.SerializeObject(messageIds);

                            command.Parameters.AddWithValue("@userId", userId);
                            command.Parameters.AddWithValue("@jsonIds", jsonIds);

                            command.ExecuteNonQuery();
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

        public Message GetMessageInfo(Guid messageId)
        {
            using (var logger = new LogWrapper())
            {
                if (messageId == Guid.Empty)
                {
                    logger.Error("Не указан Id сообщения.");
                    throw new ArgumentNullException(nameof(messageId), "Не указан Id сообщения");
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
                                                  "theme," +
                                                  "date," +
                                                  "text," +
                                                  "senderId " +
                                                  "FROM uf_Select_message_info_by_id(@id)";

                            command.Parameters.AddWithValue("@id", messageId);

                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var messageInfo = new Message
                                    {
                                        Id = reader.GetGuid(reader.GetOrdinal("id")),
                                        Theme = reader.GetString(reader.GetOrdinal("theme")),
                                        Date = reader.GetDateTime(reader.GetOrdinal("date")),
                                        Text = reader.GetString(reader.GetOrdinal("text")),
                                        User = _userRepository.FindById(reader.GetGuid(reader.GetOrdinal("senderId")))
                                    };

                                    return messageInfo;
                                }
                                logger.Error($"Сообщение с: {messageId} недоступено");
                                throw new ArgumentException($"Сообщение с: {messageId} недоступно.");
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

        public IEnumerable<Message> GetMessages(Guid userId)
        {
            using (var logger = new LogWrapper())
            {
                if (userId == Guid.Empty)
                {
                    logger.Error("Не указан пользователь");
                    throw new ArgumentNullException(nameof(userId), "Не указан пользователь");
                }

                try
                {
                    var messages = new List<Message>();

                    using (var connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();

                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = "SELECT messageId FROM uf_Select_message_info_by_userid(@userId)";
                            command.Parameters.AddWithValue("@userId", userId);

                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    messages.Add(GetMessageInfo(reader.GetGuid(reader.GetOrdinal("messageId"))));
                                }

                                return messages;
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

        public void SendMessage(Message message, IEnumerable<Guid> userIds)
        {
            using (var logger = new LogWrapper())
            {
                if (message == null)
                {
                    logger.Error("Сообщение не может быть пустым");
                    throw new ArgumentNullException(nameof(message), "Сообщение не может быть пустым");
                }

                if (userIds == null)
                {
                    logger.Error("Не указаны пользователи для отправки сообщения");
                    throw new ArgumentNullException(nameof(userIds), "Не указаны пользователи для отправки сообщения");
                }

                try
                {
                    using (var connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();

                        using (var commmand = new SqlCommand("up_Send_new_Message", connection))
                        {
                            commmand.CommandType = CommandType.StoredProcedure;

                            message.Id = Guid.NewGuid();
                            message.Date = DateTime.Now;

                            var jsonIds = JsonConvert.SerializeObject(userIds);

                            commmand.Parameters.AddWithValue("@messageId", message.Id);
                            commmand.Parameters.AddWithValue("@theme", message.Theme);
                            commmand.Parameters.AddWithValue("@date", message.Date);
                            commmand.Parameters.AddWithValue("@text", message.Text);
                            commmand.Parameters.AddWithValue("@senderId", message.User.Id);
                            commmand.Parameters.AddWithValue("@jsonIds", jsonIds);

                            commmand.ExecuteNonQuery();
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

        public void UpdateMessageRead(Guid messageId, Guid userId)
        {
            using (var logger = new LogWrapper())
            {
                if (messageId == Guid.Empty)
                {
                    logger.Error("Не указано сообщение");
                    throw new ArgumentNullException(nameof(messageId), "Не указано сообщение");
                }

                if (userId == Guid.Empty)
                {
                    logger.Error("Не указан пользователь");
                    throw new ArgumentNullException(nameof(userId), "Не указан пользователь");
                }

                try
                {
                    using (var connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();

                        using (var commmand = new SqlCommand("up_Update_read_in_message", connection))
                        {
                            commmand.CommandType = CommandType.StoredProcedure;

                            commmand.Parameters.AddWithValue("@messageId", messageId);
                            commmand.Parameters.AddWithValue("@userId", userId);

                            commmand.ExecuteNonQuery();
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