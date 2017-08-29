using System;
using System.Collections.Generic;
using DocsVision.RegistrationLetters.Model;

namespace DocsVision.RegistrationLetters.DataAccess
{
    public interface IMessageRepository
    {
        /// <summary>
        /// Получить информацию о сообщении
        /// </summary>
        /// <param name="messageId">Id сообщения</param>
        /// <returns>Сообщение пользователя</returns>
        Message FindMessageById(Guid messageId);
        /// <summary>
        /// Список сообщений пользователя в папке
        /// </summary>
        /// <param name="folderId">Id папки</param>
        /// <param name="userId">Id пользователя</param>
        /// <returns></returns>
        IEnumerable<Message> GetMessagesInFolder(int folderId, Guid userId);
        /// <summary>
        /// Удаление сообщений полльзователя
        /// </summary>
        /// <param name="userMessageIds"></param>
        void DeleteMessages(IEnumerable<Guid> userMessageIds);
        /// <summary>
        /// Перенос сообщений в Папку "Удаленные"
        /// </summary>
        /// <param name="userMessageIds">Id сообщений</param>
        void RemoveMessages(IEnumerable<Guid> userMessageIds);
        /// <summary>
        /// Отправка сообщения пользователям
        /// </summary>
        /// <param name="message">Id сообщения</param>
        /// <param name="userIds">Id пользователей</param>
        void SendMessage(Message message, IEnumerable<Guid> userIds);
        /// <summary>
        /// Пользователь прочитал сообщение
        /// </summary>
        /// <param name="userMessageId">Id сообщения пользователя</param>
        void UpdateMessageRead(Guid userMessageId);
        /// <summary>
        /// Перемещение сообщений между папками
        /// </summary>
        /// <param name="userMessageId">Id сообщения</param>
        /// <param name="folderId">Id папки</param>
        void MoveUserMessage(int folderId, Guid userMessageId);
    }
}