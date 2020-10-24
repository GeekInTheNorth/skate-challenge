using System.Threading.Tasks;

using AllInSkateChallenge.Features.Framework.Models;

using MediatR;

namespace AllInSkateChallenge.Features.Privacy
{
    public class PrivacyViewModelBuilder : PageViewModelBuilder<PrivacyViewModel>, IPrivacyViewModelBuilder
    {
        public PrivacyViewModelBuilder(IMediator mediator) : base(mediator)
        {
        }

        public override async Task<PageViewModel<PrivacyViewModel>> Build()
        {
            var model = await base.Build();
            model.PageTitle = "Privacy Policy";
            model.DisplayPageTitle = "Privacy Policy";

            return model;
        }
    }
}
