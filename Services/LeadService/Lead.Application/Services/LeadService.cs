using Lead.Application.DTOs;
using Lead.Application.Interfaces;
using Lead.Domain.Entities;
using System.Linq;

namespace Lead.Application.Services;

public class LeadService
{
    private readonly ILeadRepository _repository;

    public LeadService(ILeadRepository repository)
    {
        _repository = repository;
    }

    public async Task CreateAsync(CreateLeadDto dto)
    {
        var lead = new Leads(dto.Name, dto.Email);
        await _repository.AddAsync(lead);
        await _repository.SaveChangesAsync();
    }

    public async Task<List<Leads>> GetAllAsync()
    {
        var leads = await _repository.GetAllAsync();
        return leads.ToList();
    }
}
