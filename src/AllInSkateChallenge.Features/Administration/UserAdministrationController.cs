namespace AllInSkateChallenge.Features.Administration;

using System;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Administration.UserDelete;
using AllInSkateChallenge.Features.Administration.UserDetail;
using AllInSkateChallenge.Features.Administration.UserList;
using AllInSkateChallenge.Features.Administration.UserUpdate;
using AllInSkateChallenge.Features.Data;
using AllInSkateChallenge.Features.Skater.Registration;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

[Authorize(Roles = "Administrator")]
public class UserAdministrationController : Controller
{
    private readonly IMediator mediator;

    private readonly ILogger<UserAdministrationController> logger;

    public UserAdministrationController(IMediator mediator, ILogger<UserAdministrationController> logger)
    {
        this.mediator = mediator;
        this.logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string userFilter, PaidStatus paidFilter = PaidStatus.Any, SortOrder filterOrder = SortOrder.AtoZ, int filterPage = 1)
    {
        var query = new AdminUserListQuery
        {
            UserFilter = userFilter,
            PaidFilter = paidFilter,
            FilterOrder = filterOrder,
            FilterPage = filterPage
        };
        var result = await mediator.Send(query);

        return View("~/Views/UserAdministration/Index.cshtml", result);
    }

    [HttpGet]
    public async Task<IActionResult> UserDetail(string userId, string userFilter, PaidStatus paidFilter = PaidStatus.Any, SortOrder filterOrder = SortOrder.AtoZ, int filterPage = 1)
    {
        try
        {
            var command = new UserDetailQuery
            {
                UserId = userId,
                UserFilter = userFilter,
                PaidFilter = paidFilter,
                FilterOrder = filterOrder,
                FilterPage = filterPage
            };
            
            var response = await mediator.Send(command);

            return View("~/Views/UserAdministration/UserDetails.cshtml", response);
        }
        catch(EntityNotFoundException exception)
        {
            logger.LogInformation(exception, "User Not Found");
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    public async Task<IActionResult> Delete(AdminDeleteUserCommand command)
    {
        try
        {
            await mediator.Send(command);

            return Ok();
        }
        catch(EntityNotFoundException)
        {
            return NotFound();
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Unexpected error encountered when trying to delete a user.");
            throw;
        }
    }

    [HttpPost]
    public async Task<IActionResult> Update(AdminUpdateUserCommand command)
    {
        try
        {
            await mediator.Send(command);

            return Ok();
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Unexpected error encountered when trying to update a user.");
            throw;
        }
    }

    [HttpPost]
    public async Task<IActionResult> SendSignUpEmail(ResendRegistrationEmailCommand command)
    {
        try
        {
            await mediator.Send(command);

            return Ok();
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Unexpected error encountered when trying to resend a user registration email.");
            throw;
        }
    }
}