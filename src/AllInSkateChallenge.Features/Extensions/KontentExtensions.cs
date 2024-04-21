using System;
using System.Collections.Generic;
using System.Linq;

using Kontent.Ai.Delivery.Abstractions;

namespace AllInSkateChallenge.Features.Extensions;

public static class KontentExtensions
{
    public static string GetSingleUrl(this IEnumerable<IAsset> assets)
    {
        return assets?.FirstOrDefault()?.Url;
    }

    public static IEnumerable<string> GetAllUrls(this IEnumerable<IAsset> assets)
    {
        if (assets is null)
        {
            yield break;
        }

        foreach (var asset in assets)
        {
            if (!string.IsNullOrWhiteSpace(asset.Url))
            {
                yield return asset.Url;
            }
        }
    }

    public static bool GetBoolean(this IEnumerable<IMultipleChoiceOption> options)
    {
        if (options is null)
        {
            return false;
        }

        var booleanValues = new[] { "Yes", "True" };

        return options.Any(x => booleanValues.Any(y => y.Equals(x.Codename, StringComparison.OrdinalIgnoreCase)));
    }
}
