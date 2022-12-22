using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Subscriptions.Application.Commands.AddPlan;
using Subscriptions.Application.Commands.CreatePlan;
using Subscriptions.Application.Commands.UpdatePlan;
using Subscriptions.Application.Queries.Plans.GetPlan;
using Subscriptions.Application.Queries.Plans.GetPlans;

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

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPlan(long id)
        {
            var query = new GetPlanQuery()
            {
                PlanId = id
            };
            var res = await _mediator.Send(query);
            return Ok(res);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public async Task<CreatedAtActionResult> CreatePlan(CreatePlanCommand cmd)
        {
            var res = await _mediator.Send(cmd);
            return CreatedAtAction(nameof(GetPlan),new {id = res.Id},null);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePlan(UpdatePlanCommand cmd)
        {
            var res = await _mediator.Send(cmd);
            return Ok(res);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetPlans([FromQuery] GetPlansQuery query)
        {
            var res = await _mediator.Send(query);
            return Ok(res);
        }
    }
}