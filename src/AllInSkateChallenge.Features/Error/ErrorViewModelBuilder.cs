using System.Threading.Tasks;

using AllInSkateChallenge.Features.Framework.Models;

using MediatR;

namespace AllInSkateChallenge.Features.Error
{
    public class ErrorViewModelBuilder : PageViewModelBuilder<ErrorViewModel>, IErrorViewModelBuilder
    {
        private int statusCode;

        public ErrorViewModelBuilder(IMediator mediator) : base(mediator)
        {
        }

        public IErrorViewModelBuilder WithStatusCode(int statusCode)
        {
            this.statusCode = statusCode;

            return this;
        }

        public override async Task<PageViewModel<ErrorViewModel>> Build()
        {
            var model = await base.Build();

            model.Content.StatusCode = statusCode;
            model.PageTitle = statusCode.Equals(404) ? "Not Found" : $"Error {statusCode}";
            model.IsNoIndexPage = true;

            return model;
        }
    }
}
