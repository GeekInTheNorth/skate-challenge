using System;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data;
using AllInSkateChallenge.Features.Data.Entities;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AllInSkateChallenge.Features.Framework.Models
{
    public class PageViewModelBuilder<T> : IPageViewModelBuilder<T>
        where T : class
    {
        private readonly ApplicationDbContext context;

        private readonly UserManager<ApplicationUser> userManager;

        private readonly HttpContext httpContext;

        protected ApplicationUser User;

        public PageViewModelBuilder(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.userManager = userManager;
            httpContext = httpContextAccessor.HttpContext;
        }

        public IPageViewModelBuilder<T> WithUser(ApplicationUser user)
        {
            User = user;

            return this;
        }

        public virtual async Task<PageViewModel<T>> Build()
        {
            var hasDismissedCookieBanner = httpContext.Request.Cookies.ContainsKey("cookieWarningDismissed");

            var model = new PageViewModel<T>
            {
                IsLoggedIn = User != null,
                IsStravaUser = User?.IsStravaAccount ?? false,
                HasPaid = User?.HasPaid ?? false,
                DisplayUserName = User?.SkaterName,
                Content = Activator.CreateInstance<T>(),
                ShowCookieBanner = User == null && !hasDismissedCookieBanner
            };

            if (User != null)
            {
                model.HasStravaImports = await context.StravaEvents.AnyAsync(x => x.ApplicationUserId.Equals(User.Id) && !x.Imported);
                model.IsAdmin = await userManager.IsInRoleAsync(User, "Administrator");
            }

            model.DisplayStravaNotification = model.HasStravaImports;

            return model;
        }
    }
}
