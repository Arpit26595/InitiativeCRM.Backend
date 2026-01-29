using Lead.Application.Interfaces;
using Lead.Domain.Entities;
using Lead.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Lead.Infrastructure.Repositories;

public class LeadRepository : ILeadRepository
{
    private readonly LeadDbContext _context;

    public LeadRepository(LeadDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Leads lead)
    {
        _context.Leads.Add(lead);
        await _context.SaveChangesAsync();
    }

 
    public async Task<List<Leads>> GetAllAsync()
    {
        return await _context.Leads.ToListAsync();
    }
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }


}
