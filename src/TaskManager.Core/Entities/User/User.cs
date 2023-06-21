using FinancialControl.Core.Entities;

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
    }
}
