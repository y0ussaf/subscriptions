using System;
using System.Collections.Generic;
using Subscriptions.Domain.Common;

namespace Subscriptions.Domain.Entities
{
    public class IntervalDefinition
    {
        public IntervalDefinition()
        {
        }

        public long Id { get; set; }
        public int Order { get; set; }
        public Offer Offer { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public IEnumerable<Interval> Build(DateTime now)
        {
            throw new NotImplementedException();
        }
    }


}