using FinancialControl.Core.Entities;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace TaskManager.Core.Entities
{
    public class User : EntityBase
    {

        protected User()
        {

        }

        public User(Guid id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
            Active = true;
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public bool Active { get; private set; }

        public IEnumerable<string> Validate()
        {
            var errors = new List<string>();

            if(Email != null)
            {
                var isValid = ValidateEmail();

                if (!isValid)
                    errors.Add("Invalid email address");
            }

            return errors;
        }

        private bool ValidateEmail()
        {
            if (!MailAddress.TryCreate(Email, out var mailAddress))
                return false;

            var hostParts = mailAddress.Host.Split('.');

            if (hostParts.Length == 1)
                return false; // No dot.

            if (hostParts.Any(p => p == string.Empty))
                return false; // Double dot.

            if (hostParts[^1].Length < 2)
                return false; // TLD only one letter.

            if (mailAddress.User.Contains(' '))
                return false;

            if (mailAddress.User.Split('.').Any(p => p == string.Empty))
                return false; // Double dot or dot at end of user part.

            return true;
        }
    }
}
