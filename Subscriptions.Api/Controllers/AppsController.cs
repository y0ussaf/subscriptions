using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Subscriptions.Application.Commands.RegisterBackendApp;

namespace Subscriptions.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AppsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppsController(IMediator mediator)
        {
            _mediator = mediator;
        }

         
        [HttpPost("Backend")]
        public async Task<ActionResult<RegisterBackendAppResponse>> RegisterBackendApp(RegisterBackendAppCommand registerBackendAppCommand)
        { 
            var response = await _mediator.Send(registerBackendAppCommand);
            
            return Ok(response);
        }
        
    }
}