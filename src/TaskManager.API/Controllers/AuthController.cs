using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Core.Controllers;
using TaskManager.Api.Core.Models;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces;

namespace TaskManager.API.Controllers
{
    [Route("api/auth")]
    public class AuthController : MainController
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(
            INotifiable notifiable, 
            UserManager<IdentityUser> userManager) : base(notifiable)
        {
            _userManager = userManager;
        }


        [HttpPost("register")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(OkModel), 200)]
        [ProducesResponseType(typeof(BadRequestModel), 400)]
        [ProducesResponseType(typeof(InternalServerErrorModel), 500)]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var identityUser = new IdentityUser()
            {
                UserName = model.Email,
                Email = model.Email

            };

            var result = await _userManager.CreateAsync(identityUser);

            if (result.Succeeded)
            {
                var user = InsertUser(identityUser.Id, model.Name, model.Email);
                //criar usuario
                //salvar senha usuario e aspnetuser

                return CustomResponse();
            }

            foreach (var error in result.Errors)
            {
                _notifiable.AddNotification("Registry Error", error.Description);
            }

            await _userManager.DeleteAsync(identityUser);
            //atribuir role usuario
            //se sucesso, atribuir senha pra usuario


            return CustomResponse();
        }

        private async Task<User> InsertUser(string id, string name, string email)
        {
            var userId = Guid.Parse(id);
            var user = new User(userId, name, email);

            //await _userService.InsertAsync(user);

            return user;
        }
    }
}
