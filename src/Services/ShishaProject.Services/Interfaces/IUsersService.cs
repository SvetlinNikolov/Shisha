using ShishaProject.Web.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShishaProject.Services.Interfaces
{
    public interface IUsersService
    {
        //AuthenticateResponse Authenticate(AuthenticateRequest model);
        //IEnumerable<User> GetAll();
        //User GetById(int id);
        void Register(RegistrationInputModel model);
        //void Update(int id, UpdateRequest model);
        //void Delete(int id);
    }
}
