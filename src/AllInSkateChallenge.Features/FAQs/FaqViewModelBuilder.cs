using System.Threading.Tasks;

using AllInSkateChallenge.Features.Framework.Models;

using MediatR;

namespace AllInSkateChallenge.Features.FAQs
{
    public class FaqViewModelBuilder : PageViewModelBuilder<FaqViewModel>, IFaqViewModelBuilder
    {
        public FaqViewModelBuilder(IMediator mediator) : base(mediator)
        {
        }

        public override async Task<PageViewModel<FaqViewModel>> Build()
        {
            var model = await base.Build();
            model.PageTitle = "FAQs";
            model.DisplayPageTitle = "Frequency Asked Questions";
            model.IsNoIndexPage = true;

            return model;
        }
    }
}
