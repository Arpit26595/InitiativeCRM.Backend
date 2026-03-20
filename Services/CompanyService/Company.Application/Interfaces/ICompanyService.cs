using Company.Application.DTOs;
using Company.Application.DTOs.Request;
using Company.Application.DTOs.Response;
using Shared.Models.Utilities.Paging;

namespace Company.Application.Interfaces;

public interface ICompanyService
{
    Task CreateAsync(CreateCompanyDTO dto, CancellationToken cancellationToken);
    Task UpdateAsync(int id, CreateCompanyDTO dto, CancellationToken cancellationToken);
    Task<CompanyResponse?> GetCompanyById(int id, CancellationToken cancellationToken);
    Task<PagingResponseDto<CompanyResponse>> SearchAsync(
        CompanySearchRequest<object> request, CancellationToken cancellationToken);
}