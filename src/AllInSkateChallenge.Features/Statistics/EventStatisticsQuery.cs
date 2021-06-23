namespace AllInSkateChallenge.Features.Statistics
{
    using System;

    using MediatR;

    public class EventStatisticsQuery : IRequest<EventStatisticsQueryResponse>
    {
        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }
    }
}