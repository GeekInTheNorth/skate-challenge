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
        }

        throw new NotImplementedException();
    }

    public Type GetType(string contentType)
    {
        return contentType switch
        {
            "checkpoint" => typeof(CheckpointData),
            "skate_team" => typeof(SkateTeamData),
            _ => throw new NotImplementedException(),
        };
    }
}
