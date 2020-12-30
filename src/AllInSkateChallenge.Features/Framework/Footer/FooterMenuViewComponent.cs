namespace AllInSkateChallenge.Features.Framework.Footer
{
    using System.Threading.Tasks;

    using AllInSkateChallenge.Features.Data.Entities;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class FooterMenuViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> userManager;

        public FooterMenuViewComponent(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(ApplicationUser user)
        {
            var model = new FooterMenuViewModel();
            if (user != null)
            {
                model.IsLoggedIn = true;
                model.IsAdmin = await userManager.IsInRoleAsync(user, "Administrator");
            }

            return View("~/Views/Shared/Components/FooterMenu/Default.cshtml", model);
        }
    }
}
