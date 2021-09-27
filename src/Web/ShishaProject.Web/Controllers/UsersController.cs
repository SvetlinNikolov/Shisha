namespace ShishaProject.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ShishaProject.Services.Interfaces;
    using ShishaProject.Web.ViewModels.Users;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UsersController : BaseController
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public IActionResult Register(RegistrationInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }
            return null;

        }
    }
}
