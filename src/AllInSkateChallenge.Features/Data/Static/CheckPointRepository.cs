using System.Collections.Generic;

namespace AllInSkateChallenge.Features.Data.Static
{
    public class CheckPointRepository : ICheckPointRepository
    {
        public List<CheckPointModel> Get()
        {
            return new List<CheckPointModel>
            {
                new CheckPointModel
                {
                    Distance = 0,
                    Title = "Leeds Basin",
                    Description = "Welcome to the Skate Challenge at the very start of the Leeds Liverpool Canal",
                    Longitude = -1.551093M,
                    Latitude = 53.793155M
                },
                new CheckPointModel
                {
                    Distance = 2.1M, // MAHA put 3m
                    Title = "Aire Valley Marina",
                    Description = "Congratulations, you have skated far enough to pass the Aire Valley Marina. The Marina has two canal entrances with pedestrian bridges which must be navigated to proceed.",
                    Longitude = -1.594375M,
                    Latitude = 53.803769M
                },
                new CheckPointModel
                {
                    Distance = 13,
                    Title = "Saltaire",
                    Description = "Congratulations, you have skated a half marathon! Did you know there is an Ice Cream Boat on the side of the canal that you can stop and refresh yourself at?",
                    Longitude = -1.789923M,
                    Latitude = 53.839548M
                },
                new CheckPointModel
                {
                    Distance = 16,
                    Title = "Bingley Five Rise Locks",
                    Description  = "Congratulations, you have reached Bingley Five Rise Locks. This stair case lock requires a boat passing through the lock to be raised or lowered in five stages, covering a height of 18.03m in 98.00m.",
                    Longitude = -1.838214M,
                    Latitude = 53.856368M,
                    Url = "https://en.wikipedia.org/wiki/Bingley_Five_Rise_Locks",
                    Image = "/images/BingleyFiveRiseLock.jpg",
                    ImageAlt = "Bingley 5 Rise Lock by Maree Coates"
                },
                new CheckPointModel
                {
                    Distance = 30,
                    Title = "Skipton Castle",
                    Description = "Congratulations, you have smashed a marathon and then some to reach Skipton Castle. Over 900 years old, Skipton Castle is one of the most complete and best preserved medieval castles.",
                    Longitude = -2.015390M,
                    Latitude = 53.963992M,
                    Url = "https://www.skiptoncastle.co.uk/"
                },
                new CheckPointModel
                {
                    Distance = 38,
                    Title = "East Marton Double Arched Bridge",
                    Description = "Congratulations, thanks to the wibbly wobbly nature of the canal, it's taken you 8 miles to traverse 5 to reach this bridge made of two separate arches stacked atop of each other.",
                    Longitude = -2.1398083M,
                    Latitude = 53.953686M,
                    Image = "/images/EastMartonDoubleArch.jpg",
                    ImageAlt = "The Double Arch, East Marton, Leeds Liverpool Canal by Phil Moon"
                },
                new CheckPointModel
                {
                    Distance = 47,
                    Title = "Foulridge Summit",
                    Description = "Congratulations, you have reached Foulridge.  This is where the Leeds and Liverpool Canal goes underground into the Foulridge tunnel which stretches for 1.49km, can you swim in skates?",
                    Longitude = -2.172585M,
                    Latitude = 53.878122M
                },
                new CheckPointModel
                {
                    Distance = 57,
                    Title = "Burnley",
                    Description = "Congratulations, you have reached Burnley.  Once again the canal goes underground, this time through Gannow Tunnel for 511m, Skaters can follow alternate footpaths to rejoin the canal.",
                    Longitude = -2.2679828M,
                    Latitude = 53.7890336M
                },
                new CheckPointModel
                {
                    Distance = 63,
                    Title = "Halfway There!",
                    Description = "Congratulations, you've reached the halfway point! Did you know that the halfway marker was added to the Leeds Liverpool Canal to mark it's 200th birthday?",
                    Longitude = -2.3955425M,
                    Latitude = 53.7576887M
                },
                new CheckPointModel
                {
                    Distance = 72,
                    Title = "Blackburn Flight",
                    Description = "Congratulations, you've reached Blackburn.",
                    Longitude = -2.477020M,
                    Latitude = 53.745922M
                },
                new CheckPointModel
                {
                    Distance = 93,
                    Title = "Wigan Pier",
                    Description = "Congratulations, you've reached Wigan Pier. The name has humorous or ironic connotations since it conjures an image of a seaside pleasure pier, whereas Wigan is in fact an inland and traditionally industrial town.",
                    Longitude = -2.6385824M,
                    Latitude = 53.5422293M,
                    Url = "https://en.wikipedia.org/wiki/Wigan_Pier"
                },
                new CheckPointModel
                {
                    Distance = 113,
                    Title = "The Scotch Piper Inn",
                    Description = "Congratulations, you're now a stones throw from the Scotch Piper Inn and half way between Southport and Liverpool. Do you want a pint before that last stretch to the finish line?",
                    Longitude = -2.957032M,
                    Latitude = 53.533165M,
                    Url = "https://en.wikipedia.org/wiki/Scotch_Piper_Inn"
                },
                new CheckPointModel
                {
                    Distance = 127.5M,
                    Title = "Liverpool Canning Dock",
                    Description = "Congratulations, You have reached the finish line! This is where the Leeds Liverpool Canal terminates.",
                    Longitude = -2.994955M,
                    Latitude = 53.403282M,
                    Image = "/images/CanningDock.jpg",
                    ImageAlt = "Canning Dock, Liverpool by Roger Ellis"
                }
            };
        }
    }
}
