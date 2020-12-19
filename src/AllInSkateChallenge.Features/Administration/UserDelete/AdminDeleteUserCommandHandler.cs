namespace AllInSkateChallenge.Features.Administration.UserDelete
{
    using System.Threading;
    using System.Threading.Tasks;

    using AllInSkateChallenge.Features.Data;
    using AllInSkateChallenge.Features.Data.Entities;
    using AllInSkateChallenge.Features.Services.BlobStorage;

    using MediatR;

    using Microsoft.AspNetCore.Identity;

    public class AdminDeleteUserCommandHandler : IRequestHandler<AdminDeleteUserCommand>
    {
        private readonly UserManager<ApplicationUser> userManager;

        private readonly IBlobStorageService blobStorageService;

        public AdminDeleteUserCommandHandler(UserManager<ApplicationUser> userManager, IBlobStorageService blobStorageService)
        {
            this.userManager = userManager;
            this.blobStorageService = blobStorageService;
        }

        public async Task<Unit> Handle(AdminDeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.UserId);

            if (user == null)
            {
                throw new EntityNotFoundException(typeof(ApplicationUser), request.UserId);
            }

            var isAdmin = await userManager.IsInRoleAsync(user, "Administrator");
            if (isAdmin)
            {
                throw new EntityProtectedException(typeof(ApplicationUser), user.Id);
            }

            await blobStorageService.DeleteFile(user.ExternalProfileImage);
            await userManager.DeleteAsync(user);

            return Unit.Value;
        }
    }
}
