using System.Collections.Generic;

namespace Subscriptions.Domain.Entities
{
    public class App
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Plan> Plans { get; set; }
    }
}