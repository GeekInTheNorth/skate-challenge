﻿namespace AllInSkateChallenge.Features.Administration.UserList;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data;
using AllInSkateChallenge.Features.Data.Entities;

using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class AdminUserListQueryHandler : IRequestHandler<AdminUserListQuery, AdminUserListQueryResponse>
{
    private readonly UserManager<ApplicationUser> userManager;

    private readonly ApplicationDbContext dbContext;

    public AdminUserListQueryHandler(
        UserManager<ApplicationUser> userManager, 
        ApplicationDbContext dbContext)
    {
        this.userManager = userManager;
        this.dbContext = dbContext;
    }

    public async Task<AdminUserListQueryResponse> Handle(AdminUserListQuery request, CancellationToken cancellationToken)
    {
        var admins = await userManager.GetUsersInRoleAsync("Administrator");

        var query = userManager.Users;
        if (!string.IsNullOrWhiteSpace(request.SearchText))
        {
            query = query.Where(x => x.SkaterName.Contains(request.SearchText) || x.Email.Contains(request.SearchText));
        }

        switch (request.PaidStatus)
        {
            case PaidStatus.Paid:
                query = query.Where(x => x.HasPaid == true);
                break;
            case PaidStatus.NotPaid:
                query = query.Where(x => x.HasPaid == false);
                break;
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
        var distinctUserIds = users.Select(x => x.Id).ToList();
        var activitySummaries = await dbContext.SkateLogEntries
                                       .Where(x => distinctUserIds.Contains(x.ApplicationUserId))
                                       .GroupBy(x => x.ApplicationUserId)
                                       .Select(x => new Tuple<string, int>(x.Key, x.Count()))
                                       .ToListAsync();

        return new AdminUserListQueryResponse
        {
            TotalUsers = totalUsers,
            CurrentPage = request.Page,
            MaxPages = (int)Math.Ceiling((decimal)totalUsers / pageSize),
            Users = ConvertUsers(users, admins, activitySummaries),
            SearchText = request.SearchText,
            SortOrders = new List<SelectListItem>
            {
                new SelectListItem { Text = "A to Z", Value = SortOrder.AtoZ.ToString(), Selected = SortOrder.AtoZ.Equals(request.SortOrder) },
                new SelectListItem { Text = "Z to A", Value = SortOrder.ZtoA.ToString(), Selected = SortOrder.ZtoA.Equals(request.SortOrder) },
                new SelectListItem { Text = "Latest Registered", Value = SortOrder.LatestFirst.ToString(), Selected = SortOrder.LatestFirst.Equals(request.SortOrder) }
            },
            PaidStates = new List<SelectListItem>
            {
                new SelectListItem { Text = "All Skaters", Value = PaidStatus.Any.ToString(), Selected = PaidStatus.Any.Equals(request.PaidStatus) },
                new SelectListItem { Text = "Paid Skaters", Value = PaidStatus.Paid.ToString(), Selected = PaidStatus.Paid.Equals(request.PaidStatus) },
                new SelectListItem { Text = "Unpaid Skaters", Value = PaidStatus.NotPaid.ToString(), Selected = PaidStatus.NotPaid.Equals(request.PaidStatus) },
            }
        };
    }

    private List<AdminUserModel> ConvertUsers(
        IEnumerable<ApplicationUser> users, 
        IEnumerable<ApplicationUser> admins, 
        IEnumerable<Tuple<string, int>> activitySummaries)
    {
        return users.Select(x => new AdminUserModel
        {
            Id = x.Id,
            SkaterName = x.SkaterName,
            Email = x.Email,
            EmailConfirmed = x.EmailConfirmed,
            HasPaid = x.HasPaid,
            IsAdmin = admins?.Any(y => y.Id.Equals(x.Id)) ?? false,
            DateRegistered = x.DateRegistered,
            IsStravaAccount = x.IsStravaAccount,
            NoOfActivities = activitySummaries.FirstOrDefault(y => y.Item1.Equals(x.Id))?.Item2 ?? 0
        }).ToList();
    }
}