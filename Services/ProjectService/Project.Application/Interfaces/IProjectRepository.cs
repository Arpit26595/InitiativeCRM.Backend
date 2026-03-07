using Project.Application.DTOs.Request;
using Project.Application.DTOs.Response;
using Project.Domain.Entities;
using Shared.Models.Utilities.Paging;

namespace Project.Application.Interfaces;

public interface IProjectRepository : IRepositoryBase<Projects>
{
    Task<PagingResponseDto<ProjectResponse>> SearchAsync(
        ProjectSearchRequest<object> request, 
        CancellationToken cancellationToken = default);

    Task<Projects> GetLeadById(int id, CancellationToken cancellationToken = default);
}
