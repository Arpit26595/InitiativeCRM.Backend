using Company.Application.DTOs.Request;
using Company.Application.DTOs.Response;
using Company.Application.Interfaces;
using Company.Domain.Entities;
using Company.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Utilities.Filters;
using Shared.Models.Utilities.Paging;

namespace Company.Infrastructure.Repositories;

public class ContactRepository : RepositoryBase<Contacts>, IContactRepository
{
    private readonly CompanyDbContext _context;

    public ContactRepository(CompanyDbContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    public async Task<ContactResponse?> GetContactById(int id, CancellationToken cancellationToken)
    {
        var contact = await _context.Contacts
            .AsNoTracking()
            .Where(c => c.ContactId == id)
            .FirstOrDefaultAsync(cancellationToken);

        return contact == null ? null : MapToResponse(contact);
    }

    public async Task<List<ContactResponse>> GetContactsByCompanyId(
        int companyId, CancellationToken cancellationToken)
    {
        var contacts = await _context.Contacts
            .AsNoTracking()
            .Where(c => c.CompanyId == companyId)
            .OrderByDescending(c => c.IsPrimary)
            .ThenBy(c => c.LastName)
            .ToListAsync(cancellationToken);

        return contacts.Select(MapToResponse).ToList();
    }

    public async Task<PagingResponseDto<ContactResponse>> SearchAsync(
        ContactSearchRequest<object> request, CancellationToken cancellationToken)
    {
        var query = _context.Contacts.AsNoTracking().AsQueryable();

        // Filter by company if provided
        if (request.CompanyId > 0)
            query = query.Where(c => c.CompanyId == request.CompanyId);

        // Apply filters
        query = FilterService.ApplyFilters(query, request.Filters);

        var totalCount = await query.CountAsync(cancellationToken);

        var contacts = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new PagingResponseDto<ContactResponse>
        {
            CurrentPage = request.PageNumber,
            PageSize = request.PageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize),
            Items = contacts.Select(MapToResponse).ToArray()
        };
    }

    private static ContactResponse MapToResponse(Contacts contact)
    {
        return new ContactResponse
        {
            ContactId = contact.ContactId,
            CompanyId = contact.CompanyId,
            FirstName = contact.FirstName,
            LastName = contact.LastName,
            Title = contact.Title,
            Email = contact.Email,
            Phone = contact.Phone,
            Mobile = contact.Mobile,
            IsPrimary = contact.IsPrimary,
            Notes = contact.Notes,
            FullName = contact.FullName
        };
    }
}