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
        public TestController(
            INotifiable notifiable, 
            UserManager<IdentityUser> userManager) : base(notifiable)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Test()
        {
            return CustomResponse("Test");
        }

        [HttpPost("delete-user")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }

            return CustomResponse();
        }
    }
}