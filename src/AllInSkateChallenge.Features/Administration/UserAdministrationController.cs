namespace AllInSkateChallenge.Features.Administration
{
    using System;
    using System.Threading.Tasks;

    using AllInSkateChallenge.Features.Administration.UserDelete;
    using AllInSkateChallenge.Features.Administration.UserList;
    using AllInSkateChallenge.Features.Administration.UserUpdate;
    using AllInSkateChallenge.Features.Data;

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

        public async Task<IActionResult> Index(string searchText, int page = 1, SortOrder sortOrder = SortOrder.AtoZ)
        {
            var query = new AdminUserListQuery { SearchText = searchText, Page = page, SortOrder = sortOrder };
            var result = await mediator.Send(query);

            return View(result);
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
    }
}
