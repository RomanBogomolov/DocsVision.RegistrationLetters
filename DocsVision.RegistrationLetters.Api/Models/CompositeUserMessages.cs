using System;
using System.Collections.Generic;

namespace DocsVision.RegistrationLetters.Api.Models
{
    /// <summary>
    /// Вспомогательный класс для передачи нескольких объектов (идентификатора пользователя и списка сообщений) в теле запроса
    /// </summary>
    public class CompositeUserMessages
    {
        public Guid UserId { get; set; }
        public IEnumerable<Guid> MessageIds { get; set; }
        
    }
}