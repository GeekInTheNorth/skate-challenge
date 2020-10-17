using System.Threading;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data;
using AllInSkateChallenge.Features.Data.Entities;

using MediatR;

using Microsoft.AspNetCore.Identity;

namespace AllInSkateChallenge.Features.Administration.UserDelete
{
    public class AdminDeleteUserCommandHandler : IRequestHandler<AdminDeleteUserCommand>
    {
        private readonly UserManager<ApplicationUser> userManager;

        public AdminDeleteUserCommandHandler(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<Unit> Handle(AdminDeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.UserId);

            if (user == null)
            {
                throw new EntityNotFoundException(typeof(ApplicationUser), request.UserId);
            }

            await userManager.DeleteAsync(user);

            return Unit.Value;
        }
    }
}
