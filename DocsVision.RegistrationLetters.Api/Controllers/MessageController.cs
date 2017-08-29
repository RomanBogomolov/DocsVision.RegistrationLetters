using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using DocsVision.RegistrationLetters.Api.Message;
using DocsVision.RegistrationLetters.Api.Models;
using DocsVision.RegistrationLetters.DataAccess;
using DocsVision.RegistrationLetters.Log;

namespace DocsVision.RegistrationLetters.Api.Controllers
{
    [RoutePrefix("api/message")]
    public class MessageController : ApiController //: BaseApiController
    {
        private readonly IMessageRepository MessageRepository;
        private IUserRepository UserRepository;
        private ModelFactory _modelFactory;
        protected ModelFactory TheModelFactory => _modelFactory ?? (_modelFactory = new ModelFactory(Request));

        public MessageController(IMessageRepository messageRepository)
        {
            this.MessageRepository = messageRepository;
        }

//        public MessageController(IMessageRepository messageRepository, IUserRepository userRepository) :
//            base(messageRepository, userRepository)
//        {
//        }

        [HttpGet]
        [Route("{messageId:guid}", Name = "GetMessageById")]
        public IHttpActionResult GetMessageById(Guid messageId)
        {
            var messageInfo = MessageRepository.FindMessageById(messageId);
            return Ok(TheModelFactory.Create(messageInfo));
        }

        [HttpGet]
        [Route("user/{userId:guid}/folder/{folderId:int}")]
        public IHttpActionResult GetUserMessagesInFolder(Guid userId, int folderId)
        {
//            var user = UserRepository.FindById(userId);
//            if (user == null)
//            {
//                Logger.ServiceLog.Warn(ErrorMessageDescribed.UserIsAvailable(userId));
//                return BadRequest();
//            }
            var userMessages = MessageRepository.GetMessagesInFolder(folderId, userId);
            return Ok(TheModelFactory.Create(userMessages));
        }

        [HttpPost]
        [Route("send")]
        public IHttpActionResult SendMessageToUsers(MessageEmailsInputModel model)
        {
            IList<Guid> userIds = new List<Guid>();

            /* 
             * Лучше передать сразу массив
             */
//            foreach (var email in model.Emails)
//            {
//                var user = UserRepository.FindByEmail(email);
//
//                if (user == null)
//                {
//                    Logger.ServiceLog.Warn($"Пользователь {email} не зарегистрирован");
//                    return BadRequest($"Пользователь {email} не зарегистрирован");
//                }
//
//                userIds.Add(user.Id);
//            }
            var result = UserRepository.GetInvalidUserEmails(model.Emails);

            MessageRepository.SendMessage(model.Message, userIds);
            Logger.ServiceLog.Info(
                $"Пользователь {model.Message.Sender.Id} успешно отправил сообщения на {String.Join(",", model.Emails)}");

            return Ok();
        }

        [HttpDelete]
        [Route("delete")]
        public IHttpActionResult DeleteMessages(UserMessagesInputModel model)
        {
            var user = UserRepository.FindById(model.UserId);

            if (user == null)
            {
                Logger.ServiceLog.Warn($"Пользователь с {model.UserId} недоступен");
                return BadRequest("Пользователь недоступен");
            }

            /* 
             * Лучше передать сразу массив
             */
            foreach (var messageId in model.MessageIds)
            {
                var message = MessageRepository.FindMessageById(messageId);

                if (message == null)
                {
                    Logger.ServiceLog.Warn(
                        $"Не удалось загрузить сообщение {messageId} для пользователя {model.UserId}");
                    return BadRequest($"Не удалось загрузить сообщение {messageId}");
                }
            }

            //MessageRepository.DeleteMessages(obj.UserId, obj.MessageIds);
            Logger.ServiceLog.Info(
                $"Пользователь {model.UserId} успешно удалил сообщения {String.Join(",", model.MessageIds)}");
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}