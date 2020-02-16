using System.Net;
using System.Threading.Tasks;
using Application.Entities.UserEntity.Query.GetUserDetailCommand;
using Application.Infrastructure.RequestResponsePipeline;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// Controller for handling Users Information 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        /// <summary>
        /// Updates user's detail information
        /// </summary>
        /// <remarks>Good!</remarks>
        [HttpGet("get-userdetail")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetUsersAboutMe([FromQuery]GetUserDetailCommand command) {
            command.setTokens(User);
            return Ok(await Mediator.Send(command));
            // return Ok(command);
        }

    }
}