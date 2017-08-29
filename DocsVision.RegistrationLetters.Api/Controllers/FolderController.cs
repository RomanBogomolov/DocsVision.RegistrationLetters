using System;
using System.Web.Http;
using DocsVision.RegistrationLetters.Api.Models;
using DocsVision.RegistrationLetters.DataAccess;

namespace DocsVision.RegistrationLetters.Api.Controllers
{
    [RoutePrefix("api/folder")]
    public class FolderController : ApiController
    {
        private readonly IUserFolderRepository _userFolder;

        public FolderController(IUserFolderRepository userFolder)
        {
            _userFolder = userFolder;
        }

        [HttpGet]
        [Route("user/{userId:guid}")]
        public IHttpActionResult GetUserFolders(Guid userId)
        {
            var userFolders = _userFolder.GetUserFolders(userId);
            return Ok(userFolders);
        }

        [HttpPost]
        [Route("create")]
        public IHttpActionResult CreateFolder(CreateFolderInputModel model)
        {
            _userFolder.CreateFolder(model.UserId, model.Name, model.ParentId);
            return Ok();
        }

        /* Поправить! */
        [HttpDelete]
        [Route("{folderId:int}/user/{userId:guid}/delete")]
        public IHttpActionResult DeleteFolder(int folderId, Guid userId)
        {
            _userFolder.DeleteFolder(folderId, userId);
            return Ok();
        }
    }
}
