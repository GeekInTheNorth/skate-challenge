using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;

using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AllInSkateChallenge.Features.Administration.UserList
{
    public class AdminUserListQueryHandler : IRequestHandler<AdminUserListQuery, AdminUserListQueryResponse>
    {

        private readonly UserManager<ApplicationUser> userManager;

        public AdminUserListQueryHandler(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<AdminUserListQueryResponse> Handle(AdminUserListQuery request, CancellationToken cancellationToken)
        {
            var totalUsers = userManager.Users.Count();
            var pageSize = 10;
            var skip = (request.Page - 1) * pageSize;

            return new AdminUserListQueryResponse
            {
                TotalUsers = totalUsers,
                CurrentPage = request.Page,
                MaxPages = (totalUsers / pageSize) + 1,
                Users = await userManager.Users.Skip(skip).Take(pageSize).ToListAsync()
            };
        }
    }
}
