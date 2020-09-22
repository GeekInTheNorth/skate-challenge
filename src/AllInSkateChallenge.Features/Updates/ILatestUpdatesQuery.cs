using System.Collections.Generic;

namespace AllInSkateChallenge.Features.Updates
{
    public interface ILatestUpdatesQuery
    {
        MileageUpdateModel Get(int maximum = 10);
    }
}
