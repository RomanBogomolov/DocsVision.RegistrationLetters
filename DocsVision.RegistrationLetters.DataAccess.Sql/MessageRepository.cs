using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DocsVision.RegistrationLetters.DataAccess.Sql.Sql;
using DocsVision.RegistrationLetters.Log;
using DocsVision.RegistrationLetters.Model;
using Newtonsoft.Json;

namespace DocsVision.RegistrationLetters.DataAccess.Sql
{
    public class MessageRepository : BaseRepository, IMessageRepository
    {
        public MessageRepository(string connectionString, ILogger logger) : base(connectionString, logger)
        {
        }

        public Message FindMessageById(Guid messageId)
        {
            if (messageId == Guid.Empty)
            {
                throw new ArgumentOutOfRangeException(nameof(messageId));
            }
            try
            {
                return OneToOneTableQuery<Message, User>(StoreProcedures.SelectMessageByid, u => u.Sender,
                    new {id = messageId}, type: CommandType.StoredProcedure, splitTableName: "Sender").FirstOrDefault();
            }
            catch (Exception e)
            {
                _logger.Error(e.StackTrace);
                return null;
            }
        }
        
        /* Подумать! Id из [dbo].[UserMessage] */
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
                return OneToOneTableQuery<Message, User>(StoreProcedures.GetUserMessagesInFolder, u => u.Sender,
                    new {folderId, userId}, type: CommandType.StoredProcedure, splitTableName: "Sender");
            }
            catch (Exception e)
            {
                _logger.Error(e.StackTrace);
                return null;
            }
        }

        public void DeleteMessages(IEnumerable<Guid> userMessageIds)
        {
            if (userMessageIds == null)
            {
                throw new ArgumentNullException(nameof(userMessageIds));
            }
            try
            {
                string jsonUserMessageIds = JsonConvert.SerializeObject(userMessageIds.Select(g => g.ToString()));
                Execute(StoreProcedures.DeleteUserMessages, new { userMessageIds = jsonUserMessageIds }, CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                _logger.Error(e.StackTrace);
            }
        }

        public void RemoveMessages(IEnumerable<Guid> userMessageIds)
        {
            if (userMessageIds == null)
            {
                throw new ArgumentNullException(nameof(userMessageIds));
            }
            try
            {
                string jsonUserMessageIds = JsonConvert.SerializeObject(userMessageIds.Select(g => g.ToString()));
                Execute(StoreProcedures.RemoveUserMessages, new { userMessageIds = jsonUserMessageIds }, CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                _logger.Error(e.StackTrace);
            }
        }

        public void SendMessage(Message message, IEnumerable<string> emails)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }
            if (emails == null)
            {
                throw new ArgumentNullException(nameof(emails));
            }
            try
            {
                message.Id = Guid.NewGuid();
                message.Date = DateTime.Now;
                Execute(StoreProcedures.SendMessage, new
                {
                    messageId = message.Id,
                    theme = message.Theme,
                    date = message.Date,
                    text = message.Text,
                    senderId = message.Sender.Id,
                    jsonEmails = JsonConvert.SerializeObject(emails)
                }, CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                _logger.Error(e.StackTrace);
            }
        }

        public void UpdateMessageRead(Guid userMessageId)
        {
            if (userMessageId == Guid.Empty)
            {
                throw new ArgumentOutOfRangeException(nameof(userMessageId));
            }
            try
            {
                Execute(StoreProcedures.UpdateStatusInMessage, new { userMessageId }, CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                _logger.Error(e.StackTrace);
            }
            
        }

        public void MoveUserMessage(int folderId, Guid userMessageId)
        {
            if (userMessageId == Guid.Empty)
            {
                throw new ArgumentOutOfRangeException(nameof(userMessageId));
            }
            if (folderId < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(folderId));
            }
            try
            {
                Execute(StoreProcedures.MoveUserMessageInFolder, new {folderId, userMessageId},
                    CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                _logger.Error(e.StackTrace);
            }
        }
    }
}