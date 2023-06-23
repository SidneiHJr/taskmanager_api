using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Core.Controllers;
using TaskManager.Api.Core.Models;
using TaskManager.Core.Entities;
using TaskManager.Core.Enums;
using TaskManager.Core.Interfaces;

namespace TaskManager.API.Controllers
{
    [Route("api/auth")]
    public class AuthController : MainController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IPasswordHasher<IdentityUser> _passwordHasher;

        private readonly IUserService _userService;

        public AuthController(
            INotifiable notifiable,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IPasswordHasher<IdentityUser> passwordHasher,
            IUserService userService
            ) : base(notifiable)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userService = userService;
            _passwordHasher = passwordHasher;
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

            var result = await _userManager.CreateAsync(identityUser, model.Password);

            if (result.Succeeded)
            {
                var userId = await InsertUser(identityUser.Id, model.Name, model.Email);

                await CreateRoles();

                await _userManager.AddToRoleAsync(identityUser, UserProfileEnum.User.ToString());

                if (!_notifiable.HasNotification)
                    return CustomResponse(userId);

                if (userId != Guid.Empty)
                    await _userService.DeleteAsync(userId);

            }

            foreach (var error in result.Errors)
            {
                _notifiable.AddNotification("Registry Error", error.Description);
            }

            await _userManager.DeleteAsync(identityUser);

            return CustomResponse();
        }

        private async Task<Guid> InsertUser(string id, string name, string email)
        {
            var guidId = Guid.Parse(id);

            var userId = await _userService.InsertAsync(guidId, name, email);

            return userId;
        }

        private async Task CreateRoles()
        {
            string[] rolesNames = {
                UserProfileEnum.User.ToString(),
            };

            foreach(var role in rolesNames)
            {
                var roleExist = await _roleManager.RoleExistsAsync(role);
                if(!roleExist)
                    await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}
