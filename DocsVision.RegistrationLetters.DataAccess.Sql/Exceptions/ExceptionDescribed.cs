using System;

namespace DocsVision.RegistrationLetters.DataAccess.Sql.Exceptions
{
    public class ExceptionDescribed
    {
        public static string MessageIdIsNull => "Не указан Id сообщения.";
        public static string IdIsNegative => "Id не может быть меньше 0.";
        public static string GuidIsEmpty => "Guid не может быть пустым.";
        public static string DeletedIdIsNotSpecified => "Не указаны удаляемые идентификаторы сообщений.";
        public static string MessageIsNull => "Сообщение не может быть пустым.";
        public static string UserIdsIsNull => "Не указаны идентификаторы пользователей.";
        public static string UserMessageIdIsNull => "Не указан идентификатор сообщения пользователя.";
        public static string UserFolderNameIsNull => "Наименование дирректории не может быть пустой.";
        public static string EmailIsNull => "Email пользователя не может быть пустым.";
        public static string StringIsUnavailable(string str) => $"Пользователя  c: {str} недоступен.";
        public static string MessageIsUnavailable(Guid mes) => $"Сообщение c: {mes} недоступено.";


    }
}