using Company.Application.DTOs.Request;
using Company.Application.DTOs.Response;
using Company.Domain.Entities;
using Shared.Models.Utilities.Paging;

namespace Company.Application.Interfaces;

public interface IContactRepository : IRepositoryBase<Contacts>
{
    Task<ContactResponse?> GetContactById(int id, CancellationToken cancellationToken);
    Task<List<ContactResponse>> GetContactsByCompanyId(int companyId, CancellationToken cancellationToken);
    Task<PagingResponseDto<ContactResponse>> SearchAsync(
        ContactSearchRequest<object> request, CancellationToken cancellationToken);
}