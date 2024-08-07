﻿using AllInSkateChallenge.Features.Common;

namespace AllInSkateChallenge.Features.Home;

public class HomePageViewModel
{
    public int NumberOfSkaters { get; set; }
    
    public decimal CumulativeMiles { get; set; }

    public decimal CumulativeKilometers => Conversion.MilesToKilometres(CumulativeMiles);

    public bool ShowLeaderBoardButton { get; set; }

    public bool ShowAllUpdatesButton { get; set; }

    public string Introduction { get; set; }

    public string RegistrationTitle { get; set; }

    public string RegistrationGuidance { get; set; }

    public string EventMap { get; set; }
}