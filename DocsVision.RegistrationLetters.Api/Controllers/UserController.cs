using System;
using System.Web.Http;
using DocsVision.RegistrationLetters.DataAccess;
using DocsVision.RegistrationLetters.Log;

namespace DocsVision.RegistrationLetters.Api.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IHttpActionResult GetUserById(Guid id)
        {
            var user = _userRepository.FindById(id);

            if (user != null)
            {
                return Ok(user);
            }

            Logger.ServiceLog.Warn($"Не удалось найти пользователя с {id}");
            return BadRequest("Не удалось найти пользователя");
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetUserByEmail([FromUri] string email)
        {
            var user = _userRepository.FindByEmail(email);

            if (user != null)
            {
                return Ok(user);
            }

            Logger.ServiceLog.Warn($"Не удалось найти пользователя с {email}");
            return BadRequest("Не удалось найти пользователя");
        }

    }
}
