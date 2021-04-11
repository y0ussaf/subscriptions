using System;
using Subscriptions.Domain.Common;
using Subscriptions.Domain.Entities;
using Xunit;

namespace Subscriptions.Domain.Tests.Domain
{
    public class PaidSubscriptionTests
    {
        private PaidSubscription Subscription { get; set; }
        public PaidSubscriptionTests()
        {
            var subscriber = new Subscriber(Guid.NewGuid().ToString());

            var offerId = Guid.NewGuid().ToString();
            var plan = new Plan();
            var expiration = new Expiration(1, TimeIn.Months);
            var price = 12;
            var paidOffer = new PaidOffer(offerId, plan, price, expiration);
            var subscriptionId = Guid.NewGuid().ToString();
            Subscription = new PaidSubscription(subscriptionId, subscriber, paidOffer);
        }
        
        [Fact]
        public void Paid_Subscription_With_AllCyclesShouldBePaid_Set_To_True_Should_Be_Valid_If_All_Cycles_Are_Paid()
        {
            Subscription.AllPaidCyclesShouldBePaid = true;
            var offer = Subscription.PaidOffer;
            var invoice1 = CreateInvoice(InvoiceStatus.Paid);
            var cycle1 = offer.CreatePaidCycle(Subscription,invoice1,new DateTime(2020,9,12));
            var invoice2 = CreateInvoice(InvoiceStatus.Paid);
            var cycle2 = offer.CreatePaidCycle(Subscription, invoice2, new DateTime(2020, 10, 12));
            Subscription.AddCycle(cycle1);
            Subscription.AddCycle(cycle2);
            Assert.True(Subscription.IsValid(new DateTime(2020,10,19)));
        }
        [Fact]
        public void Paid_Subscription_With_AllCyclesShouldBePaid_Set_To_True_Should_Not_Be_Valid_If_Any_Of_Cycles_Is_Not_Paid()
        {
            Subscription.AllPaidCyclesShouldBePaid = true;
            var offer = Subscription.PaidOffer;
            var invoice1 = CreateInvoice(InvoiceStatus.WaitingToBePaid);
            var cycle1 = offer.CreatePaidCycle(Subscription,invoice1,new DateTime(2020,9,12));
            var invoice2 = CreateInvoice(InvoiceStatus.Paid);
            var cycle2 = offer.CreatePaidCycle(Subscription, invoice2, new DateTime(2020, 10, 12));
            Subscription.AddCycle(cycle1);
            Subscription.AddCycle(cycle2);
            Assert.False(Subscription.IsValid(new DateTime(2020,10,19)));
        }

        [Fact]
        public void Trying_To_Add_Cycle_That_Its_Start_Datetime_Less_Than_The_Last_Cycle_EndTime_Throws_Exception()
        {
            var offer = Subscription.PaidOffer;
            var cycle1 = offer.CreatePaidCycle(Subscription, CreateInvoice(InvoiceStatus.Paid), new DateTime(2020, 9, 1));
            var cycle2 = offer.CreatePaidCycle(Subscription, CreateInvoice(InvoiceStatus.Paid),
                new DateTime(2020, 9, 15));
            Subscription.AddCycle(cycle1);

            void AddingCycleAction()
            {
                Subscription.AddCycle(cycle2);
            }
            Assert.Throws<Exception>(AddingCycleAction);
        }

        [Fact]
        public void Trying_To_Add_Cycle_After_UnClosed_FreeCycle_Throws_Exception()
        {
            var offer = Subscription.PaidOffer;
            var cycle1 = offer.CreateExpiredFreeCycleWithUndefinedEnd(Subscription, new DateTime(2020, 1, 1));
            var cycle2 = offer.CreatePaidCycle(Subscription, CreateInvoice(InvoiceStatus.WaitingToBePaid),
                new DateTime(2020,2,1));
            Subscription.AddCycle(cycle1);

            void AddCycleAction()
            {
                Subscription.AddCycle(cycle2);
            }

            Assert.Throws<Exception>(AddCycleAction);
        }
        public Invoice CreateInvoice(InvoiceStatus status)
        {
            return new(Guid.NewGuid().ToString(), status, true,
                new Payment(Guid.NewGuid().ToString()));
        }
    }
}