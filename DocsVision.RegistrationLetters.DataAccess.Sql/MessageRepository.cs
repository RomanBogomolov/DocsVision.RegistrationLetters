using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DocsVision.RegistrationLetters.DataAccess.Sql.Exceptions;
using DocsVision.RegistrationLetters.DataAccess.Sql.SQLHelper;
using DocsVision.RegistrationLetters.Log;
using DocsVision.RegistrationLetters.Model;
using Newtonsoft.Json;
using DocsVision.RegistrationLetters.DataAccess.Sql.SqlSrtring;

namespace DocsVision.RegistrationLetters.DataAccess.Sql
{
    public class MessageRepository : BaseRepository, IMessageRepository
    {
        private readonly IUserRepository _userRepository;

        public MessageRepository(string connectionString, IUserRepository userRepository, ILogger logger) : base(connectionString, logger)
        {
            _userRepository = userRepository;
        }

        public Message FindMessageById(Guid messageId)
        {
            if (messageId == Guid.Empty)
            {
                throw new ArgumentOutOfRangeException(nameof(messageId));
            }
            try
            {
                return QueryFirstOrDefault<Message>(SqlStrings.SelectMessageById, new { id = messageId });
            }
            catch (Exception e)
            {
                _logger.Error(e.StackTrace);
                return null;
            }
        }
        
        public IEnumerable<Message> GetMessagesInFolder(int folderId, Guid userId)
        {
            if (folderId < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(folderId));
            }
            if (userId == Guid.Empty)
            {
                throw new ArgumentOutOfRangeException(nameof(userId));
            }
            try
            {
                return Query<Message>("up_Select_user_messages_in_folder", new { folderId = folderId, userId = userId }, CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                _logger.Error(e.StackTrace);
                return null;
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

        public void MoveUserMessage(int folderId, Guid userMessageId)
        {
            using (var logger = new LogWrapper())
            {
                if (userMessageId == Guid.Empty)
                {
                    logger.Error(ExceptionDescribed.GuidIsEmpty);
                    return;
                }
                if (folderId < 0)
                {
                    logger.Error(ExceptionDescribed.IdIsNegative);
                    return;
                }
                try
                {
                    SqlParameter[] param =
                    {
                        new SqlParameter("@folderId", folderId),
                        new SqlParameter("@userMessageId", userMessageId)
                    };
                    using (var connection = new SqlConnection(SqlHelper.GetConnectionString()))
                    {
                        SqlHelper.ExecuteNonQuery(
                            connection,
                            CommandType.StoredProcedure,
                            "up_Move_user_message_in_folder",
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