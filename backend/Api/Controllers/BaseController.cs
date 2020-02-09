using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Controllers
{
    /// <summary>
    /// The base controller that initializes the mediator
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        private IMediator _mediator;

        /// <summary>
        /// 
        /// </summary>
        /// <param>IMediator</param>
        /// <typeparam>IMediator</typeparam>
        /// <returns></returns>
        protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());
    }
}