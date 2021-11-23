using Kuvio.Kernel.Core;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParentCare.Model.Users.Commands
{
    public class UserModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Nome { get; set; }

        public string Telefone { get; set; }
    }

    //public class CreateUserCommand
    //{
        //private readonly UserManager<User> _userManager;

        //public CreateUserCommand(UserManager<User> userManager)
        //{
        //    _userManager = userManager;
        //}

        //public async Task<Result<Consultor>> Execute(UserModel model)
        //{
        //    var user = new ApplicationUser { UserName = model.Email, Email = model.Email, DataRegistro = DateTime.UtcNow, PhoneNumber = model.Telefone, Nome = model.Nome };

        //    var result = await _userManager.CreateAsync(user, model.Password);
        //    if (result.Succeeded)
        //    {
        //        return BuildToken(model);
        //    }

            
        //}
    //}
}
