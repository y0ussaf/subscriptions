using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Subscriptions.Application.Commands.AddOfferToPlan;

namespace Subscriptions.Api.Controllers
{
    public class OffersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OffersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> AddOffer(AddOfferToPlanCommand cmd)
        {
            var res = await _mediator.Send(cmd);
            return Ok();
        }
    }
}