using Project.Application.DTOs;
using Project.Application.DTOs.Request;
using Project.Application.DTOs.Response;
using Shared.Models.Utilities.Paging;

namespace Project.Application.Interfaces;

public interface IProjectService
{
    Task CreateAsync(CreateProjectDTO dto, CancellationToken cancellationToken);
    Task UpdateAsync(int id, CreateProjectDTO dto, CancellationToken cancellationToken);


    Task<ProjectResponse> GetLeadById(int id, CancellationToken cancellationToken);
    Task<PagingResponseDto<ProjectResponse>> SearchAsync(ProjectSearchRequest<object> request, CancellationToken cancellationToken);
}