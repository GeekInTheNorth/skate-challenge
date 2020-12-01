using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace AllInSkateChallenge.Features.Data.Static
{
    public interface ICheckPointRepository
    {
        List<CheckPointModel> Get();

        List<SelectListItem> GetSelectList();
    }
}
