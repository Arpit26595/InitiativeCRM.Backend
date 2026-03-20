using Shared.Models.Utilities.Filters;
using Shared.Models.Utilities.Paging;
using Shared.Models.Utilities.Sorting;
using System.Linq.Expressions;

namespace Company.Application.Interfaces;

public interface IRepositoryBase<T> where T : class
{
    // Query Methods
    public IQueryable<T> FindAll();
    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
    public IQueryable<T> GetAll();
    public Task<T> GetByIdAsync(object id, CancellationToken cancellationToken);
    public Task<T> CreateAsync(T entity, CancellationToken cancellationToken);
    public Task<T> UpdateAsync(T entity, CancellationToken cancellationToken);
    public Task<T> DeleteAsync(T entity, CancellationToken cancellationToken);
    public Task<ICollection<T>> DeleteAsync(ICollection<T> collection, CancellationToken cancellationToken);
    public Task<ICollection<T>> UpdateAsync(ICollection<T> collection, CancellationToken cancellationToken);
    public Task<ICollection<T>> CreateAsync(ICollection<T> collection, CancellationToken cancellationToken);

    // Generic Search with Filters, Sorting, and Pagination
    Task<PagingResponseDto<T>> SearchAsync(
        List<Filter<object>> filters,
        string sortBy = null,
        SortDirection sortDirection = SortDirection.Asc,
        int pageNumber = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default);
}
