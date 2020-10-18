using System;

using AllInSkateChallenge.Features.Data.Entities;

using MediatR;

namespace AllInSkateChallenge.Features.Activities
{
    public class DeleteActivityCommand : IRequest
    {
        public ApplicationUser Skater { get; set; }

        public Guid MileageEntryId { get; set; }
    }
}
