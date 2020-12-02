using System.Collections.Generic;

using AllInSkateChallenge.Features.Data.Static;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace AllInSkateChallenge.Features.LeaderBoard
{
    public class LeaderBoardModel
    {
        public List<SelectListItem> Targets { get; set; }

        public List<LeaderBoardEntryModel> Entries { get; set; }

        public SkateTarget SelectedTarget { get; set; }
    }
}
