using Company.Application.DTOs.Request;
using Company.Application.DTOs.Response;
using Company.Domain.Entities;
using Shared.Models.Utilities.Paging;

namespace Company.Application.Interfaces;

public interface ICompanyRepository : IRepositoryBase<Companies>
{
    Task<CompanyResponse?> GetCompanyById(int id, CancellationToken cancellationToken);
    Task<PagingResponseDto<CompanyResponse>> SearchAsync(
        CompanySearchRequest<object> request, CancellationToken cancellationToken);
}