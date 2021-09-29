﻿namespace ShishaProject.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

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

        public async Task<IActionResult> LoginUser(UserDto inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var result = await this.usersService.LoginUserAsync(inputModel);

            return this.View();
        }

        public async Task<IActionResult> RegisterUser(RegistrationInputModel inputModel)
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
    }
}
