namespace ShishaProject.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Localization;
    using ShishaProject.Services.Interfaces;
    using ShishaProject.Web.ViewModels.User;

    public class UsersController : BaseController
    {
        private readonly IUsersService usersService;
        private readonly IUserSecurityService userSecurityService;
        private readonly IStringLocalizer<UsersController> stringLocalizer;
        private readonly IEmailService emailService;
        private readonly LinkGenerator linkGenerator;

        public UsersController(
            IUsersService usersService,
            IUserSecurityService userSecurityService,
            IStringLocalizer<UsersController> stringLocalizer,
            IEmailService emailService,
            LinkGenerator linkGenerator)
        {
            this.usersService = usersService;
            this.userSecurityService = userSecurityService;
            this.stringLocalizer = stringLocalizer;
            this.emailService = emailService;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet("confirmEmail/{confirmEmailToken}")]
        public async Task<IActionResult> ConfirmUserEmail(string confirmEmailToken)
        {
            if (!string.IsNullOrEmpty(confirmEmailToken))
            {
                if (!await this.usersService.UpdateUserConfirmedEmailAsync(confirmEmailToken))
                {
                    throw new InvalidOperationException("User has already confirmed email"); // FIX this maybe
                }

                return this.RedirectToAction(nameof(this.LoginUser));
            }

            return this.RedirectToAction(this.RemoveController(nameof(HomeController)), nameof(HomeController.Index));
        }

        [HttpGet]
        public IActionResult LoginUser()
        {
            if (this.usersService.UserLoggedIn())
            {
                return this.RedirectToAction(nameof(this.UserProfile));
            }

            return this.View(new LoginInputModel());
        }

        public async Task<IActionResult> LoginUser(LoginInputModel inputModel, string returnUrl = "")
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var userAuthenticated = await this.usersService.AuthenticateUserAsync(inputModel);

            if (userAuthenticated)
            {
                if (this.usersService.UserLoggedIn())
                {
                    return this.RedirectToAction(nameof(this.UserProfile));
                }

                await this.usersService.LoginUserAsync(inputModel);

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
            await this.usersService.LogoutUserAsync();

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

            var user = await this.usersService.RegisterUserAsync(inputModel); // Throw errors or show 500

            if (user != null)
            {
                string confirmationLink = this.linkGenerator.GetUriByAction(
                                                             this.HttpContext,
                                                             nameof(UsersController.ConfirmUserEmail),
                                                             nameof(UsersController),
                                                             values: new { userId = user.UserId, token = inputModel.EmailConfirmToken },
                                                             scheme: this.HttpContext.Request.Scheme);

                await this.emailService.SendConfirmEmailMessageAsync(user.Email, "Confirm Email", "Potvardi si emaila we", confirmationLink);
            }

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
