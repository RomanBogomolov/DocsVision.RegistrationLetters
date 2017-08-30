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
//                return QueryFirstOrDefault<Message>(SqlStrings.SelectMessageById, new { id = messageId });
                return MultipleTablesQuery<Message, User, Guid>(StoreProcedures.SelectMessageByid, m => m.Id, u => u.Sender,
                    new { id = messageId }, commandType: CommandType.StoredProcedure).FirstOrDefault();
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
                var lookup = new Dictionary<Guid, Message>();
                Query<Message, User, Message>(StoreProcedures.GetUserMessagesInFolder, (m, u) =>
                {
                    Message mess;
                    if (!lookup.TryGetValue(m.Id, out mess))
                        lookup.Add(m.Id, mess = m);
                    if (mess.Sender == null)
                        mess.Sender = new User();
                    mess.Sender = u;
                    return mess;
                }, new { folderId, userId }, CommandType.StoredProcedure);

                var messages = lookup.Values.ToList();
                return messages;
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
                Execute(StoreProcedures.DeleteUserMessages, new {userMessageIds}, CommandType.StoredProcedure);
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
                Execute(StoreProcedures.RemoveUserMessages, new { userMessageIds }, CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                _logger.Error(e.StackTrace);
            }
        }

        public void SendMessage(Message message, IEnumerable<Guid> userIds)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }
            if (userIds == null)
            {
                throw new ArgumentNullException(nameof(userIds));
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
                    jsonIds = JsonConvert.SerializeObject(userIds)
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