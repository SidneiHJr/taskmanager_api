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

        [HttpPost("register")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(OkModel), 200)]
        [ProducesResponseType(typeof(BadRequestModel), 400)]
        [ProducesResponseType(typeof(InternalServerErrorModel), 500)]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);


            return CustomResponse();
        }
    }
}
