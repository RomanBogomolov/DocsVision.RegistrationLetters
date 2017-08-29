using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DocsVision.RegistrationLetters.DataAccess.Sql.Exceptions;
using DocsVision.RegistrationLetters.DataAccess.Sql.SQLHelper;
using DocsVision.RegistrationLetters.Log;
using DocsVision.RegistrationLetters.Model;
using Newtonsoft.Json;

namespace DocsVision.RegistrationLetters.DataAccess.Sql
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IUserRepository _userRepository;

        public MessageRepository(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Message FindMessageById(Guid messageId)
        {
            using (var logger = new LogWrapper())
            {
                if (messageId == Guid.Empty)
                {
                    logger.Error(ExceptionDescribed.MessageIdIsNull);
                    return null;
                }
                try
                {
                    SqlParameter[] param = {new SqlParameter("@id", messageId)};
                    string query = "SELECT id, theme, date, text, senderId FROM uf_Select_message_info_by_id(@id)";
                    
                    using (var connection = new SqlConnection(SqlHelper.GetConnectionString()))
                    {
                        SqlDataReader data = SqlHelper.ExecuteReader(connection, CommandType.Text, query, param);

                        if (data.Read())
                        {
                            Message message = new Message
                            {
                                Id = (Guid) data["id"],
                                Theme = data["theme"].ToString(),
                                Date = (DateTime) data["date"],
                                Text = data["text"].ToString(),
                                Sender = _userRepository.FindById((Guid) data["senderId"])
                            };
                            return message;
                        }
                        logger.Error(ExceptionDescribed.MessageIsUnavailable(messageId));
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
        
        public IEnumerable<Message> GetMessagesInFolder(int folderId, Guid userId)
        {
            using (var logger = new LogWrapper())
            {
                if (folderId < 0)
                {
                    logger.Error(ExceptionDescribed.IdIsNegative);
                    return null;
                }
                if (userId == Guid.Empty)
                {
                    logger.Error(ExceptionDescribed.GuidIsEmpty);
                    return null;
                }
                try
                {
                    List<Message> userMessage = new List<Message>();
                    SqlParameter[] param =
                    {
                        new SqlParameter("@folderId", folderId),
                        new SqlParameter("@userId", userId),
                    };
                    string query = "SELECT messageId FROM uf_Select_user_messages_in_folder(@userId, @folderId)";

                    using (var connection = new SqlConnection(SqlHelper.GetConnectionString()))
                    {
                        SqlDataReader data = SqlHelper.ExecuteReader(connection, CommandType.Text, query, param);
                        while (data.Read())
                        {
                            userMessage.Add(FindMessageById(userId));
                        }
                        return userMessage;
                    }
                }
                catch (Exception e)
                {
                    logger.Error(e.StackTrace);
                    return null;
                }
            }
        }

        public void DeleteMessages(IEnumerable<Guid> userMessageIds)
        {
            using (var logger = new LogWrapper())
            {
                if (userMessageIds == null)
                {
                    logger.Error(ExceptionDescribed.DeletedIdIsNotSpecified);
                    return;
                }
                try
                {
                    SqlParameter[] param ={new SqlParameter("@userMessageIds", JsonConvert.SerializeObject(userMessageIds))};

                    using (var connection = new SqlConnection(SqlHelper.GetConnectionString()))
                    {
                        SqlHelper.ExecuteNonQuery(
                            connection, 
                            CommandType.StoredProcedure, 
                            "up_Delete_user_messages",
                            param);  
                    }
                }
                catch (Exception e)
                {
                    logger.Error(e.StackTrace);
                }
            }
        }

        public void RemoveMessages(IEnumerable<Guid> userMessageIds)
        {
            using (var logger = new LogWrapper())
            {
                if (userMessageIds == null)
                {
                    logger.Error(ExceptionDescribed.DeletedIdIsNotSpecified);
                    return;
                }
                try
                {
                    SqlParameter[] param ={new SqlParameter("@userMessageIds", JsonConvert.SerializeObject(userMessageIds))};

                    using (var connection = new SqlConnection(SqlHelper.GetConnectionString()))
                    {
                        SqlHelper.ExecuteNonQuery(
                            connection,
                            CommandType.StoredProcedure,
                            "up_Remove_user_messages",
                            param);
                    }
                }
                catch (Exception e)
                {
                    logger.Error(e.StackTrace);
                }
            }
        }

        public void SendMessage(Message message, IEnumerable<Guid> userIds)
        {
            using (var logger = new LogWrapper())
            {
                if (message == null)
                {
                    logger.Error(ExceptionDescribed.MessageIsNull);
                    return;
                }
                if (userIds == null)
                {
                    logger.Error(ExceptionDescribed.UserIdsIsNull);
                    return;
                }
                try
                {
                    message.Id = Guid.NewGuid();
                    message.Date = DateTime.Now;

                    SqlParameter[] param =
                    {
                        new SqlParameter("@messageId", message.Id),
                        new SqlParameter("@theme", message.Theme),
                        new SqlParameter("@date", message.Date),
                        new SqlParameter("@text", message.Text),
                        new SqlParameter("@senderId", message.Sender.Id),
                        new SqlParameter("@jsonIds", JsonConvert.SerializeObject(userIds))
                    };
                    using (var connection = new SqlConnection(SqlHelper.GetConnectionString()))
                    {
                        SqlHelper.ExecuteNonQuery(
                            connection, 
                            CommandType.StoredProcedure, 
                            "up_Send_new_Message",
                            param);
                    }
                }
                catch (Exception e)
                {
                    logger.Error(e.StackTrace);
                }
            }
        }

        public void UpdateMessageRead(Guid userMessageId)
        {
            using (var logger = new LogWrapper())
            {
                if (userMessageId == Guid.Empty)
                {
                    logger.Error(ExceptionDescribed.UserMessageIdIsNull);
                    return;
                }
                try
                {
                    SqlParameter[] param = { new SqlParameter("@userMessageId", userMessageId) };

                    using (var connection = new SqlConnection(SqlHelper.GetConnectionString()))
                    {
                        SqlHelper.ExecuteNonQuery(
                            connection,
                            CommandType.StoredProcedure,
                            "up_Update_read_in_message",
                            param);
                    }
                }
                catch (Exception e)
                {
                    logger.Error(e.StackTrace);
                }
            }
        }
    }
}