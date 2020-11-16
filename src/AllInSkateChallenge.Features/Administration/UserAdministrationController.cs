using System;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Administration.UserDelete;
using AllInSkateChallenge.Features.Administration.UserList;
using AllInSkateChallenge.Features.Administration.UserUpdate;
using AllInSkateChallenge.Features.Data;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllInSkateChallenge.Features.Administration
{
    [Authorize(Roles = "Administrator")]
    public class UserAdministrationController : Controller
    {
        private readonly IMediator mediator;

        public UserAdministrationController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> Index(string searchText, int page = 1)
        {
            var query = new AdminUserListQuery { SearchText = searchText, Page = page };
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
            catch (Exception)
            {
                // TODO - Add Logging
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
            catch (Exception)
            {
                // TODO - Add Logging
                throw;
            }
        }
    }
}
