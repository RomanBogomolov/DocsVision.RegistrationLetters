using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using DocsVision.RegistrationLetters.Api.Message;
using DocsVision.RegistrationLetters.Api.Models;
using DocsVision.RegistrationLetters.DataAccess;
using DocsVision.RegistrationLetters.Log;

namespace DocsVision.RegistrationLetters.Api.Controllers
{
    [RoutePrefix("api/message")]
    public class MessageController : BaseApiController
    {
        public MessageController(IMessageRepository messageRepository, IUserRepository userRepository, ILogger logger) :
            base(messageRepository, userRepository, logger)
        {
        }

        [HttpGet]
        [Route("{messageId:guid}", Name = "GetMessageById")]
        public IHttpActionResult GetMessageById(Guid messageId)
        {
            var messageInfo = MessageRepository.FindMessageById(messageId);

            if (messageInfo != null)
                return Ok(TheModelFactory.Create(messageInfo));

            Logger.Warn(LoggerMessageDescribed.MessageIsAvailable(messageId));
            return BadRequest(ModelStateErrorDescribed.InvalidMessage(ModelState));
        }

        [HttpGet]
        [Route("user/{userId:guid}/folder/{folderId:int}")]
        public IHttpActionResult GetUserMessagesInFolder(Guid userId, int folderId)
        {
            var user = UserRepository.FindById(userId);

            if (user == null)
            {
                Logger.Warn(LoggerMessageDescribed.UserIsAvailable(userId));
                return BadRequest(ModelStateErrorDescribed.InvalidMessage(ModelState));
            }

            var userMessages = MessageRepository.GetMessagesInFolder(folderId, userId);
            return Ok(TheModelFactory.Create(userMessages));
        }

        [HttpPost]
        [Route("send")]
        public IHttpActionResult SendMessageToUsers(MessageEmailsInputModel model)
        {
            var invalidEmails = UserRepository.GetInvalidUserEmails(model.Emails);
            var enumerable = invalidEmails as string[] ?? invalidEmails.ToArray();
            if (enumerable.Any())
            {
                Logger.Warn(LoggerMessageDescribed.UserSendMessageError(model.Message.Sender.Id, enumerable));
                return BadRequest(ModelStateErrorDescribed.InvalidEmails(ModelState, enumerable));
            }

            MessageRepository.SendMessage(model.Message, model.Emails);
            Logger.Info(LoggerMessageDescribed.UserSendMessage(model.Message.Sender.Id, enumerable));
            return Ok();
        }

        [HttpPut]
        [Route("{userMessageId:Guid}/move/{folderId:int}")]
        public IHttpActionResult MoveMessage(Guid userMessageId, int folderId)
        {
            MessageRepository.MoveUserMessage(folderId, userMessageId);
            return Ok();
        }
        
        [HttpPut]
        [Route("bucket")]
        public IHttpActionResult AddToBucket(UserMessagesInputModel model)
        {
            MessageRepository.RemoveMessages(model.MessageIds);
            return Ok();
        }
        
        [HttpDelete]
        [Route("delete")]
        public IHttpActionResult DeleteMessages(UserMessagesInputModel model)
        {
            MessageRepository.DeleteMessages(model.MessageIds);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPut]
        [Route("reading/{userMessageId:Guid}")]
        public IHttpActionResult UpdateRead(Guid userMessageId)
        {
            MessageRepository.UpdateMessageRead(userMessageId);
            return Ok();
        }
    }
}