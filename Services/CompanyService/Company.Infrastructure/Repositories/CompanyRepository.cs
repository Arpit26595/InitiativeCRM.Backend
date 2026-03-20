using Company.Application.DTOs.Request;
using Company.Application.DTOs.Response;
using Company.Application.Interfaces;
using Company.Domain.Entities;
using Company.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Utilities.Filters;
using Shared.Models.Utilities.Paging;

namespace Company.Infrastructure.Repositories;

public class CompanyRepository : RepositoryBase<Companies>, ICompanyRepository
{
    private readonly CompanyDbContext _context;

    public CompanyRepository(CompanyDbContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    public async Task<CompanyResponse?> GetCompanyById(int id, CancellationToken cancellationToken)
    {
        var company = await _context.Companies
            .AsNoTracking()
            .Where(c => c.CompanyId == id)
            .FirstOrDefaultAsync(cancellationToken);

        if (company == null) return null;

        var trades = await _context.CompanyTrades
            .AsNoTracking()
            .Where(t => t.CompanyId == id)
            .ToListAsync(cancellationToken);

        var contacts = await _context.Contacts
            .AsNoTracking()
            .Where(c => c.CompanyId == id)
            .ToListAsync(cancellationToken);

        return MapToResponse(company, trades, contacts);
    }

    public async Task<PagingResponseDto<CompanyResponse>> SearchAsync(
        CompanySearchRequest<object> request, CancellationToken cancellationToken)
    {
        var query = _context.Companies.AsNoTracking().AsQueryable();

        // Apply filters
        query = FilterService.ApplyFilters(query, request.Filters);

        // Get total count
        var totalCount = await query.CountAsync(cancellationToken);

        // Apply pagination
        var companies = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        // Batch load trades and contacts for the page
        var companyIds = companies.Select(c => c.CompanyId).ToList();

        var allTrades = await _context.CompanyTrades
            .AsNoTracking()
            .Where(t => companyIds.Contains(t.CompanyId))
            .ToListAsync(cancellationToken);

        var allContacts = await _context.Contacts
            .AsNoTracking()
            .Where(c => companyIds.Contains(c.CompanyId))
            .ToListAsync(cancellationToken);

        var items = companies.Select(c => MapToResponse(
            c,
            allTrades.Where(t => t.CompanyId == c.CompanyId).ToList(),
            allContacts.Where(ct => ct.CompanyId == c.CompanyId).ToList()
        )).ToArray();

        return new PagingResponseDto<CompanyResponse>
        {
            CurrentPage = request.PageNumber,
            PageSize = request.PageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize),
            Items = items
        };
    }

    private static CompanyResponse MapToResponse(
        Companies company,
        List<CompanyTrades>? trades = null,
        List<Contacts>? contacts = null)
    {
        return new CompanyResponse
        {
            CompanyId = company.CompanyId,
            CompanyName = company.CompanyName,
            Type = company.Type,
            Status = company.Status,
            Address = company.Address,
            City = company.City,
            State = company.State,
            Zip = company.Zip,
            Phone = company.Phone,
            Website = company.Website,
            BondingCompany = company.BondingCompany,
            BondingLimit = company.BondingLimit,
            PaymentTerms = company.PaymentTerms,
            IsPreferredVendor = company.IsPreferredVendor,
            Notes = company.Notes,
            Attachments = company.Attachments,
            IsActive = company.IsActive,
            FullAddress = company.FullAddress,
            Trades = trades?.Select(t => new CompanyTradeResponse
            {
                CompanyTradeId = t.CompanyTradeId,
                Trade = t.Trade,
                CustomTrade = t.CustomTrade,
                TradeName = t.TradeName
            }).ToList() ?? [],
            Contacts = contacts?.Select(c => new ContactResponse
            {
                ContactId = c.ContactId,
                CompanyId = c.CompanyId,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Title = c.Title,
                Email = c.Email,
                Phone = c.Phone,
                Mobile = c.Mobile,
                IsPrimary = c.IsPrimary,
                Notes = c.Notes,
                FullName = c.FullName
            }).ToList() ?? []
        };
    }
}