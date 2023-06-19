using FinancialControl.Core.Entities;

namespace TaskManager.Core.Entities
{
    public class User : EntityBase
    {

        protected User()
        {

        }

        public User(string name, string email)
        {
            Name = name;
            Email = email;
            Active = true;
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public bool Active { get; private set; }
    }
}
