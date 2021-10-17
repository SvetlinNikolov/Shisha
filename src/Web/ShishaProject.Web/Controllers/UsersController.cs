namespace ShishaProject.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Mvc;
    using ShishaProject.Services.Data.Models.Dtos;
    using ShishaProject.Services.Interfaces;
    using ShishaProject.Web.ViewModels.Users;

    public class UsersController : BaseController
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpGet]
        public IActionResult LoginUser()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser(UserDto inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                throw new Exception("You didn't enter the needed stuff bruh");
            }

            var userAuthenticated = await this.usersService.AuthenticateUser(inputModel);

            if (userAuthenticated)
            {
                await this.SignInUser(inputModel);
            }

            return this.Json("you have logged in");
        }

        public async Task<IActionResult> RegisterUserAsync(RegistrationInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var result = await this.usersService.RegisterUserAsync(inputModel);

            return this.View();
        }

        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await this.usersService.GetUserByIdAsync(17);

            throw new Exception();
        }

        private async Task SignInUser(UserDto inputModel)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, inputModel.Username),
                };

            var claimsIdentity = new ClaimsIdentity(claims, "Login");

            await this.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }
    }
}
