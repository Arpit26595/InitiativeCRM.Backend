using Lead.Application.DTOs.Request;
using Lead.Application.DTOs.Response;
using Lead.Application.Interfaces;
using Lead.Domain.Entities;
using Lead.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Utilities.Filters;
using Shared.Models.Utilities.Paging;
using Shared.Models.Utilities.Sorting;
using System.Diagnostics;

namespace Lead.Infrastructure.Repositories;

public class LeadRepository : RepositoryBase<Leads>, ILeadRepository
{
    private readonly LeadDbContext leadDbContext;
    public LeadRepository(LeadDbContext context) : base(context)
    {
        leadDbContext = context;
    }
    public async Task<Leads> GetLeadById(int id,CancellationToken cancellationToken)
    {
        var query = await leadDbContext.Leads.Where(x => x.LeadId == id).AsNoTracking().FirstOrDefaultAsync();
        return query;
    }
    public async Task<PagingResponseDto<LeadResponse>> SearchAsync(
        LeadSearchRequest<object> request, 
        CancellationToken cancellationToken)
    {
        Console.WriteLine("=== LeadRepository.SearchAsync START ===");
        Console.WriteLine($"PageNumber: {request.PageNumber}");
        Console.WriteLine($"PageSize: {request.PageSize}");
        Console.WriteLine($"Filters: {request.Filters?.Count ?? 0}");

        var query = leadDbContext.Leads.AsNoTracking().AsQueryable();
        
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
        var sortedQuery = filteredQuery.OrderByDescending(l => l.DateOpened);

        // Apply pagination
        var leads = await sortedQuery
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);
            
        Console.WriteLine($"Leads returned: {leads.Count}");

        // Map to response
        var items = leads.Select(MapToResponse).ToArray();

        Console.WriteLine("=== LeadRepository.SearchAsync END ===");

        return new PagingResponseDto<LeadResponse>
        {
            CurrentPage = request.PageNumber,
            PageSize = request.PageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize),
            Items = items
        };
    }

    private LeadResponse MapToResponse(Leads lead)
    {
        return new LeadResponse
        {
            LeadId = lead.LeadId,
            DateOpened = lead.DateOpened,
            Status = lead.Status,
            LeadSource = lead.LeadSource,
            LeadType = lead.LeadType,
            FirstName = lead.FirstName,
            LastName = lead.LastName,
            Phone = lead.Phone,
            Email = lead.Email,
            Company = lead.Company,
            Address = lead.Address,
            City = lead.City,
            State = lead.State,
            ZipCode = lead.ZipCode,
            Description = lead.Description,
            Details = lead.Details,
            EstimatedValue = lead.EstimatedValue,
            Probability = lead.Probability,
            ExpectedCloseDate = lead.ExpectedCloseDate,
            AssignedTo = lead.AssignedTo,
            ConvertedDate = lead.ConvertedDate,
            Attachments = lead.Attachments,
            FullName = $"{lead.FirstName} {lead.LastName}".Trim(),
            FullAddress = $"{lead.Address}, {lead.City}, {lead.State} {lead.ZipCode}".Trim().TrimEnd(','),
            IsActive = lead.Status != Domain.Enums.LeadStatus.Lost && lead.Status != Domain.Enums.LeadStatus.Won,
            DaysOpen = (DateTime.UtcNow - lead.DateOpened).Days
        };
    }
}
