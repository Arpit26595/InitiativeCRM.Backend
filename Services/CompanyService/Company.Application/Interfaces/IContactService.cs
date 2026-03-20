using Company.Application.DTOs;
using Company.Application.DTOs.Request;
using Company.Application.DTOs.Response;
using Shared.Models.Utilities.Paging;

namespace Company.Application.Interfaces;

public interface IContactService
{
    Task CreateAsync(CreateContactDTO dto, CancellationToken cancellationToken);
    Task UpdateAsync(int id, CreateContactDTO dto, CancellationToken cancellationToken);
    Task<ContactResponse?> GetContactById(int id, CancellationToken cancellationToken);
    Task<List<ContactResponse>> GetContactsByCompanyId(int companyId, CancellationToken cancellationToken);
    Task<PagingResponseDto<ContactResponse>> SearchAsync(
        ContactSearchRequest<object> request, CancellationToken cancellationToken);
}