using System;
using System.Linq;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Administration.UserDelete;
using AllInSkateChallenge.Features.Administration.UserUpdate;
using AllInSkateChallenge.Features.Data;
using AllInSkateChallenge.Features.Data.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AllInSkateChallenge.Features.Administration
{
    [Authorize(Roles = "Administrator")]
    public class UserAdministrationController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        private readonly IMediator mediator;

        public UserAdministrationController(UserManager<ApplicationUser> userManager, IMediator mediator)
        {
            this.userManager = userManager;
            this.mediator = mediator;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var totalUsers = userManager.Users.Count();
            var pageSize = 10;
            var skip = (page - 1) * pageSize;

            var model = new UserAdministrationViewModel
            {
                TotalUsers = totalUsers,
                CurrentPage = page,
                MaxPages = (totalUsers / pageSize) + 1,
                Users = await userManager.Users.Skip(skip).Take(pageSize).ToListAsync()
            };

            return View(model);
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
