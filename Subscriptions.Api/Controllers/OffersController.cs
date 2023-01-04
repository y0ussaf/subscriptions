using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Subscriptions.Application.Commands.AddOfferToPlan;
using Subscriptions.Application.Queries.Offers.GetOffer;
using Subscriptions.Application.Queries.Offers.GetOffers;

namespace Subscriptions.Api.Controllers
{
    [ApiController]

    [Route("api/v1/[controller]")]
    public class OffersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OffersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetOffers([Required] int planId,[FromQuery] GetOffersQuery query)
        {
            query.PlanId = planId;
            var res = await _mediator.Send(query);
            return Ok(res);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddOffer(AddOfferToPlanCommand cmd)
        {
            var res = await _mediator.Send(cmd);
            return CreatedAtAction(nameof(GetOffer),new {res.Id});
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOffer(GetOfferQuery query)
        {
            var res = await _mediator.Send(query);
            return Ok(res);
        }

    }
}