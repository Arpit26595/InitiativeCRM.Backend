using Microsoft.EntityFrameworkCore;
using Project.Application.Interfaces;
using Project.Infrastructure.ExtensionMethods;
using Project.Infrastructure.Persistence;
using Shared.Models.Utilities.Filters;
using Shared.Models.Utilities.Paging;
using Shared.Models.Utilities.Sorting;
using System.Linq.Expressions;

namespace Project.Infrastructure.Repositories;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected readonly ProjectDbContext _dbContext;
    protected readonly DbSet<T> _dbSet;

    public RepositoryBase(ProjectDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public virtual async Task<T> CreateAsync(T entity, CancellationToken cancellationToken)
    {
        var changeState = _dbContext.SaveChangeState();
        await _dbContext.Set<T>().AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        _dbContext.DetachNotInChangeState(changeState);
        return entity;
    }

    public virtual async Task<ICollection<T>> CreateAsync(ICollection<T> collection, CancellationToken cancellationToken)
    {
        var changeState = _dbContext.SaveChangeState();
        await _dbContext.Set<T>().AddRangeAsync(collection, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        _dbContext.DetachNotInChangeState(changeState);
        return collection;
    }

    public virtual async Task<T> DeleteAsync(T entity, CancellationToken cancellationToken)
    {
        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public virtual async Task<ICollection<T>> DeleteAsync(ICollection<T> collection, CancellationToken cancellationToken)
    {
        _dbContext.Set<T>().RemoveRange(collection);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return collection;
    }

    public IQueryable<T> FindAll()
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
    {
        var entity = _dbContext.Set<T>().Where(expression);
        //if (entity != null)
        //{
        //    _dbContext.Entry(entity).State = EntityState.Detached;
        //}
        return entity;
    }

    public IQueryable<T> GetAll()
    {
        return _dbContext.Set<T>();
    }

    public virtual async Task<T> GetByIdAsync(object id, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Set<T>().FindAsync(new[] { id }, cancellationToken);
        if (entity != null)
            _dbContext.Entry(entity).State = EntityState.Detached;
        return entity;
    }

    public virtual async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        var changeState = _dbContext.SaveChangeState();
        _dbContext.Set<T>().Update(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        _dbContext.DetachNotInChangeState(changeState);
        return entity;
    }

    public virtual async Task<ICollection<T>> UpdateAsync(ICollection<T> collection, CancellationToken cancellationToken)
    {
        var changeState = _dbContext.SaveChangeState();
        _dbContext.Set<T>().UpdateRange(collection);
        await _dbContext.SaveChangesAsync(cancellationToken);
        _dbContext.DetachNotInChangeState(changeState);
        return collection;
    }
    

    // Generic search with filters, sorting, and pagination
    public virtual async Task<PagingResponseDto<T>> SearchAsync(
        List<Filter<object>> filters,
        string sortBy = null,
        SortDirection sortDirection = SortDirection.Asc,
        int pageNumber = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var query = _dbSet.AsNoTracking().AsQueryable();

        // Apply filters
        query = FilterService.ApplyFilters(query, filters);

        // Get total count
        var totalCount = await query.CountAsync(cancellationToken);

        // Apply sorting
        if (!string.IsNullOrWhiteSpace(sortBy))
        {
            query = SortingService.ApplySort(query, sortBy, sortDirection);
        }

        // Apply pagination
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToArrayAsync(cancellationToken);

        return new PagingResponseDto<T>
        {
            CurrentPage = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
            Items = items
        };
    }
}
