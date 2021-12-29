using System.Collections.Generic;

namespace Subscriptions.Domain.Entities
{
    public class App
    {
        public App()
        {
            Plans = new List<Plan>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public AppType Type { get; set; }
        public Plan DefaultPlan { get; set; }
        public List<Plan> Plans { get; set; }
    }


    public enum AppType
    {
        Backend,
        Frontend
    }
}