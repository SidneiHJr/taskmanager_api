using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Core.Controllers;
using TaskManager.Api.Core.Models;
using TaskManager.Core.Interfaces;

namespace TaskManager.API.Controllers
{
    public class AuthController : MainController
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(INotifiable notifiable) : base(notifiable)
        {
        }

        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="model">The user registration model</param>
        /// <returns>A response indicating the status of the registration</returns>
        [HttpPost("register")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(OkModel), 200)]
        [ProducesResponseType(typeof(BadRequestModel), 400)]
        [ProducesResponseType(typeof(InternalServerErrorModel), 500)]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            //Verificar senha e confirmacao senha
            //Tentativa de criar usuario (sem a senha)
            //atribuir role usuario
            //se sucesso, atribuir senha pra usuario

            var identityUser = new IdentityUser()
            {
                Email = model.Email,
                UserName = model.Name,
                
            };

            var result = await _userManager.CreateAsync(identityUser);

            if(result.Succeeded)
            {

            }

            foreach (var error in result.Errors)
            {
                _notifiable.AddNotification("Registry Error", error.Description);
            }

            await _userManager.DeleteAsync(identityUser);

            return CustomResponse();
        }
    }
}
