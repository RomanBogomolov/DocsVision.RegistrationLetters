using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DocsVision.RegistrationLetters.Api.Models;
using DocsVision.RegistrationLetters.DataAccess;

namespace DocsVision.RegistrationLetters.Api.Controllers
{
    public class BaseApiController : ApiController
    {
        private ModelFactory _modelFactory;
        protected ModelFactory TheModelFactory => _modelFactory ?? (_modelFactory = new ModelFactory(Request));

        protected readonly IMessageRepository MessageRepository;
        protected readonly IUserRepository UserRepository;

        public BaseApiController(IMessageRepository messageRepository, IUserRepository userRepository)
        {
            MessageRepository = messageRepository;
            UserRepository = userRepository;
        }
    }
}
