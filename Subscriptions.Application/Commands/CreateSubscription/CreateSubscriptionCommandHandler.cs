using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Stripe;
using Subscriptions.Application.Common.Exceptions;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Application.Common.Services.Stripe.Interfaces;
using Subscriptions.Domain.Entities;
using Invoice = Subscriptions.Domain.Entities.Invoice;
using Subscription = Subscriptions.Domain.Entities.Subscription;

namespace Subscriptions.Application.Commands.CreateSubscription
{
    public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand,CreateSubscriptionCommandResponse>
    {
        private readonly IUnitOfWorkContext _unitOfWorkContext;
        private readonly ISubscriptionsRepository _subscriptionsRepository;
        private readonly IOffersRepository _offersRepository;
        private readonly IInvoicesRepository _invoicesRepository;
        private readonly ISubscribersRepository _subscribersRepository;
        private readonly IMapper _mapper;
        private readonly IPaymentIntentService _paymentIntentService;

        public CreateSubscriptionCommandHandler(
            IUnitOfWorkContext unitOfWorkContext,
            ISubscriptionsRepository subscriptionsRepository,
            IOffersRepository offersRepository,
            IInvoicesRepository invoicesRepository,
            ISubscribersRepository subscribersRepository,
            IMapper mapper,
            IPaymentIntentService paymentIntentService
        )
        {
            _unitOfWorkContext = unitOfWorkContext;
            _subscriptionsRepository = subscriptionsRepository;
            _offersRepository = offersRepository;
            _invoicesRepository = invoicesRepository;
            _subscribersRepository = subscribersRepository;
            _mapper = mapper;
            _paymentIntentService = paymentIntentService;
        }
        public async Task<CreateSubscriptionCommandResponse> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
        {
            await using (var unitOfWork = await _unitOfWorkContext.CreateUnitOfWork())
            {
                await unitOfWork.BeginWork();
                try
                {
                    var offer = await _offersRepository.GetOffer(request.OfferId);
                    var subscriber = await _subscribersRepository.GetSubscriber(request.SubscriberId);
                    if (offer is null)
                    {
                        throw new NotFoundException("");
                    }

                    if (subscriber is null)
                    {
                        throw new NotFoundException("");
                    }
                    
                    var subscriptionId = Guid.NewGuid().ToString();
                    var subscription = new Subscription(subscriptionId, subscriber,offer);
                    var now = DateTime.Now;
                    var firstTimelineDescription = offer.TimeLineDescriptions.First();
                    var timeLine = firstTimelineDescription.Build(now);
                    if (timeLine is PaidTimeLine paidTimeLine)
                    {
                        var paymentId = Guid.NewGuid().ToString();
                        var paymentIntent = await _paymentIntentService.CreateAsync(
                            new PaymentIntentCreateOptions
                            {
                                Amount = (long?) paidTimeLine.Price,
                                Currency = "usd",
                                Confirm = true,
                                PaymentMethod = subscriber.DefaultPaymentMethod.Id
                            },null, cancellationToken);
                            
                        var stripePayment = new StripePayment(paymentId,paymentIntent.Id);
                        var invoiceId = Guid.NewGuid().ToString();
                        var invoice = new Invoice(invoiceId, InvoiceStatus.WaitingToBePaid, paidTimeLine.AutoCharging,stripePayment);
                        await _invoicesRepository.StoreInvoice(invoice);
                    }
                    await _subscriptionsRepository.AddTimeline(subscriptionId,timeLine);
                    await unitOfWork.CommitWork();
                    var subscriptionDto = new SubscriptionDto();
                    _mapper.Map(subscription, subscriptionDto);
                    return new CreateSubscriptionCommandResponse()
                    {
                        Subscription = subscriptionDto
                    };
                }
                catch (Exception)
                {
                    await unitOfWork.RollBack();
                    throw;
                }
            }
        }
    }
}