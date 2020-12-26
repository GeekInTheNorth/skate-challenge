namespace AllInSkateChallenge.Features.Framework.Models
{
    using System;
    using System.Threading.Tasks;

    using AllInSkateChallenge.Features.Data.Entities;

    using MediatR;

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
                CurrentUser = this.User,
                DisplayStravaNotification = response.HasStravaImports,
                Content = Activator.CreateInstance<T>(),
                IsNoIndexPage = false
            };

            return model;
        }
    }
}
