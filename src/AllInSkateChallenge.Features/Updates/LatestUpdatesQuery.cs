using System;
using System.Collections.Generic;

using AllInSkateChallenge.Features.Gravatar;

namespace AllInSkateChallenge.Features.Updates
{
    public class LatestUpdatesQuery : ILatestUpdatesQuery
    {
        private readonly IGravatarResolver gravatarResolver;

        public LatestUpdatesQuery(IGravatarResolver gravatarResolver)
        {
            this.gravatarResolver = gravatarResolver;
        }

        public MileageUpdateModel Get(int maximum = 10)
        {
            var now = DateTime.Now;

            return new MileageUpdateModel
            {
                Entries = new List<MileageUpdateEntryModel> 
                {
                    new MileageUpdateEntryModel { Logged = now.AddMinutes(-1), Skater = "Joe Bloggs", GravatarUrl = gravatarResolver.GetGravatarUrl("joe.bloggs@example.com"), Miles = 4.5M },
                    new MileageUpdateEntryModel { Logged = now.AddHours(-2), Skater = "Joe Bloggs", GravatarUrl = gravatarResolver.GetGravatarUrl("joe.bloggs@example.com"), Miles = 4.5M },
                    new MileageUpdateEntryModel { Logged = now.AddHours(-3), Skater = "Joe Bloggs", GravatarUrl = gravatarResolver.GetGravatarUrl("joe.bloggs@example.com"), Miles = 4.5M },
                    new MileageUpdateEntryModel { Logged = now.AddHours(-4), Skater = "Joe Bloggs", GravatarUrl = gravatarResolver.GetGravatarUrl("joe.bloggs@example.com"), Miles = 4.5M },
                    new MileageUpdateEntryModel { Logged = now.AddHours(-5), Skater = "Joe Bloggs", GravatarUrl = gravatarResolver.GetGravatarUrl("joe.bloggs@example.com"), Miles = 4.5M },
                    new MileageUpdateEntryModel { Logged = now.AddHours(-6), Skater = "Joe Bloggs", GravatarUrl = gravatarResolver.GetGravatarUrl("joe.bloggs@example.com"), Miles = 4.5M },
                    new MileageUpdateEntryModel { Logged = now.AddHours(-7), Skater = "Joe Bloggs", GravatarUrl = gravatarResolver.GetGravatarUrl("joe.bloggs@example.com"), Miles = 4.5M },
                    new MileageUpdateEntryModel { Logged = now.AddHours(-8), Skater = "Joe Bloggs", GravatarUrl = gravatarResolver.GetGravatarUrl("joe.bloggs@example.com"), Miles = 4.5M },
                    new MileageUpdateEntryModel { Logged = now.AddHours(-9), Skater = "Joe Bloggs", GravatarUrl = gravatarResolver.GetGravatarUrl("joe.bloggs@example.com"), Miles = 4.5M },
                    new MileageUpdateEntryModel { Logged = now.AddHours(-10), Skater = "Joe Bloggs", GravatarUrl = gravatarResolver.GetGravatarUrl("joe.bloggs@example.com"), Miles = 4.5M }
                }
            };
        }
    }
}
