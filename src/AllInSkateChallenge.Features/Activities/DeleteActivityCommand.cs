using System;

using AllInSkateChallenge.Features.Data.Entities;

namespace AllInSkateChallenge.Features.Activities
{
    public class DeleteActivityCommand
    {
        public ApplicationUser Skater { get; set; }

        public Guid MileageEntryId { get; set; }
    }
}
