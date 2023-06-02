namespace AllInSkateChallenge.Features.Framework.Models;

using System;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Skater.Registration;
using AllInSkateChallenge.Features.Strava.User;

using MediatR;

public class PageViewModelBuilder<T> : IPageViewModelBuilder<T>
    where T : class
{
    private readonly IMediator mediator;

    protected ApplicationUser User;

    protected bool IsStravaAuthenticated;

    protected string StravaId;

    public PageViewModelBuilder(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public IPageViewModelBuilder<T> WithUser(ApplicationUser user)
    {
        User = user;

        return this;
    }

    public IPageViewModelBuilder<T> WithUser(StavaDetails user)
    {
        User = user?.User;
        IsStravaAuthenticated = user?.IsStravaAuthenticated ?? false;
        StravaId = user?.StravaId;

        return this;
    }

    public virtual async Task<PageViewModel<T>> Build()
    {
        var query = new UserStateQuery { User = this.User };
        var response = await mediator.Send(query);
        var isRegistrationOver = await mediator.Send(new RegistrationAvailabilityQuery());

        var model = new PageViewModel<T>
        {
            CurrentUser = this.User,
            IsStravaAuthenticated = this.IsStravaAuthenticated,
            StravaId = this.StravaId,
            DisplayStravaNotification = response.HasStravaImports,
            Content = Activator.CreateInstance<T>(),
            IsNoIndexPage = false,
            IsRegistrationOver = isRegistrationOver
        };

        return model;
    }
}