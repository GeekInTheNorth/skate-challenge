using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;

using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var admins = await userManager.GetUsersInRoleAsync("Administrator");

            var query = userManager.Users;
            if (!string.IsNullOrWhiteSpace(request.SearchText))
            {
                query = query.Where(x => x.SkaterName.Contains(request.SearchText) || x.Email.Contains(request.SearchText));
            }

            var totalUsers = query.Count();
            var pageSize = 10;
            var skip = (request.Page - 1) * pageSize;

            switch (request.SortOrder)
            {
                case SortOrder.AtoZ:
                    query = query.OrderBy(x => x.SkaterName);
                    break;
                case SortOrder.ZtoA:
                    query = query.OrderByDescending(x => x.SkaterName);
                    break;
                case SortOrder.LatestFirst:
                    query = query.OrderByDescending(x => x.DateRegistered);
                    break;
            }

            var users = await query.Skip(skip).Take(pageSize).ToListAsync();

            return new AdminUserListQueryResponse
            {
                TotalUsers = totalUsers,
                CurrentPage = request.Page,
                MaxPages = (int)Math.Ceiling((decimal)totalUsers / pageSize),
                Users = ConvertUsers(users, admins),
                SearchText = request.SearchText,
                SortOrders = new List<SelectListItem>
                {
                    new SelectListItem { Text = "A to Z", Value = SortOrder.AtoZ.ToString(), Selected = SortOrder.AtoZ.Equals(request.SortOrder) },
                    new SelectListItem { Text = "Z to A", Value = SortOrder.ZtoA.ToString(), Selected = SortOrder.ZtoA.Equals(request.SortOrder) },
                    new SelectListItem { Text = "Latest Registered", Value = SortOrder.LatestFirst.ToString(), Selected = SortOrder.LatestFirst.Equals(request.SortOrder) }
                }
            };
        }

        private List<AdminUserModel> ConvertUsers(IEnumerable<ApplicationUser> users, IEnumerable<ApplicationUser> admins)
        {
            return users.Select(x => new AdminUserModel
            {
                Id = x.Id,
                SkaterName = x.SkaterName,
                Email = x.Email,
                EmailConfirmed = x.EmailConfirmed,
                HasPaid = x.HasPaid,
                IsAdmin = admins?.Any(y => y.Id.Equals(x.Id)) ?? false,
                DateRegistered = x.DateRegistered
            }).ToList();
        }
    }
}
