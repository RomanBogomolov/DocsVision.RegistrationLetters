using System;

namespace DocsVision.RegistrationLetters.Api.Message
{
    public class LoggerMessageDescribed
    {
        #region MessageController
        public static string UserIsAvailable(Guid userId) => $"Пользователь с {userId} недоступен.";
        public static string MessageIsAvailable(Guid messageId) => $"Сообщение с {messageId} недоступено.";
        public static string UserSendMessageError(Guid userId, string[] emails) =>
            $"Пользователь {userId} ввел невалидные email-ы: {string.Join(",", emails)}";
        public static string UserSendMessage(Guid userId, string[] emails) =>
            $"Пользователь {userId} успешно отправил сообщения на {string.Join(",", emails)}";
        #endregion
    }
}