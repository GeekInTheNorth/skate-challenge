using System.Collections.Generic;

namespace AllInSkateChallenge.Features.Data.Static
{
    public interface ICheckPointRepository
    {
        List<CheckPointModel> Get();
    }
}
