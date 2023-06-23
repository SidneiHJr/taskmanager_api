using FinancialControl.Core.Interfaces;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces;

namespace TaskManager.Domain.Services
{
    public class UserService : Service<User>, IUserService
    {
        public UserService(
            IRepository<User> repository, 
            INotifiable notifiable, 
            IUnitOfWork unitOfWork) : base(repository, notifiable, unitOfWork)
        {
        }

        public async Task<Guid> InsertAsync(Guid userId, string name, string email)
        {
            var user = new User(userId, name, email);

            await Validate(user);

            if (_notifiable.HasNotification)
                return Guid.Empty;

            user.NewInsertion();

            await _repository.InsertAsync(user);
            var created = await _unitOfWork.CommitAsync();

            if(created)
                return user.Id;

            return Guid.Empty;
        }
    }
}
