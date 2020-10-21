using System.Collections.Generic;

using AllInSkateChallenge.Features.Framework.Routing;

namespace AllInSkateChallenge.Features.Data.Static
{
    public class CheckPointRepository : ICheckPointRepository
    {
        private readonly IAbsoluteUrlHelper absoluteUrlHelper;

        public CheckPointRepository(IAbsoluteUrlHelper absoluteUrlHelper)
        {
            this.absoluteUrlHelper = absoluteUrlHelper;
        }

        public List<CheckPointModel> Get()
        {
            return new List<CheckPointModel>
            {
                new CheckPointModel
                {
                    Distance = 0,
                    Title = "Leeds Basin",
                    Description = "Start your Skate Challenge at the very start of the Leeds Liverpool Canal",
                    Longitude = -1.551093M,
                    Latitude = 53.793155M
                },
                new CheckPointModel
                {
                    Distance = 2.1M, // MAHA put 3m
                    Title = "Aire Valley Marina",
                    Description = "Congratulations, you've made it to the Aire Valley Marina. The Marina has two canal entrances with pedestrian bridges which must be navigated to proceed.",
                    Longitude = -1.594375M,
                    Latitude = 53.803769M
                },
                new CheckPointModel
                {
                    Distance = 13,
                    Title = "Saltaire",
                    Description = "Flippin eck, you have skated a half marathon! There's an Ice Cream Boat on the side of the canal for a quick snack on the move!",
                    Longitude = -1.789923M,
                    Latitude = 53.839548M
                },
                new CheckPointModel
                {
                    Distance = 16,
                    Title = "Bingley Five Rise Locks",
                    Description  = "Bravo! You have reached Bingley Five Rise Locks.  Boats passing through this stair case lock are raised or lowered in five stages, covering a height of 18.03m over 98.00m.  (That's almost 60 feet!)",
                    Longitude = -1.838214M,
                    Latitude = 53.856368M,
                    Url = "https://en.wikipedia.org/wiki/Bingley_Five_Rise_Locks",
                    Image = absoluteUrlHelper.Get("/images/BingleyFiveRiseLock.jpg"),
                    ImageAlt = "Bingley 5 Rise Lock by Maree Coates"
                },
                new CheckPointModel
                {
                    Distance = 30,
                    Title = "Skipton Castle",
                    Description = "Awesome! You have smashed a marathon and then some to reach Skipton Castle. Over 900 years old, Skipton Castle is one of the most complete and best preserved medieval castles.",
                    Longitude = -2.015390M,
                    Latitude = 53.963992M,
                    Url = "https://www.skiptoncastle.co.uk/"
                },
                new CheckPointModel
                {
                    Distance = 38,
                    Title = "East Marton Double Arched Bridge",
                    Description = "Nice work! Thanks to the wibbly wobbly nature of the canal, it's taken you 8 miles to actually go 5 miles to reach this bridge made of two separate arches stacked atop of each other.",
                    Longitude = -2.1398083M,
                    Latitude = 53.953686M,
                    Image = absoluteUrlHelper.Get("/images/EastMartonDoubleArch.jpg"),
                    ImageAlt = "The Double Arch, East Marton, Leeds Liverpool Canal by Phil Moon"
                },
                new CheckPointModel
                {
                    Distance = 47,
                    Title = "Foulridge Summit",
                    Description = "Hooray! You have reached Foulridge, the highest point on the Leeds-Liverpool Canal.  This is also where the canal goes underground into the Foulridge tunnel which stretches for 1.49km, can you swim in skates?",
                    Longitude = -2.172585M,
                    Latitude = 53.878122M
                },
                new CheckPointModel
                {
                    Distance = 57,
                    Title = "Burnley",
                    Description = "Pat on the back! You have reached Burnley. Once again the canal goes underground, this time through Gannow Tunnel for 511m, Skaters can follow alternate footpaths to rejoin the canal.",
                    Longitude = -2.2679828M,
                    Latitude = 53.7890336M
                },
                new CheckPointModel
                {
                    Distance = 63,
                    Title = "Halfway There!",
                    Description = "Whooooaaaa! You've reached the halfway point! Did you know that the halfway marker was added to the Leeds Liverpool Canal to mark its 200th birthday?",
                    Longitude = -2.3955425M,
                    Latitude = 53.7576887M
                },
                new CheckPointModel
                {
                    Distance = 72,
                    Title = "Blackburn Flight",
                    Description = "Good on you! You've reached the Blackburn flightwhere there are 7 locks in quick succession...  stop and watch the boats work their way through or skate on past at speed!",
                    Longitude = -2.477020M,
                    Latitude = 53.745922M
                },
                new CheckPointModel
                {
                    Distance = 93,
                    Title = "Wigan Pier",
                    Description = "Top effort! You've reached Wigan Pier. The name has humorous or ironic connotations since it conjures an image of a seaside pleasure pier, whereas Wigan is in fact an inland and traditionally industrial town.",
                    Longitude = -2.6385824M,
                    Latitude = 53.5422293M,
                    Url = "https://en.wikipedia.org/wiki/Wigan_Pier"
                },
                new CheckPointModel
                {
                    Distance = 113,
                    Title = "The Scotch Piper Inn",
                    Description = "Hats off! You're now a stones throw from the Scotch Piper Inn, a thatched pub reputed to be the oldest inn in Lancashire and dates back to the 14th century. Originally the inn was built around an oak tree which can be seen in the tap room.  Do you want a pint before that last stretch to the finish line?",
                    Longitude = -2.957032M,
                    Latitude = 53.533165M,
                    Url = "https://en.wikipedia.org/wiki/Scotch_Piper_Inn"
                },
                new CheckPointModel
                {
                    Distance = 127.5M,
                    Title = "Liverpool Canning Dock",
                    Description = "YOU ARE AMAZING! You have reached the finish line and found yourself in Liverpool Docks! Time for a pint or a cuppa!",
                    Longitude = -2.994955M,
                    Latitude = 53.403282M,
                    Image = absoluteUrlHelper.Get("/images/CanningDock.jpg"),
                    ImageAlt = "Canning Dock, Liverpool by Roger Ellis"
                }
            };
        }
    }
}
