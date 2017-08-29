using System;

namespace DocsVision.RegistrationLetters.Api.Message
{
    public class ErrorMessageDescribed
    {
        public static string UserIsAvailable(Guid userId) => $"Пользователь с {userId} недоступен.";


    }
}