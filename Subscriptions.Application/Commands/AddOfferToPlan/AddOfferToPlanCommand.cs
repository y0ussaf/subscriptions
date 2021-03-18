using MediatR;

namespace Subscriptions.Application.Commands.AddOfferToPlan
{
    public class AddOfferToPlanCommand : IRequest<AddOfferToPlanCommandResponse>
    {
        public string Name { get; set; }
        public string PlanId { get; set; }
        public uint? ExpireAfter { get; set; }
        public string ExpireAfterTimeIn { get; set; }
        public decimal? Price { get; set; }
    }
}