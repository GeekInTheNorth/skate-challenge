using AllInSkateChallenge.Features.Framework.Models;

namespace AllInSkateChallenge.Features.Error
{
    public interface IErrorViewModelBuilder : IPageViewModelBuilder<ErrorViewModel>
    {
        IErrorViewModelBuilder WithStatusCode(int statusCode);
    }
}
