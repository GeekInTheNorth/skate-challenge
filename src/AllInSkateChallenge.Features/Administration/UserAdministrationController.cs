using System;
using System.Linq;
using System.Threading.Tasks;

using AllInSkateChallenge.Features.Data.Entities;

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

        public UserAdministrationController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
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
        public async Task<IActionResult> Delete(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            await userManager.DeleteAsync(user);

            return Ok();
        }
    }
}
