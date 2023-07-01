namespace AllInSkateChallenge.Features.Administration.UserDetail;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data;
using AllInSkateChallenge.Features.Data.Entities;

using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class UserDetailQueryHandler : IRequestHandler<UserDetailQuery, UserDetailQueryResponse>
{
    private readonly UserManager<ApplicationUser> userManager;

    private readonly ApplicationDbContext context;

    public UserDetailQueryHandler(
        UserManager<ApplicationUser> userManager, 
        ApplicationDbContext context)
    {
        this.userManager = userManager;
        this.context = context;
    }

    public async Task<UserDetailQueryResponse> Handle(UserDetailQuery request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId);
        if (user == null)
        {
            throw new EntityNotFoundException(typeof(ApplicationUser), Guid.Parse(user.Id));
        }

        var activities = await context.SkateLogEntries
                                      .Where(x => x.ApplicationUserId.Equals(request.UserId))
                                      .OrderByDescending(x => x.Logged)
                                      .ToListAsync();

        return new UserDetailQueryResponse
        {
            Activities = activities,
            User = user,
            UserFilter = request.UserFilter,
            PaidFilter = request.PaidFilter,
            FilterOrder = request.FilterOrder,
            FilterPage = request.FilterPage,
        };
    }
}