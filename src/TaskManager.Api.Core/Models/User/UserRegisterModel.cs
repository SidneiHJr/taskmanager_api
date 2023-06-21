using System.ComponentModel.DataAnnotations;

namespace TaskManager.Api.Core.Models
{
    public class UserRegisterModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password and Confirmation Password must be the same")]
        public string ConfirmationPassword { get; set; }

    }
}
