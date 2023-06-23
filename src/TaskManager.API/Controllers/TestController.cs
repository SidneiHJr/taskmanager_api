using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Core.Controllers;
using TaskManager.Core.Interfaces;

namespace TaskManager.API.Controllers
{
    [Route("api/test")]
    public class TestController : MainController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserService _userService;
        public TestController(
            INotifiable notifiable,
            UserManager<IdentityUser> userManager,
            IUserService userService) : base(notifiable)
        {
            _userManager = userManager;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Test()
        {
            return CustomResponse("Test");
        }

        [HttpPost("delete-user")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var identityUser = await _userManager.FindByIdAsync(id);
            if (identityUser != null)
                await _userManager.DeleteAsync(identityUser);

            var guidId = Guid.Parse(id);

            var user = await _userService.GetAsync(guidId);

            if (user != null)
                await _userService.DeleteAsync(user.Id);

            return CustomResponse();
        }

        [HttpPost("get-identiy-user")]
        public async Task<IActionResult> Get(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            return CustomResponse(user);

        }

        [HttpPost("get-user")]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await _userService.GetAsync(id);

            return CustomResponse(user);
        }
    }
}