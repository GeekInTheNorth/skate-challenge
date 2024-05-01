using System;

using Kontent.Ai.Delivery.Abstractions;

namespace AllInSkateChallenge.Features.Data.Kontent;

public sealed class CustomTypeProvider : ITypeProvider
{
    public string GetCodename(Type contentType)
    {
        if (contentType.IsAssignableFrom(typeof(CheckpointData)))
        {
            return "checkpoint";
        } else if (contentType.IsAssignableFrom(typeof(SkateTeamData)))
        {
            return "skate_team";
        } else if (contentType.IsAssignableFrom(typeof(HomePageData)))
        {
            return "home_page";
        }

        throw new NotImplementedException();
    }

    public Type GetType(string contentType)
    {
        return contentType switch
        {
            "checkpoint" => typeof(CheckpointData),
            "skate_team" => typeof(SkateTeamData),
            "home_page" => typeof(HomePageData),
            _ => throw new NotImplementedException(),
        };
    }
}