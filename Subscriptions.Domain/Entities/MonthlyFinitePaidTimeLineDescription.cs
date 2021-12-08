namespace Subscriptions.Domain.Entities
{
    public class MonthlyFinitePaidTimeLineDescription : PaidTimeLineDescription
    {
        public int StartingDay { get; set; } = 1;
    }
}