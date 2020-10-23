using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data;
using AllInSkateChallenge.Features.Data.Entities;

using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AllInSkateChallenge.Features.Strava.Webhook.Deauthorise
{
    public class DeauthoriseStravaUserCommandHandler : IRequestHandler<DeauthoriseStravaUserCommand, DeauthoriseStravaUserCommandResponse>
    {
        private readonly UserManager<ApplicationUser> userManager;

        private readonly ApplicationDbContext dbContext;

        public DeauthoriseStravaUserCommandHandler(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext dbContext)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
        }

        public async Task<DeauthoriseStravaUserCommandResponse> Handle(DeauthoriseStravaUserCommand request, CancellationToken cancellationToken)
        {
            var response = new DeauthoriseStravaUserCommandResponse { WasSuccessful = false };
            var user = await userManager.FindByNameAsync(request.StravaUserId);
            if (user == null)
            {
                return response;
            }

            try
            {
                response.UserDetails = user;
                response.SkateLogs = await dbContext.SkateLogEntries.Where(x => x.ApplicationUserId.Equals(user.Id)).OrderBy(x => x.Logged).ToListAsync();
                response.StravaEvents = await dbContext.StravaEvents.Where(x => x.ApplicationUserId.Equals(user.Id)).ToListAsync();

                await userManager.DeleteAsync(user);

                response.WasSuccessful = true;
            }
            catch (Exception)
            {
                response.WasSuccessful = false;
            }

            return response;
        }
    }
}
