using System.Collections.Generic;

using AllInSkateChallenge.Features.Framework.Routing;

using Microsoft.AspNetCore.Mvc.Rendering;

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
                    SkateTarget = SkateTarget.None,
                    Distance = 0,
                    Title = "Leeds Canal Basin",
                    Description = "Start your Skate Challenge at the very start of the Leeds Liverpool Canal",
                    Longitude = -1.551093M,
                    Latitude = 53.793155M,
                    Image = absoluteUrlHelper.Get("/images/GreetingsFromLeedsCanalBasin.png")
                },
                new CheckPointModel
                {
                    SkateTarget = SkateTarget.AireValleyMarina,
                    Distance = 2.1M, // MAHA put 3m
                    Title = "Aire Valley Marina",
                    Description = "Congratulations, you've made it to the Aire Valley Marina. The Marina has two canal entrances with pedestrian bridges which must be navigated to proceed.",
                    Longitude = -1.594375M,
                    Latitude = 53.803769M,
                    Image = absoluteUrlHelper.Get("/images/GreetingsFromAireValleyMarina.png"),
                    DigitalBadge = absoluteUrlHelper.Get("/images/badges/AireValleyMarina.png")
                },
                new CheckPointModel
                {
                    SkateTarget = SkateTarget.Saltaire,
                    Distance = 13,
                    Title = "Saltaire",
                    Description = "Flippin eck, you have skated a half marathon! There's an Ice Cream Boat on the side of the canal for a quick snack on the move!",
                    Longitude = -1.789923M,
                    Latitude = 53.839548M,
                    Image = absoluteUrlHelper.Get("/images/GreetingsFromSaltaire.png"),
                    DigitalBadge = absoluteUrlHelper.Get("/images/badges/Saltaire.png")
                },
                new CheckPointModel
                {
                    SkateTarget = SkateTarget.BingleyFiveRiseLocks,
                    Distance = 16,
                    Title = "Bingley Five Rise Locks",
                    Description  = "Bravo! You have reached Bingley Five Rise Locks.  Boats passing through this stair case lock are raised or lowered in five stages, covering a height of 18.03m over 98.00m.  (That's almost 60 feet!)",
                    Longitude = -1.838214M,
                    Latitude = 53.856368M,
                    Url = "https://en.wikipedia.org/wiki/Bingley_Five_Rise_Locks",
                    Image = absoluteUrlHelper.Get("/images/GreetingsFromBingleyFiveRiseLocks.png"),
                    DigitalBadge = absoluteUrlHelper.Get("/images/badges/BingleyFiveRise.png")
                },
                new CheckPointModel
                {
                    SkateTarget = SkateTarget.SkiptonCastle,
                    Distance = 30,
                    Title = "Skipton Castle",
                    Description = "Awesome! You have smashed a marathon and then some to reach Skipton Castle. Over 900 years old, Skipton Castle is one of the most complete and best preserved medieval castles.",
                    Longitude = -2.015390M,
                    Latitude = 53.963992M,
                    Url = "https://www.skiptoncastle.co.uk/",
                    Image = absoluteUrlHelper.Get("/images/GreetingsFromSkiptonCastle.png"),
                    DigitalBadge = absoluteUrlHelper.Get("/images/badges/SkiptonCastle.png")
                },
                new CheckPointModel
                {
                    SkateTarget = SkateTarget.EastMartonDoubleArchedBridge,
                    Distance = 38,
                    Title = "East Marton Double Arched Bridge",
                    Description = "Nice work! Thanks to the wibbly wobbly nature of the canal, it's taken you 8 miles to actually go 5 miles to reach this bridge made of two separate arches stacked atop of each other.",
                    Longitude = -2.1398083M,
                    Latitude = 53.953686M,
                    Image = absoluteUrlHelper.Get("/images/GreetingsFromEastMarton.png"),
                    DigitalBadge = absoluteUrlHelper.Get("/images/badges/EastMarton.png")
                },
                new CheckPointModel
                {
                    SkateTarget = SkateTarget.FoulridgeSummit,
                    Distance = 47,
                    Title = "Foulridge Summit",
                    Description = "Hooray! You have reached Foulridge, the highest point on the Leeds-Liverpool Canal.  This is also where the canal goes underground into the Foulridge tunnel which stretches for 1.49km, can you swim in skates?",
                    Longitude = -2.172585M,
                    Latitude = 53.878122M,
                    Image = absoluteUrlHelper.Get("/images/GreetingsFromFoulridgeSummit.png"),
                    DigitalBadge = absoluteUrlHelper.Get("/images/badges/FoulridgeSummit.png")
                },
                new CheckPointModel
                {
                    SkateTarget = SkateTarget.Burnley,
                    Distance = 57,
                    Title = "Burnley",
                    Description = "Pat on the back! You have reached Burnley. Once again the canal goes underground, this time through Gannow Tunnel for 511m, Skaters can follow alternate footpaths to rejoin the canal.",
                    Longitude = -2.2679828M,
                    Latitude = 53.7890336M,
                    Image = absoluteUrlHelper.Get("/images/GreetingsFromBurnley.png"),
                    DigitalBadge = absoluteUrlHelper.Get("/images/badges/Burnley.png")
                },
                new CheckPointModel
                {
                    SkateTarget = SkateTarget.HalfwayThere,
                    Distance = 63,
                    Title = "Halfway There!",
                    Description = "Whooooaaaa! You've reached the halfway point! Did you know that the halfway marker was added to the Leeds Liverpool Canal to mark its 200th birthday?",
                    Longitude = -2.3955425M,
                    Latitude = 53.7576887M,
                    Image = absoluteUrlHelper.Get("/images/GreetingsFromHalfway.png"),
                    DigitalBadge = absoluteUrlHelper.Get("/images/badges/Halfway.png")
                },
                new CheckPointModel
                {
                    SkateTarget = SkateTarget.BlackburnFlight,
                    Distance = 72,
                    Title = "Blackburn Flight",
                    Description = "Good on you! You've reached the Blackburn flightwhere there are 7 locks in quick succession...  stop and watch the boats work their way through or skate on past at speed!",
                    Longitude = -2.477020M,
                    Latitude = 53.745922M,
                    Image = absoluteUrlHelper.Get("/images/GreetingsFromBlackburn.png"),
                    DigitalBadge = absoluteUrlHelper.Get("/images/badges/BlackburnFlight.png")
                },
                new CheckPointModel
                {
                    SkateTarget = SkateTarget.WiganPier,
                    Distance = 93,
                    Title = "Wigan Pier",
                    Description = "Top effort! You've reached Wigan Pier. The name has humorous or ironic connotations since it conjures an image of a seaside pleasure pier, whereas Wigan is in fact an inland and traditionally industrial town.",
                    Longitude = -2.6385824M,
                    Latitude = 53.5422293M,
                    Url = "https://en.wikipedia.org/wiki/Wigan_Pier",
                    Image = absoluteUrlHelper.Get("/images/GreetingsFromWiganPier.png"),
                    DigitalBadge = absoluteUrlHelper.Get("/images/badges/WiganPier.png")
                },
                new CheckPointModel
                {
                    SkateTarget = SkateTarget.TheScotchPiperInn,
                    Distance = 113,
                    Title = "The Scotch Piper Inn",
                    Description = "Hats off! You're now a stones throw from the Scotch Piper Inn, a thatched pub reputed to be the oldest inn in Lancashire and dates back to the 14th century. Originally the inn was built around an oak tree which can be seen in the tap room.  Do you want a pint before that last stretch to the finish line?",
                    Longitude = -2.957032M,
                    Latitude = 53.533165M,
                    Url = "https://en.wikipedia.org/wiki/Scotch_Piper_Inn",
                    Image = absoluteUrlHelper.Get("/images/GreetingsFromTheScotchPiperInn.png"),
                    DigitalBadge = absoluteUrlHelper.Get("/images/badges/ScotchPiper.png")
                },
                new CheckPointModel
                {
                    SkateTarget = SkateTarget.LiverpoolCanningDock,
                    Distance = 127.5M,
                    Title = "Liverpool Canning Dock",
                    Description = "YOU ARE AMAZING! You have reached the finish line and found yourself in Liverpool Docks! Time for a pint or a cuppa!",
                    Longitude = -2.994955M,
                    Latitude = 53.403282M,
                    Image = absoluteUrlHelper.Get("/images/GreetingsFromLiverpool.png"),
                    IsFinalCheckpoint = true,
                    DigitalBadge = absoluteUrlHelper.Get("/images/badges/Liverpool.png"),
                    FinisherDigitalBadge = absoluteUrlHelper.Get("/images/badges/Finisher.png")
                },
                new CheckPointModel
                {
                    SkateTarget = SkateTarget.ThereAndBackAgain,
                    Distance = 255M,
                    Title = "Leeds Canal Basin",
                    Description = "YOU ARE AMAZING! Not only have you reached the finish line, but you skated all the way home to Leeds! Time for a pint or a cuppa!",
                    Longitude = -1.551093M,
                    Latitude = 53.793155M,
                    Image = absoluteUrlHelper.Get("/images/GreetingsFromLeedsCanalBasin.png"),
                    IsFinalCheckpoint = true
                },
            };
        }

        public List<SelectListItem> GetSelectList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Aire Valley Marina (2.1 Miles)", Value = SkateTarget.AireValleyMarina.ToString("F") },
                new SelectListItem { Text = "Saltaire (13 Miles)", Value = SkateTarget.Saltaire.ToString("F") },
                new SelectListItem { Text = "Bingley Five Rise Locks (16 Miles)", Value = SkateTarget.BingleyFiveRiseLocks.ToString("F") },
                new SelectListItem { Text = "Skipton Castle (30 Miles)", Value = SkateTarget.SkiptonCastle.ToString("F") },
                new SelectListItem { Text = "East Marton Double Arched Bridge (38 Miles)", Value = SkateTarget.EastMartonDoubleArchedBridge.ToString("F") },
                new SelectListItem { Text = "Foulridge Summit (47 Miles)", Value = SkateTarget.FoulridgeSummit.ToString("F") },
                new SelectListItem { Text = "Burnley (57 Miles)", Value = SkateTarget.Burnley.ToString("F") },
                new SelectListItem { Text = "Halfway There (63 Miles)", Value = SkateTarget.HalfwayThere.ToString("F") },
                new SelectListItem { Text = "Blackburn Flight (72 Miles)", Value = SkateTarget.BlackburnFlight.ToString("F") },
                new SelectListItem { Text = "WiganPier (93 Miles)", Value = SkateTarget.WiganPier.ToString("F") },
                new SelectListItem { Text = "The Scotch Piper Inn (113 Miles)", Value = SkateTarget.TheScotchPiperInn.ToString("F") },
                new SelectListItem { Text = "Liverpool Canning Dock (127.5 Miles)", Value = SkateTarget.LiverpoolCanningDock.ToString("F") },
                new SelectListItem { Text = "There And Back Again (255 Miles)", Value = SkateTarget.ThereAndBackAgain.ToString("F") }
            };
        }
    }
}
