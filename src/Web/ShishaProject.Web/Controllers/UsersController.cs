namespace ShishaProject.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Localization;
    using ShishaProject.Services.Interfaces;
    using ShishaProject.Web.ViewModels.User;

    public class UsersController : BaseController
    {
        private readonly IUsersService usersService;
        private readonly IStringLocalizer<UsersController> stringLocalizer;

        public UsersController(
            IUsersService usersService,
            IStringLocalizer<UsersController> stringLocalizer)
        {
            this.usersService = usersService;
            this.stringLocalizer = stringLocalizer;
        }

        [HttpGet]
        public IActionResult LoginUser(LoginInputModel inputModel)
        {
            if (this.usersService.UserLoggedIn())
            {
                return this.RedirectToAction(nameof(this.UserProfile));
            }

            return this.View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser(LoginInputModel inputModel, string returnUrl = "")
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var userAuthenticated = await this.usersService.AuthenticateUser(inputModel);

            if (userAuthenticated)
            {
                if (this.usersService.UserLoggedIn())
                {
                    return this.RedirectToAction(nameof(this.UserProfile));
                }

                await this.usersService.LoginUser(inputModel);

                return this.LocalRedirect(returnUrl);
            }
            else
            {
                this.ViewData["LoginError"] = this.stringLocalizer["LoginError"];
            }

            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> LogoutUser()
        {
            await this.usersService.LogoutUser();

            return this.RedirectToAction(nameof(this.LoginUser));
        }

        [HttpGet]
        public IActionResult RegisterUser()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromForm] RegistrationInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var result = await this.usersService.RegisterUserAsync(inputModel);

            return this.View();
        }

        public async Task<IActionResult> ResetPassword(RegistrationInputModel inputModel)
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult UserProfile()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> UserProfile(object myVm)
        {
            // create vm so user can change his profile info
            return this.View();
        }
    }
}
