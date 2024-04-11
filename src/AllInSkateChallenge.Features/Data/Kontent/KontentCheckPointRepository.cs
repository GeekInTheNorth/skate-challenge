using System;
using System.Collections.Generic;
using System.Linq;

using Kontent.Ai.Delivery.Abstractions;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace AllInSkateChallenge.Features.Data.Kontent;

public sealed class KontentCheckPointRepository : ICheckPointRepository
{
    private readonly IDeliveryClient _deliveryClient;

    private string[] BooleanValues = { "Yes", "True" };

    private List<CheckPointModel> _checkPoints;

    public KontentCheckPointRepository(IDeliveryClient deliveryClient)
    {
        _deliveryClient = deliveryClient;
    }

    public List<CheckPointModel> Get()
    {
        if (_checkPoints != null)
        {
            return _checkPoints;
        }

        var response = _deliveryClient.GetItemsAsync<CheckpointData>().GetAwaiter().GetResult();

        _checkPoints = response.Items
                                  .OrderBy(x => x.DistanceFromStart)
                                  .ThenBy(x => x.Title)
                                  .Select((cp, index) => new CheckPointModel
                                  {
                                      SkateTarget = index,
                                      DistanceInKilometers = cp.DistanceFromStart,
                                      Title = cp.Title,
                                      Description = cp.Description,
                                      Latitude = cp.Latitude,
                                      Longitude = cp.Longitude,
                                      Links = GetLinks(cp).ToList(),
                                      Image = GetAssetUrl(cp.Image),
                                      AllImages = GetAssetUrls(cp.Image).ToList(),
                                      DigitalBadge = GetAssetUrl(cp.DigitalBadge),
                                      IsEndPoint = GetBool(cp.IsPersonalTarget)
                                  }).ToList();

        _checkPoints.Last().IsEndPoint = true;

        return _checkPoints;
    }

    public List<SelectListItem> GetSelectList()
    {
        return GetGoalCheckpoints().Select(x => new SelectListItem { Text = x.Title, Value = x.SkateTarget.ToString("F0") }).ToList();
    }

    public List<CheckPointModel> GetGoalCheckpoints()
    {
        return Get().Where(x => x.IsEndPoint).ToList();
    }

    private static string GetAssetUrl(IEnumerable<IAsset> assets)
    {
        var asset = assets?.FirstOrDefault();

        return asset?.Url;
    }

    private static IEnumerable<string> GetAssetUrls(IEnumerable<IAsset> assets)
    {
        if (assets is null)
        {
            yield break;
        }

        foreach(var asset in assets)
        {
            if (!string.IsNullOrWhiteSpace(asset.Url))
            {
                yield return asset.Url;
            }
        }
    }

    private static IEnumerable<CheckPointUrl> GetLinks(CheckpointData checkpointData)
    {
        if (!string.IsNullOrWhiteSpace(checkpointData?.LocationUrl) && !string.IsNullOrWhiteSpace(checkpointData?.LocationUrlTitle))
        {
            yield return new CheckPointUrl { Title = checkpointData.LocationUrlTitle, Url = checkpointData.LocationUrl };
        }

        if (!string.IsNullOrWhiteSpace(checkpointData?.CommunityUrlOne) && !string.IsNullOrWhiteSpace(checkpointData?.CommunityUrlOneTitle))
        {
            yield return new CheckPointUrl { Title = checkpointData.CommunityUrlOneTitle, Url = checkpointData.CommunityUrlOne };
        }

        if (!string.IsNullOrWhiteSpace(checkpointData?.CommunityUrlTwo))
        {
            yield return new CheckPointUrl { Title = checkpointData.CommunityUrlTwoTitle, Url = checkpointData.CommunityUrlTwo };
        }

        if (!string.IsNullOrWhiteSpace(checkpointData?.CommunityUrlThree))
        {
            yield return new CheckPointUrl { Title = checkpointData.CommunityUrlThreeTitle, Url = checkpointData.CommunityUrlThree };
        }
    }
    
    private bool GetBool(IEnumerable<IMultipleChoiceOption> options)
    {
        if (options is null)
        {
            return false;
        }

        return options.Any(x => BooleanValues.Any(y => y.Equals(x.Codename, StringComparison.OrdinalIgnoreCase)));
    }
}