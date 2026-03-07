using Lead.Application.DTOs;
using Lead.Application.DTOs.Request;
using Lead.Application.DTOs.Response;
using Lead.Domain.Entities;
using Shared.Models.Utilities.Paging;

namespace Lead.Application.Interfaces;

public interface ILeadService
{
    Task CreateAsync(CreateLeadDto dto, CancellationToken cancellationToken);
    Task UpdateAsync(int id, CreateLeadDto dto, CancellationToken cancellationToken);


    Task<LeadResponse> GetLeadById(int id, CancellationToken cancellationToken);
    Task<PagingResponseDto<LeadResponse>> SearchAsync(LeadSearchRequest<object> request, CancellationToken cancellationToken);
}