namespace AllInSkateChallenge.Features.Framework.Models
{
    using System.Threading;
    using System.Threading.Tasks;

    using AllInSkateChallenge.Features.Data;
    using AllInSkateChallenge.Features.Data.Entities;

    using MediatR;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class UserStateQueryHandler : IRequestHandler<UserStateQuery, UserStateQueryResponse>
    {
        private readonly ApplicationDbContext context;

        private readonly UserManager<ApplicationUser> userManager;

        private readonly HttpContext httpContext;

        public UserStateQueryHandler(
            ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager, 
            IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.userManager = userManager;
            httpContext = httpContextAccessor.HttpContext;
        }

        public async Task<UserStateQueryResponse> Handle(UserStateQuery request, CancellationToken cancellationToken)
        {
            var hasDismissedCookieBanner = httpContext.Request.Cookies.ContainsKey("cookieWarningDismissed");

            var response = new UserStateQueryResponse
            {
                IsLoggedIn = request?.User != null,
                IsStravaUser= request?.User?.IsStravaAccount ?? false,
                HasPaid = request?.User?.HasPaid ?? false,
                HasDismissedCookieBanner = hasDismissedCookieBanner
            };

            if (request?.User != null)
            {
                response.HasStravaImports = await context.StravaEvents.AnyAsync(x => x.ApplicationUserId.Equals(request.User.Id) && !x.Imported);
                response.IsAdmin = await userManager.IsInRoleAsync(request.User, "Administrator");
            }

            return response;
        }
    }
}
