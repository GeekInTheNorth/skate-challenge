namespace AllInSkateChallenge.Features.Strava.Webhook.Deauthorise
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AllInSkateChallenge.Features.Data;
    using AllInSkateChallenge.Features.Data.Entities;
    using AllInSkateChallenge.Features.Services.BlobStorage;

    using MediatR;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class DeauthoriseStravaUserCommandHandler : IRequestHandler<DeauthoriseStravaUserCommand, DeauthoriseStravaUserCommandResponse>
    {
        private readonly UserManager<ApplicationUser> userManager;

        private readonly ApplicationDbContext dbContext;

        private readonly IBlobStorageService blobStorageService;

        public DeauthoriseStravaUserCommandHandler(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext dbContext,
            IBlobStorageService blobStorageService)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
            this.blobStorageService = blobStorageService;
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
                response.SkateLogs = await dbContext.SkateLogEntries.Where(x => x.ApplicationUserId.Equals(user.Id)).OrderBy(x => x.Logged).ToListAsync(cancellationToken);
                response.StravaEvents = await dbContext.StravaEvents.Where(x => x.ApplicationUserId.Equals(user.Id)).ToListAsync(cancellationToken);

                await blobStorageService.DeleteFile(user.ExternalProfileImage);
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
