using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Subscriptions.Application.Commands.AddPlan;
using Subscriptions.Application.Commands.UpdatePlan;

namespace Subscriptions.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PlansController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PlansController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<CreatePlanCommandResponse>> CreatePlan(CreatePlanCommand cmd)
        {
            var res = await _mediator.Send(cmd);
            return Ok(res);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePlan(UpdatePlanCommand cmd)
        {
            var res = await _mediator.Send(cmd);
            return Ok(res);
        }
    }
}