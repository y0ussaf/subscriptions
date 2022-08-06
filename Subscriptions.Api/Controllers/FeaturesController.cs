using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Subscriptions.Application.Commands.CreateFeature;

namespace Subscriptions.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class FeaturesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FeaturesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public Task<IActionResult> GetFeature()
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public async Task<IActionResult> CreateFeature(CreateFeatureCommand cmd)
        {
            var res   = await _mediator.Send(cmd);
            return CreatedAtAction(nameof(GetFeature), new { res.Id }, res);
        }
    }
}