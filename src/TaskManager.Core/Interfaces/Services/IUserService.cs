using TaskManager.Core.Entities;

namespace TaskManager.Core.Interfaces
{
    public interface IUserService : IService<User>
    {
        Task<Guid> InsertAsync(Guid userId, string name, string email);
    }
}
