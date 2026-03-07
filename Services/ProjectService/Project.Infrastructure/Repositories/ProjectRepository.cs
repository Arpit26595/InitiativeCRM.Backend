using Microsoft.EntityFrameworkCore;
using Project.Application.DTOs.Request;
using Project.Application.DTOs.Response;
using Project.Application.Interfaces;
using Project.Domain.Entities;
using Project.Infrastructure.Persistence;
using Shared.Models.Utilities.Filters;
using Shared.Models.Utilities.Paging;

namespace Project.Infrastructure.Repositories;

public class ProjectRepository : RepositoryBase<Projects>, IProjectRepository
{
    private readonly ProjectDbContext projectDbContext;
    public ProjectRepository(ProjectDbContext context) : base(context)
    {
        projectDbContext = context;
    }
    public async Task<Projects> GetLeadById(int id,CancellationToken cancellationToken)
    {
        var query = await projectDbContext.Projects.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
        return query;
    }
    public async Task<PagingResponseDto<ProjectResponse>> SearchAsync(
        ProjectSearchRequest<object> request, 
        CancellationToken cancellationToken)
    {
        Console.WriteLine("=== ProjectRepository.SearchAsync START ===");
        Console.WriteLine($"PageNumber: {request.PageNumber}");
        Console.WriteLine($"PageSize: {request.PageSize}");
        Console.WriteLine($"Filters: {request.Filters?.Count ?? 0}");

        var query = projectDbContext.Projects.AsNoTracking().AsQueryable();
        
        Console.WriteLine($"Initial query count: {query.Count()}");

        // Debug filters
        if (request.Filters != null)
        {
            foreach (var filter in request.Filters)
            {
                Console.WriteLine($"Filter - Id: {filter.Id}, Value: {filter.Value}, Operation: {filter.Operation}");
            }
        }

        // Apply generic filters using FilterService
        var filteredQuery = FilterService.ApplyFilters(query, request.Filters);
        
        Console.WriteLine($"After filters query count: {filteredQuery.Count()}");

        // Get total count
        var totalCount = await filteredQuery.CountAsync(cancellationToken);
        Console.WriteLine($"Total count: {totalCount}");

        // Apply sorting (default by DateOpened descending)
       // var sortedQuery = filteredQuery.OrderByDescending(l => l.DateOpened);

        // Apply pagination
        var projects = await filteredQuery
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);
            
        Console.WriteLine($"Project returned: {projects.Count}");

        // Map to response
        var items = projects.Select(MapToResponse).ToArray();

        Console.WriteLine("=== ProjectRepository.SearchAsync END ===");

        return new PagingResponseDto<ProjectResponse>
        {
            CurrentPage = request.PageNumber,
            PageSize = request.PageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize),
            Items = items
        };
    }

    private ProjectResponse MapToResponse(Projects lead)
    {
        return new ProjectResponse
        {
           
        };
    }
}
