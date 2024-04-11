using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AllInSkateChallenge.Features.Data.Kontent
{
    public interface ICheckPointRepository
    {
        List<CheckPointModel> Get();

        List<SelectListItem> GetSelectList();

        List<CheckPointModel> GetGoalCheckpoints();
    }
}
