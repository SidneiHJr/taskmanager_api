using FinancialControl.Core.Entities;
using FinancialControl.Core.Interfaces;
using TaskManager.Core.Interfaces;

namespace TaskManager.Domain.Services
{
    public class Service<TEntity>: IService<TEntity> where TEntity : EntityBase
    {
        protected readonly IRepository<TEntity> _repository;
        protected readonly INotifiable _notifiable;
        protected readonly IUnitOfWork _unitOfWork;

        public Service(IRepository<TEntity> repository, INotifiable notifiable, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _notifiable = notifiable;
            _unitOfWork = unitOfWork;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync() => await _repository.GetAsync();

        public virtual async Task<TEntity> GetAsync(Guid id) => await _repository.GetAsync(id);

        public virtual async Task<Guid> InsertAsync(TEntity entity)
        {
            await Validate(entity);

            if (_notifiable.HasNotification)
                return Guid.Empty;

            entity.NewInsertion();

            await _repository.InsertAsync(entity);
            await _unitOfWork.CommitAsync();

            return await Task.FromResult(entity.Id);
        }

        public virtual async Task UpdateAsync(Guid id, TEntity entity)
        {
            await Validate(entity);

            if (_notifiable.HasNotification)
                return;

            var entityBd = await _repository.GetAsync(id);

            var method = entityBd.GetType().GetMethod("Update");

            if (method != null)
                method.Invoke(entityBd, new object[] { entity });
            else
                return;

            entityBd.NewUpdate();

            _repository.Update(entityBd);
            await _unitOfWork.CommitAsync();
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);

            var success = await _unitOfWork.CommitAsync();

            if (!success)
                _notifiable.AddNotification("Delete", $"Error deleting record {id}");

            await Task.CompletedTask;
        }

        protected async Task Validate(TEntity item)
        {
            var method = item.GetType().GetMethod("Validate");

            if (method != null)
            {
                var errors = method.Invoke(item, null);
                _notifiable.AddNotifications(errors as IEnumerable<string>);
            }

            await Task.CompletedTask;
        }

    }
}
