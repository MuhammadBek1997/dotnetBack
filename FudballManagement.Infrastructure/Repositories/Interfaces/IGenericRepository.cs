using FudballManagement.Domain.Commons;
using System.Linq.Expressions;

namespace FudballManagement.Infrastructure.Repositories.Interfaces;
public interface IGenericRepository<T> where T : Auditable
{
    Task<T> CreateAsync(T entity,CancellationToken cancellationToken);
    Task<T> GetAsync(Expression<Func<T, bool>> expression, string[] includes = null);

    Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>>? expression = null, string[] includes = null);
    Task<bool> DeleteAsync(Expression<Func<T, bool>> expression,CancellationToken cancellationToken);
    Task<T>  UpdateAsync(T entity,CancellationToken token);

}
