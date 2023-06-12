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
                    SkateTarget = SkateTarget.CornExchange,
                    DistanceInKilometers = 0,
                    Title = "The Corn Exchange",
                    Description = "Ey Up! Welcome to the Leeds and the Roller Girl Gang shop!  We are looking forward to showing you our city's sights and local skate hangouts.  The Roller Girl Gang shop is our favourite place to get all our skate gear (obvs!)  The shop is situated in the beautiful historic Corn Exchange building. Built in 1861 for trading corn, the building is now home to a number of independent businesses.",
                    Longitude = -1.54018M,
                    Latitude = 53.79597M,
                    Image = absoluteUrlHelper.Get("/rggeventone/checkpoint-photo/roller-girl-gang.jpg"),
                    DigitalBadge = absoluteUrlHelper.Get("/rggeventone/checkpoint-badge/corn-exchange-rgg-purple.jpg"),
                    Url = "https://en.wikipedia.org/wiki/Leeds_Corn_Exchange"
                },
                new CheckPointModel
                {
                    SkateTarget = SkateTarget.SoveriegnSquare,
                    DistanceInKilometers = 0.9M,
                    Title = "Sovereign Square",
                    Description = "Yay! you made it to the first stop! This is Sovereign Square- It's a fab space to skate, jam, picnic and hang out during the Summer.",
                    Longitude = -1.544647M,
                    Latitude = 53.793662M,
                    Image = absoluteUrlHelper.Get("/rggeventone/checkpoint-photo/sovereign-square.jpg"),
                    DigitalBadge = absoluteUrlHelper.Get("/rggeventone/checkpoint-badge/soverign-square-teal.jpg"),
                    Url = "https://www.tripadvisor.co.uk/Attraction_Review-g186411-d23485244-Reviews-Sovereign_Square-Leeds_West_Yorkshire_England.html"
                },
                new CheckPointModel
                {
                    SkateTarget = SkateTarget.GranaryWharf,
                    DistanceInKilometers = 1.3M,
                    Title = "Granary Wharf",
                    Description = "Always a favourite stop on skate roll outs, Granary Wharf is a lovely waterfront spot on the Leeds- Liverpool Canal. There are plenty of bars to stop and have a drink at, as well as some cool light installations for insta ready selfies.",
                    Longitude = -1.54789M,
                    Latitude = 53.79362M,
                    Image = absoluteUrlHelper.Get("/rggeventone/checkpoint-photo/granary-wharf.jpg"),
                    DigitalBadge = absoluteUrlHelper.Get("/rggeventone/checkpoint-badge/granary-wharf-teal.jpg"),
                    Url = "https://www.visitleeds.co.uk/things-to-do/arts-culture/granary-wharf/"
                },
                new CheckPointModel
                {
                    SkateTarget = SkateTarget.TetleyBreweryWharf,
                    DistanceInKilometers = 2.3M,
                    Title = "Tetley Brewery Warf",
                    Description  = "We've travelled along the River to Tetley Brewery Wharf, formally the headquarters of the Tetley Brewery, this majestic building now houses an art gallery and also a nice cafe.",
                    Longitude = -1.53727M,
                    Latitude = 53.79391M,
                    Image = absoluteUrlHelper.Get("/rggeventone/checkpoint-photo/tetley-brewery.jpg"),
                    DigitalBadge = absoluteUrlHelper.Get("/rggeventone/checkpoint-badge/tetley-teal.jpg"),
                    Url = "https://thetetley.org/"
                },
                new CheckPointModel
                {
                    SkateTarget = SkateTarget.LeedsIndustrialMuseum,
                    DistanceInKilometers = 6,
                    Title = "Leeds Industrial Museum at Armley Mills",
                    Description = "As we begin to head out of the city centre along the canal we've arrived at Leeds Industrial Museum.  In its heyday during the industrial revolution, Leeds was a big player in the textile industry - you can learn about it all here.",
                    Longitude = -1.58271M,
                    Latitude = 53.80257M,
                    Image = absoluteUrlHelper.Get("/rggeventone/checkpoint-photo/leeds-industrail-museum.jpg"),
                    DigitalBadge = absoluteUrlHelper.Get("/rggeventone/checkpoint-badge/armley-mills-teal.jpg"),
                    Url = "https://museumsandgalleries.leeds.gov.uk/leeds-industrial-museum/"
                },
                new CheckPointModel
                {
                    SkateTarget = SkateTarget.ArmleyPark,
                    DistanceInKilometers = 7.8M,
                    Title = "Armley Park",
                    Description = "Welcome to Armley park!  You can have a nice skate through the gardens here.  The park has a number of historic features, including this fountain which was built in 1897.",
                    Longitude = -1.59544M,
                    Latitude = 53.80158M,
                    Image = absoluteUrlHelper.Get("/rggeventone/checkpoint-photo/armley-park.jpg"),
                    DigitalBadge = absoluteUrlHelper.Get("/rggeventone/checkpoint-badge/armley-park-teal.jpg"),
                    Url = "https://www.visitleeds.co.uk/things-to-do/outdoors/armley-park-leeds/"
                },
                new CheckPointModel
                {
                    SkateTarget = SkateTarget.EllandRoad,
                    DistanceInKilometers = 10.8M,
                    Title = "Elland Road",
                    Description = "Amazing! You have skated 10 kilometers and reached the home of Leeds United Football Club, Elland Road.  The club competes at the highest level of football in England, the Premier league and have been based at this site since 1919",
                    Longitude = -1.57214M,
                    Latitude = 53.77781M,
                    Image = absoluteUrlHelper.Get("/rggeventone/checkpoint-photo/elland-road.jpg"),
                    DigitalBadge = absoluteUrlHelper.Get("/rggeventone/checkpoint-badge/elland-road-purple.jpg"),
                    Url = "https://www.leedsunited.com/"
                },
                new CheckPointModel
                {
                    SkateTarget = SkateTarget.MiddletonRailway,
                    DistanceInKilometers = 13.6M,
                    Title = "The Middleton Railway",
                    Description = "Choo choo! - you are now at Middleton Railway!  Now preserved as a heritage railway, the railway was founded in 1758 to transport coal from the coal pits to the River Aire.",
                    Longitude = -1.53833M,
                    Latitude = 53.77472M,
                    Image = absoluteUrlHelper.Get("/rggeventone/checkpoint-photo/middleton-railway.jpg"),
                    DigitalBadge = absoluteUrlHelper.Get("/rggeventone/checkpoint-badge/middleton-railway-teal.jpg"),
                    Url = "https://www.middletonrailway.org.uk/"
                },
                new CheckPointModel
                {
                    SkateTarget = SkateTarget.Carlton,
                    DistanceInKilometers = 19.9M,
                    Title = "Carlton",
                    Description = "Well, you have now skated 20K and made it to.....the middle of nowhere!   This tiny village is a point in the Rhubarb Triangle, a small area (9 square miles)  in Yorkshire that has a protected status for the growing of forced rhubarb.  Originally from Serbia, rhubarb loves the Yorkshire weather and here in Carlon, there is a small farm shop where you can buy some - don't worry if rhubarb ain't your thing they do a fab butty too!",
                    Longitude = -1.49093M,
                    Latitude = 53.73986M,
                    Image = absoluteUrlHelper.Get("/rggeventone/checkpoint-photo/carlton-rubarb-triangle-farm-shop.jpg"),
                    DigitalBadge = absoluteUrlHelper.Get("/rggeventone/checkpoint-badge/carlton-purple.jpg"),
                    Url = "https://theyorkshiresociety.org/the-story-of-the-yorkshire-rhubarb-triangle/"
                },
                new CheckPointModel
                {
                    SkateTarget = SkateTarget.TempleNewsamPark,
                    DistanceInKilometers = 29.3M,
                    Title = "Temple Newsam Park",
                    Description = "OMG - you have totally completed a half marathon go you!  And what better place to celebrate than the impressive Temple Newsam Estate - a stately home with landscaped gardens, a small farm and a a huge area of woodland, ponds and fields to stretch your legs. It is a huge green space within a city and provides a welcome break from the bustle of the city.",
                    Longitude = -1.45505M,
                    Latitude = 53.7901M,
                    Image = absoluteUrlHelper.Get("/rggeventone/checkpoint-photo/temple_newsam_park.jpg"),
                    DigitalBadge = absoluteUrlHelper.Get("/rggeventone/checkpoint-badge/temple-newsham-green.jpg"),
                    Url = "https://museumsandgalleries.leeds.gov.uk/temple-newsam/visit/"
                },
                new CheckPointModel
                {
                    SkateTarget = SkateTarget.LsTen,
                    DistanceInKilometers = 35.5M,
                    Title = "LS-TEN",
                    Description = "We have arrived at the only indoor skate park in Leeds!  With a lovely mini ramp, unique bowl and new concrete outdoor skate area you there is something to challenge all levels of skater.",
                    Longitude = -1.53415M,
                    Latitude = 53.7858M,
                    Image = absoluteUrlHelper.Get("/rggeventone/checkpoint-photo/ls-ten.jpg"),
                    DigitalBadge = absoluteUrlHelper.Get("/rggeventone/checkpoint-badge/ls-ten-teal.jpg")
                },
                new CheckPointModel
                {
                    SkateTarget = SkateTarget.RoyalArmouriesMuseum,
                    DistanceInKilometers = 36.7M,
                    Title = "Royal Armouries Museum",
                    Description = "As we approach the city of Leeds again we have arrived at The Royal Armouries -  featuring over 5000 years of arms and armour, with over 75,000 objects in this world-renowned collection.  We also love to skate the docks area nearby.",
                    Longitude = -1.53223M,
                    Latitude = 53.79178M,
                    Image = absoluteUrlHelper.Get("/rggeventone/checkpoint-photo/royal-armouries.jpg"),
                    DigitalBadge = absoluteUrlHelper.Get("/rggeventone/checkpoint-badge/royal-armouries-teal.jpg"),
                    Url = "https://royalarmouries.org/venue/royal-armouries-museum/"
                },
                new CheckPointModel
                {
                    SkateTarget = SkateTarget.KirkgateMarket,
                    DistanceInKilometers = 37.9M,
                    Title = "Kirkgate Market",
                    Description = "Back in the hustle and bustle of the city stands one of the largest covered market in Europe, Kirkgate market is located in a stunning Victorian building with lots of architectural features that elevate the space. It has been a shopping destination with a huge variety of vendors for over a century.  We held a pop-up roller disco for Child Friendly Leeds in the event space in 2021.",
                    Longitude = -1.53926M,
                    Latitude = 53.79862M,
                    Image = absoluteUrlHelper.Get("/rggeventone/checkpoint-photo/kirkgate-market.jpg"),
                    DigitalBadge = absoluteUrlHelper.Get("/rggeventone/checkpoint-badge/kirkgate-market-teal.jpg"),
                    Url = "https://www.leeds.gov.uk/leedsmarkets/Home"
                },
                new CheckPointModel
                {
                    SkateTarget = SkateTarget.LeedsGrandTheatre,
                    DistanceInKilometers = 38.2M,
                    Title = "The Grand Theatre & Opera House Leeds",
                    Description = "We have arrived at The Grand Theatre, built in 1878, it is well known for its opulent interior.  You can see all the big touring theatre shows here, as well as the UK's favorite comedians on tour.",
                    Longitude = -1.54111M,
                    Latitude = 53.79999M,
                    Image = absoluteUrlHelper.Get("/rggeventone/checkpoint-photo/leeds-grand-theatre.jpg"),
                    DigitalBadge = absoluteUrlHelper.Get("/rggeventone/checkpoint-badge/grand-theatre-teal.jpg"),
                    Url = "https://leedsheritagetheatres.com/leeds-grand-theatre/"
                },
                new CheckPointModel
                {
                    SkateTarget = SkateTarget.MilleniumSquare,
                    DistanceInKilometers = 39,
                    Title = "Millennium Square",
                    Description = "Our last stop in the city centre is Millenium Square, whilst it's not ideal for roller skating, as it is on a hill (as much of Leeds is!)  it is a large open space in the centre of Leeds, often used as an informal meeting point or to hold large scale city events.  Millenium square is a space we pass through as part of our Summer Solstice route.  More confident skaters will practise their hill control or jump off the steps at the top, while others use it as a space to catch their breath while the group gathers back together.",
                    Longitude = -1.54834M,
                    Latitude = 53.80149M,
                    Image = absoluteUrlHelper.Get("/rggeventone/checkpoint-photo/millennium-square.jpg"),
                    DigitalBadge = absoluteUrlHelper.Get("/rggeventone/checkpoint-badge/millenium-square-teal.jpg"),
                    Url = "https://www.rollergirlgang.co.uk/learn"
                },
                new CheckPointModel
                {
                    SkateTarget = SkateTarget.RamgarhiaSikhSportsCentre,
                    DistanceInKilometers = 40.5M,
                    Title = "Ramgarhia Sikh Sports Centre",
                    Description = "We've skated 40K and have arrived at our home skate space. Mel used to play roller derby here over a decade ago and the relationship we established with the folks at the Sikh Centre  is one we cherish.  After lockdown, this was one of the last places to reopen their doors.  Many of the Sikh community elders are in the vulnerable group, so protecting them from Covid-19 was vital.  We now use the Sikh Sports centre to hold our weekly classes.  It's lovely to have had such a long relationship with a space, if you are a local, come join us sometime!",
                    Longitude = -1.53449M,
                    Latitude = 53.80856M,
                    Image = absoluteUrlHelper.Get("/rggeventone/checkpoint-photo/ramgarhia.jpg"),
                    DigitalBadge = absoluteUrlHelper.Get("/rggeventone/checkpoint-badge/ramgarhia-purple.jpg"),
                    Url = "https://www.rollergirlgang.co.uk/learn"
                },
                new CheckPointModel
                {
                    SkateTarget = SkateTarget.PotternewtonPark,
                    DistanceInKilometers = 42.2M,
                    Title = "Potternewton Park",
                    Description = "Congratulations you have now skated a marathon!  This milestone brings us to a very special space for the Leeds skate community.  During lockdown, the super smooth tennis courts became a popular skate destination and the Potternewton Rollers group was established.  It is one of the best outdoor spaces to have a jam in Leeds. If you are visiting Leeds in August, you might be able to catch the Leeds Carnival here, started by the West Indian community in 1967, it was the first carnival parade of its type in the UK.",
                    Longitude = -1.52471M,
                    Latitude = 53.81958M,
                    Image = absoluteUrlHelper.Get("/rggeventone/checkpoint-photo/potternewton-park.jpg"),
                    DigitalBadge = absoluteUrlHelper.Get("/rggeventone/checkpoint-badge/potternewton-park-green.jpg"),
                    Url = "https://en.wikipedia.org/wiki/Potternewton_Park"
                },
                new CheckPointModel
                {
                    SkateTarget = SkateTarget.MeanwoodValleyUrbanFarm,
                    DistanceInKilometers = 45,
                    Title = "Meanwood Valley Urban Farm",
                    Description = "This is another little green haven within the city of Leeds.  This farm is a lovely place to bring the family and see all the farm animals, as well as some small animals for petting.",
                    Longitude = -1.55264M,
                    Latitude = 53.82056M,
                    Image = absoluteUrlHelper.Get("/rggeventone/checkpoint-photo/meanwood-valley-urban-farm.jpg"),
                    DigitalBadge = absoluteUrlHelper.Get("/rggeventone/checkpoint-badge/meanwood-valley-urban-farm-teal.jpg"),
                    Url = "https://www.mvuf.org.uk/"
                },
                new CheckPointModel
                {
                    SkateTarget = SkateTarget.YorkshireCricketGround,
                    DistanceInKilometers = 47.4M,
                    Title = "Yorkshire Cricket Ground",
                    Description = "We have arrived at Headingley Stadium located in student hotspot Headingley.  The Stadium complex contains the fifth largest cricket ground in the UK - the home of Yorkshire Cricket Club as well as a Rugby Staduim which is home to Leeds Rhinos Rugby League Club.",
                    Longitude = -1.58173M,
                    Latitude = 53.81774M,
                    Image = absoluteUrlHelper.Get("/rggeventone/checkpoint-photo/headingley-stadium.jpg"),
                    DigitalBadge = absoluteUrlHelper.Get("/rggeventone/checkpoint-badge/headingley-stadium-teal.jpg"),
                    Url = "https://en.wikipedia.org/wiki/Headingley_Cricket_Ground"
                },
                new CheckPointModel
                {
                    SkateTarget = SkateTarget.KirkstallAbbey,
                    DistanceInKilometers = 49.6M,
                    Title = "Kirkstall Abbey",
                    Description = "You have reached a massive milestone!  50K skated!  Well done, you are also halfway to the finish!  We are at one of the oldest landmarks in Leeds, Kirkstall Abbey.  Founded in 1152, the Abbey is now a ruin, but the paths are skatable (just mind the hill), and the surrounding grounds and river make a beautiful backdrop.",
                    Longitude = -1.60677M,
                    Latitude = 53.82076M,
                    Image = absoluteUrlHelper.Get("/rggeventone/checkpoint-photo/kirkstall-abbey.jpg"),
                    DigitalBadge = absoluteUrlHelper.Get("/rggeventone/checkpoint-badge/kirkstall-abbey-purple.jpg"),
                    Url = "https://museumsandgalleries.leeds.gov.uk/kirkstall-abbey/"
                },
                new CheckPointModel
                {
                    SkateTarget = SkateTarget.SunnyBankMillsGallery,
                    DistanceInKilometers = 55.5M,
                    Title = "Sunny Bank Mills Gallery",
                    Description = "We have reached the suburb of Farsley, home of Sunny Bank Mills, built in 1829 The Mill was famous for its production of fine worsted cloths (the material that really expensive suits are made from).  Today the mills have been turned into office space, but there is an Art Gallery and textile archive for visitors.  You can also play Bongo's bingo once a month here.",
                    Longitude = -1.67085M,
                    Latitude = 53.81422M,
                    Image = absoluteUrlHelper.Get("/rggeventone/checkpoint-photo/sunnybank-mill.jpg"),
                    DigitalBadge = absoluteUrlHelper.Get("/rggeventone/checkpoint-badge/sunnybank-mill-teal.jpg"),
                    Url = "https://www.sunnybankmills.co.uk/"
                },
                new CheckPointModel
                {
                    SkateTarget = SkateTarget.BrownleeCentre,
                    DistanceInKilometers = 63.6M,
                    Title = "The Brownlee Centre",
                    Description = "At 60K reached why not stretch your legs and learn to control your speed on hills!  The super smooth cycle track at the Brownlee centre (named after Leeds Triathlon legends Alistair and Jonny Brownlee) is fab to skate on.  We had two Summer seasons here until 2021 when only outdoor meetups were allowed. We still hold our annual Summer Skate Picnic here it's a great afternoon for all wheels and all ages.  This year we are meeting up here for Worldwide Roll Out Day.",
                    Longitude = -1.58868M,
                    Latitude = 53.84302M,
                    Image = absoluteUrlHelper.Get("/rggeventone/checkpoint-photo/brownlee-centre.jpg"),
                    DigitalBadge = absoluteUrlHelper.Get("/rggeventone/checkpoint-badge/brownlee-centre-teal.jpg"),
                    Url = "https://sport.leeds.ac.uk/facilities/bodington-playing-fields/"
                },
                new CheckPointModel
                {
                    SkateTarget = SkateTarget.GoldenAcrePark,
                    DistanceInKilometers = 67.4M,
                    Title = "Golden Acre Park",
                    Description = "As we enter the more rural parts of Leeds, it's Golden Acre Park.  This park used to have a miniature railway! Sadly not all the paths are suitable for skating, but the ducks are pretty friendly and the cafe is good. This park has good accessibility for pushchairs and wheelchair users, plus there are plenty of cute doggos to say hi to!",
                    Longitude = -1.58551M,
                    Latitude = 53.87128M,
                    Image = absoluteUrlHelper.Get("/rggeventone/checkpoint-photo/golden-acre-park.jpg"),
                    DigitalBadge = absoluteUrlHelper.Get("/rggeventone/checkpoint-badge/golden-acre-park-teal.jpg"),
                    Url = "https://goldenacrepark.co.uk/"
                },
                new CheckPointModel
                {
                    SkateTarget = SkateTarget.EccupReservoir,
                    DistanceInKilometers = 71.5M,
                    Title = "Eccup Reservoir",
                    Description = "You have made it to the 70K mark, Eccup reservoir - it's a great place to bird watch, as the area is designated as a Site of Special Scientific Interest and is nationally important for birds.  Keep an eye out for Red Kites, they have been saved from extinction and are known to nest in the area.",
                    Longitude = -1.54489M,
                    Latitude = 53.87017M,
                    Image = absoluteUrlHelper.Get("/rggeventone/checkpoint-photo/eccup-reservoir.jpg"),
                    DigitalBadge = absoluteUrlHelper.Get("/rggeventone/checkpoint-badge/eccup-reservoir-teal.jpg"),
                    Url = "https://www.yorkshirewater.com/things-to-do/reservoirs/eccup-reservoir/"
                },
                new CheckPointModel
                {
                    SkateTarget = SkateTarget.EmmerdaleTheTour,
                    DistanceInKilometers = 77.1M,
                    Title = "Emmerdale The Tour",
                    Description = "You are now 3/4 of the way to the finish of our virtual skate!  On the map you have reached the fictional village of Emmerdale.  Emmerdale is a British TV soap opera about a small farming village which was first broadcast in 1972.  The shows outdoor scenes are filmed here on a purpose built set.",
                    Longitude = -1.53767M,
                    Latitude = 53.88666M,
                    Image = absoluteUrlHelper.Get("/rggeventone/checkpoint-photo/emmerdale.jpg"),
                    DigitalBadge = absoluteUrlHelper.Get("/rggeventone/checkpoint-badge/emmerdale-purple.jpg"),
                    Url = "https://www.itv.com/emmerdale"
                },
                new CheckPointModel
                {
                    SkateTarget = SkateTarget.HarewoodHouseTrust,
                    DistanceInKilometers = 82.2M,
                    Title = "Harewood House Trust",
                    Description = "Can you believe it - you are at 80K!  We have reached Harewood House, a stately home which sits in the heart of Yorkshire and is one of the Treasure Houses of England. The House was built in the 18th century and has art collections to rival the finest in Britain.",
                    Longitude = -1.52731M,
                    Latitude = 53.89689M,
                    Image = absoluteUrlHelper.Get("/rggeventone/checkpoint-photo/harewood-house.jpg"),
                    DigitalBadge = absoluteUrlHelper.Get("/rggeventone/checkpoint-badge/harewood-house-teal.jpg"),
                    Url = "https://harewood.org/"
                },
                new CheckPointModel
                {
                    SkateTarget = SkateTarget.OtleyChevinForestPark,
                    DistanceInKilometers = 93.4M,
                    Title = "Otley Chevin Forest Park",
                    Description = "You have now skated over 90K and reached Otley Chevin. The Chevin is a picturesque nature reserve and is named after a ridge that rises to a height of 282m above sea level, overlooking the market town of Otley, and the Wharfe Valley.",
                    Longitude = -1.67177M,
                    Latitude = 53.89492M,
                    Image = absoluteUrlHelper.Get("/rggeventone/checkpoint-photo/otley-chevin-forest.jpg"),
                    DigitalBadge = absoluteUrlHelper.Get("/rggeventone/checkpoint-badge/otley-chevin-teal.jpg"),
                    Url = "https://www.chevinforest.co.uk/"
                },
                new CheckPointModel
                {
                    SkateTarget = SkateTarget.YeadonTarn,
                    DistanceInKilometers = 97.3M,
                    Title = "Yeadon Tarn",
                    Description = "The penultimate stop on our virtual skate is Yeadon Tarn Field Lake (also known as Yeadon Tarn, Yeadon Dam, or ‘The Dam’ by locals).It is bursting with activities to keep you entertained and offers the chance of some close up plane spotting.  It's a smooth flat path for skating too. Not far to go now!",
                    Longitude = -1.67441M,
                    Latitude = 53.86877M,
                    Image = absoluteUrlHelper.Get("/rggeventone/checkpoint-photo/yeadon-tarn.jpg"),
                    DigitalBadge = absoluteUrlHelper.Get("/rggeventone/checkpoint-badge/yeadon-tarn-teal.jpg"),
                    Url = "https://www.tripadvisor.co.uk/Attraction_Review-g186411-d2534618-Reviews-Yeadon_Tarn-Leeds_West_Yorkshire_England.html"
                },
                new CheckPointModel
                {
                    SkateTarget = SkateTarget.LeedsBradfordAirport,
                    DistanceInKilometers = 100,
                    Title = "Leeds Bradford Airport",
                    Description = "Chuffin' 'eck! Well done! you made it to the end of our Virtual skate Marathon, you have skated 100K, and we leave you, here at Leeds Bradford International where you can catch a flight to locations all over Europe.  We hope you'll come skate with us again soon, whether virtually or in real life. See thi' soon!",
                    Longitude = -1.66153M,
                    Latitude = 53.86794M,
                    Image = absoluteUrlHelper.Get("/rggeventone/checkpoint-photo/leeds-bradford-airport.jpg"),
                    DigitalBadge = absoluteUrlHelper.Get("/rggeventone/checkpoint-badge/leeds-bradford-airport-purple.jpg"),
                    Url = "https://www.leedsbradfordairport.co.uk/"
                },
                new CheckPointModel
                {
                    SkateTarget = SkateTarget.ThereAndBackAgain,
                    DistanceInKilometers = 200,
                    Title = "There and Back Again!",
                    Description = "Ey Up! Welcome back to the Roller Girl Gang shop!  We are amazed at how far you have travelled on skates.  The Roller Girl Gang shop is our favourite place to get all our skate gear (obvs!)  The shop is situated in the beautiful historic Corn Exchange building. Built in 1861 for trading corn, the building is now home to a number of independent businesses.",
                    Longitude = -1.54022M,
                    Latitude = 53.79609M,
                    Image = absoluteUrlHelper.Get("/rggeventone/checkpoint-photo/roller-girl-gang.jpg"),
                    DigitalBadge = absoluteUrlHelper.Get("/rggeventone/checkpoint-badge/corn-exchange-rgg-purple.jpg"),
                    Url = "https://en.wikipedia.org/wiki/Leeds_Corn_Exchange"
                }
            };
        }

        public List<SelectListItem> GetSelectList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Temple Newsam Park (Half Marathon)", Value = SkateTarget.TempleNewsamPark.ToString("F") },
                new SelectListItem { Text = "Potternewton Park (Full Marathon)", Value = SkateTarget.PotternewtonPark.ToString("F") },
                new SelectListItem { Text = "Emmerdale The Tour (One & Half Marathons)", Value = SkateTarget.EmmerdaleTheTour.ToString("F") },
                new SelectListItem { Text = "Leeds Bradford Airport (100KM)", Value = SkateTarget.LeedsBradfordAirport.ToString("F") },
                new SelectListItem { Text = "There and Back Again (200KM)", Value = SkateTarget.ThereAndBackAgain.ToString("F") }
            };
        }
    }
}
