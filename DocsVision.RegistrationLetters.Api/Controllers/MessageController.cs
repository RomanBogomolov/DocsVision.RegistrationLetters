using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using DocsVision.RegistrationLetters.Api.Models;
using DocsVision.RegistrationLetters.DataAccess;
using DocsVision.RegistrationLetters.Log;

namespace DocsVision.RegistrationLetters.Api.Controllers
{
    [RoutePrefix("api/message")]
    public class MessageController : ApiController
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;

        private ModelFactory _modelFactory;
        private ModelFactory TheModelFactory => _modelFactory ?? (_modelFactory = new ModelFactory(Request));

        public MessageController(IMessageRepository messageRepository, IUserRepository userRepository)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        [Route("user/{userId:guid}")]
        public IHttpActionResult GetUserMessages(Guid userId)
        {
            var user = _userRepository.FindById(userId);

            if (user == null)
            {
                Logger.ServiceLog.Warn($"Пользователь с {userId} недоступен");
                return BadRequest("Пользователь недоступен");
            }

            var userMessages = _messageRepository.GetMessages(userId);

            if (userMessages == null)
            {
                Logger.ServiceLog.Warn($"Не удалось загрузить сообщения для пользователя {userId}");
                return BadRequest("Не удалось загрузить сообщения");
            }

            return Ok(TheModelFactory.Create(userMessages));
        }

        [HttpGet]
        [Route("{messageId:guid}/user/{userId:Guid}", Name = "GetMessageById")]
        public IHttpActionResult GetMessageInfo(Guid messageId, Guid userId)
        {
            var messageInfo = _messageRepository.GetMessageInfo(messageId);

            if (messageInfo == null)
            {
                Logger.ServiceLog.Warn($"Не удалось загрузить сообщение {messageId}");
                return BadRequest("Не удалось загрузить сообщение");
            }

            _messageRepository.UpdateMessageRead(messageId, userId);

            return Ok(TheModelFactory.Create(messageInfo));
        }

        [HttpPost]
        [Route("send")]
        public IHttpActionResult SendMessageToUsers(CompositeMessageEmails obj)
        {
            IList<Guid> userIds = new List<Guid>();

            foreach (var email in obj.Emails)
            {
                var user = _userRepository.FindByEmail(email);

                if (user == null)
                {
                    Logger.ServiceLog.Warn($"Пользователь {email} не зарегистрирован");
                    return BadRequest($"Пользователь {email} не зарегистрирован");
                }

                userIds.Add(user.Id);
            }

            _messageRepository.SendMessage(obj.Message, userIds);
            Logger.ServiceLog.Info($"Пользователь {obj.Message.User.Id} успешно отправил сообщения на {String.Join(",", obj.Emails)}");
            
            return Ok();
        }

        [HttpDelete]
        [Route("delete")]
        public IHttpActionResult DeleteMessages(CompositeUserMessages obj)
        {
            var user = _userRepository.FindById(obj.UserId);

            if (user == null)
            {
                Logger.ServiceLog.Warn($"Пользователь с {obj.UserId} недоступен");
                return BadRequest("Пользователь недоступен");
            }

            foreach (var messageId in obj.MessageIds)
            {
                var message = _messageRepository.GetMessageInfo(messageId);

                if (message == null)
                {
                    Logger.ServiceLog.Warn($"Не удалось загрузить сообщение {messageId} для пользователя {obj.UserId}");
                    return BadRequest($"Не удалось загрузить сообщение {messageId}");
                }
            }

            _messageRepository.DeleteMessages(obj.UserId, obj.MessageIds);
            Logger.ServiceLog.Info($"Пользователь {obj.UserId} успешно удалил сообщения {String.Join(",", obj.MessageIds)}");
            return StatusCode(HttpStatusCode.NoContent);
        }

    }
}