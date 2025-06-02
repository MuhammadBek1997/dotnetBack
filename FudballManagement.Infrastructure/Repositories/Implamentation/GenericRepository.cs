using FudballManagement.Domain.Commons;
using FudballManagement.Infrastructure.DbContexts;
using FudballManagement.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FudballManagement.Infrastructure.Repositories.Implamentation;
public class GenericRepository<T> : IGenericRepository<T> where T : Auditable
{
    private readonly AppDbContext _appDbContext;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
        _dbSet = _appDbContext.Set<T>();
    }
    public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity);
        await _appDbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<bool> DeleteAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken)
    {
        var FindEntity = await _dbSet.FirstOrDefaultAsync(expression);
        if (FindEntity == null)
            return false;
        _dbSet.Remove(FindEntity);
        await _appDbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>>? expression = null, string[] includes = null)
    {
        IQueryable<T> query = _dbSet;
        if(includes!= null)
        {
            foreach(var include in includes)
            {
                query = query.Include(include);
            }
        }
        if(expression != null)
            query = query.Where(expression);
        
        return query;
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> expression, string[] includes = null)
    {
        IQueryable<T> values = _dbSet;

        if(includes != null)
        {
            foreach(var include in includes)
                values = values.Include(include);
        }
        return await values.FirstOrDefaultAsync(expression);

    }

    public async Task<T> UpdateAsync(T entity,CancellationToken token)
    {
        _dbSet.Update(entity);
        await _appDbContext.SaveChangesAsync(token);
        return entity;
    }
}
