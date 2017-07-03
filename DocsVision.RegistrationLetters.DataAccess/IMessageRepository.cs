using System;
using System.Collections.Generic;
using DocsVision.RegistrationLetters.Model;

namespace DocsVision.RegistrationLetters.DataAccess
{
    public interface IMessageRepository
    {
        Message GetMessageInfo(Guid messageId);
        void SendMessage(Message message, IEnumerable<Guid> userIds);
        IEnumerable<Message> GetMessages(Guid userId);
        void DeleteMessages(Guid userId, IEnumerable<Guid> messageIds);
        void UpdateMessageRead(Guid messageId, Guid userId);
    }
}