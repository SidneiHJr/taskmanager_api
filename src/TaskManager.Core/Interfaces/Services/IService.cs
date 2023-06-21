using FinancialControl.Core.Entities;

namespace TaskManager.Core.Interfaces
{
    public interface IService<TEntity> where TEntity : EntityBase
    {
        Task<IEnumerable<TEntity>> GetAsync();
        Task<TEntity> GetAsync(Guid id);
        Task<Guid> InsertAsync(TEntity entity);
        Task DeleteAsync(Guid id);
    }
}
