using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Stripe;
using Subscriptions.Application.Common.Exceptions;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Application.Common.Services.Stripe.Interfaces;
using Subscriptions.Domain.Common;
using Subscriptions.Domain.Entities;
using Invoice = Subscriptions.Domain.Entities.Invoice;
using PaymentMethod = Stripe.PaymentMethod;
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
                    if (offer is null)
                    {
                        throw new NotFoundException("no offer found");
                    }
                    var id = Guid.NewGuid().ToString();
                    var subscription = new Subscription(id,offer,request.SubscriberId);
                    Cycle cycle = null;
                    var subscriber = await _subscribersRepository.GetSubscriber(request.SubscriberId);
                    if (subscriber is null)
                    {
                        throw new NotFoundException("no subscriber with this id");
                    }
                    switch (offer.OfferType)
                    {
                        case OfferType.Trial:
                        {
                            var trialOffer = (TrialOffer) offer;
                            if (trialOffer.TrialRequireCreditCard && subscriber.DefaultPaymentMethod is null)
                            {
                                throw new BadRequestException("this trial offer require a credit card");
                            }
                            var cycleDateTimeRange = trialOffer.Expiration.CreateDateTimeRangeFromExpiration();
                            cycle = new FreeExpiredCycle(subscription,cycleDateTimeRange);
                            await _subscriptionsRepository.StoreSubscription(subscription);
                            await _subscriptionsRepository.AddCycle(cycle);
                            break;
                        }
                        case OfferType.Paid:
                        {
                            var paidOffer = (PaidOffer) offer;
                            await _subscriptionsRepository.StoreSubscription(subscription);
                            if (paidOffer.OfferFreeCycle)
                            {
                                var cycleDateTimeRange = paidOffer.FreeCycle.CreateDateTimeRangeFromExpiration();
                                cycle = new FreeExpiredCycle(subscription, cycleDateTimeRange);
                            }
                            else
                            {
                                var cycleDateTimeRange = paidOffer.Expiration.CreateDateTimeRangeFromExpiration();
                                var paymentId = Guid.NewGuid().ToString();
                                var paymentIntent = await _paymentIntentService.CreateAsync(
                                    new PaymentIntentCreateOptions
                                    {
                                        Amount = paidOffer.Price,
                                        Currency = "usd",
                                        Confirm = true,
                                        PaymentMethod = subscriber.DefaultPaymentMethod.Id
                                    },null, cancellationToken);
                                
                                var stripePayment = new StripePayment(paymentId,paymentIntent.Id);
                                var invoiceId = Guid.NewGuid().ToString();
                                var invoice = new Invoice(invoiceId, InvoiceStatus.WaitingToBePaid, paidOffer.AutoCharging,stripePayment);
                                await _invoicesRepository.StoreInvoice(invoice);
                                cycle = new PaidCycle(subscription, cycleDateTimeRange,invoice);
                            }
                            
                            await _subscriptionsRepository.AddCycle(cycle);
                            break;
                        }
                        case OfferType.Free:
                        {
                            await _subscriptionsRepository.StoreSubscription(subscription);
                            cycle = new UnExpiredFreeCycle(subscription);
                            break;
                        }
                    }
                    await _subscriptionsRepository.AddCycle(cycle);
                    await unitOfWork.CommitWork();
                    var subscriptionDto = new SubscriptionDto();
                    _mapper.Map(subscription, subscriptionDto);
                    return new CreateSubscriptionCommandResponse
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