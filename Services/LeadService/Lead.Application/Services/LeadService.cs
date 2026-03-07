using AutoMapper;
using Lead.Application.DTOs;
using Lead.Application.DTOs.Request;
using Lead.Application.DTOs.Response;
using Lead.Application.Interfaces;
using Lead.Domain.Entities;
using Shared.Models.Utilities.Paging;

namespace Lead.Application.Services;

public class LeadService : ILeadService
{
    private readonly ILeadRepository _repository;
    private readonly IMapper _mapper;

    public LeadService(ILeadRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task CreateAsync(CreateLeadDto dto, CancellationToken cancellationToken)
    {
        try
        {

            var leadEntity = _mapper.Map<Leads>(dto);


            // TODO: Map DTO to entity and save
            await _repository.CreateAsync(leadEntity, cancellationToken);
        }
        catch (Exception ex)
        {
            // Log the exception (not implemented here)
            throw new ApplicationException("An error occurred while creating the lead.", ex);
        }
    }

    public async Task<PagingResponseDto<LeadResponse>> SearchAsync(
        LeadSearchRequest<object> request,
        CancellationToken cancellationToken)
    {
        // Call repository SearchAsync which applies filters, sorting, and pagination
        return await _repository.SearchAsync(request, cancellationToken);
    }

    public async Task<LeadResponse> GetLeadById(int id, CancellationToken cancellationToken)
    {
        var leadResponse = new LeadResponse();
        var lead = await _repository.GetByIdAsync(id, cancellationToken);
        return _mapper.Map(lead, leadResponse);
    }

    public async Task UpdateAsync(int id, CreateLeadDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var leadResponse = new LeadResponse();
            var lead = await _repository.GetByIdAsync(id, cancellationToken);
            var leadEntity = _mapper.Map<Leads>(dto);
            leadEntity.LeadId = id;
            if (leadEntity.LeadId == id)
            {
                await _repository.UpdateAsync(leadEntity, cancellationToken);
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while updating the lead.", ex);
        }
    }
}
