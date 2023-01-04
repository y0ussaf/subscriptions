using System;
using System.Threading.Tasks;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Commands.TransformInfiniteIntervalIntroFinite.Persistence
{
    public interface ITransformInfiniteTimelineIntoFinitePersistence
    {
        Task<Interval> GetInterval(string id);
        Task SetIntervalEnd(string id, DateTime now);
    }
}