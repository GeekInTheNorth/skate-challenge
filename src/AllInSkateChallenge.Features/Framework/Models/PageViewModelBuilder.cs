using System;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;

using MediatR;

namespace AllInSkateChallenge.Features.Framework.Models
{
    public class PageViewModelBuilder<T> : IPageViewModelBuilder<T>
        where T : class
    {
        private readonly IMediator mediator;

        protected ApplicationUser User;

        public PageViewModelBuilder(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public IPageViewModelBuilder<T> WithUser(ApplicationUser user)
        {
            User = user;

            return this;
        }

        public virtual async Task<PageViewModel<T>> Build()
        {
            var query = new UserStateQuery { User = this.User };
            var response = await mediator.Send(query);

            var model = new PageViewModel<T>
            {
                IsLoggedIn = response.IsLoggedIn,
                IsStravaUser = response.IsStravaUser,
                IsAdmin = response.IsAdmin,
                HasPaid = response.HasPaid,
                HasStravaImports = response.HasStravaImports,
                DisplayStravaNotification = response.HasStravaImports,
                DisplayUserName = response.SkaterName,
                Content = Activator.CreateInstance<T>(),
                ShowCookieBanner = User == null && !response.HasDismissedCookieBanner,
                IsNoIndexPage = false
            };

            return model;
        }
    }
}
