using System.Collections.Generic;
using System.Linq;

using AllInSkateChallenge.Features.Extensions;

using Kontent.Ai.Delivery.Abstractions;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace AllInSkateChallenge.Features.Data.Kontent;

public sealed class CheckPointRepository(IDeliveryClient deliveryClient) : ICheckPointRepository
{
    private List<CheckPointModel> _checkPoints;

    public List<CheckPointModel> Get()
    {
        if (_checkPoints != null)
        {
            return _checkPoints;
        }

        var response = deliveryClient.GetItemsAsync<CheckpointData>().GetAwaiter().GetResult();

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
                                   Image = cp.Image.GetSingleUrl(),
                                   AllImages = cp.Image.GetAllUrls().ToList(),
                                   DigitalBadge = cp.DigitalBadge.GetSingleUrl(),
                                   IsEndPoint = cp.IsPersonalTarget.GetBoolean()
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
}