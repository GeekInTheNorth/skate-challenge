using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data;
using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Framework.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace AllInSkateChallenge.Features.FAQs
{
    public class FaqViewModelBuilder : PageViewModelBuilder<FaqViewModel>, IFaqViewModelBuilder
    {
        public FaqViewModelBuilder(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor) : base(context, userManager, httpContextAccessor)
        {
        }

        public override async Task<PageViewModel<FaqViewModel>> Build()
        {
            var model = await base.Build();
            model.PageTitle = "FAQs";
            model.DisplayPageTitle = "Frequency Asked Questions";

            return model;
        }
    }
}
