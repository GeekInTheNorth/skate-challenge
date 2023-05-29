using System.Threading;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data;
using AllInSkateChallenge.Features.Data.Entities;

using MediatR;

using Microsoft.AspNetCore.Identity;

namespace AllInSkateChallenge.Features.Administration.UserUpdate
{
    public class AdminUpdateUserCommandHandler : IRequestHandler<AdminUpdateUserCommand>
    {
        private readonly UserManager<ApplicationUser> userManager;

        public AdminUpdateUserCommandHandler(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task Handle(AdminUpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.UserId);

            if (user == null)
            {
                throw new EntityNotFoundException(typeof(ApplicationUser), request.UserId);
            }

            user.HasPaid = request.HasPaid ?? user.HasPaid;

            await userManager.UpdateAsync(user);
        }
    }
}
