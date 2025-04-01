using System.Linq.Expressions;

namespace CleanArchitecture.Advanced.Api.Application.Interfaces.Repositories.Base
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetByIdAsync(long id);
        Task<TEntity> FirstEntityAsync();
        Task<TEntity> FirstEntityAsync(Expression<Func<TEntity, bool>> expression);
        Task<IEnumerable<TField>> SelectAsync<TField>(Expression<Func<TEntity, TField>> expression) where TField : class;
        Task InsertAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(long id);
        Task CommitChangesAsync();
    }
}
