using Lead.Application.DTOs.Request;
using Lead.Application.DTOs.Response;
using Lead.Domain.Entities;
using Shared.Models.Utilities.Paging;

namespace Lead.Application.Interfaces;

public interface ILeadRepository : IRepositoryBase<Leads>
{
    Task<PagingResponseDto<LeadResponse>> SearchAsync(
        LeadSearchRequest<object> request, 
        CancellationToken cancellationToken = default);

    Task<Leads> GetLeadById(int id, CancellationToken cancellationToken = default);
}
