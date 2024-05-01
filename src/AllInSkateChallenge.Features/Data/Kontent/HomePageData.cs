using System.Collections.Generic;

using Kontent.Ai.Delivery.Abstractions;

namespace AllInSkateChallenge.Features.Data.Kontent;

public sealed class HomePageData
{
    public string Title { get; set; }

    public string Introduction { get; set; }

    public string RegistrationTitle { get; set; }

    public string RegistrationGuidance { get; set; }

    public IEnumerable<IAsset> EventMap { get; set; }
}