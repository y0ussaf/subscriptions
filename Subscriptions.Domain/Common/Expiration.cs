using System.Collections.Generic;

namespace Subscriptions.Domain.Common
{
    public class Expiration : ValueObject
    {
        public Expiration(uint expireAfter, TimeIn expireAfterTimeIn)
        {
            ExpireAfter = expireAfter;
            ExpireAfterTimeIn = expireAfterTimeIn;
        }

        public uint ExpireAfter { get; private set; }
        public TimeIn ExpireAfterTimeIn { get; private set; }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ExpireAfter;
            yield return ExpireAfterTimeIn;
        }
    }
}