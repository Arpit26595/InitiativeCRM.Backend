using Lead.Application.Interfaces;
using Lead.Domain.Entities;
using Lead.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Lead.Infrastructure.Repositories;

public class LeadActivityRepository : RepositoryBase<LeadActivity>, ILeadActivityRepository
{
    private readonly LeadDbContext _context;

    public LeadActivityRepository(LeadDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<LeadActivity>> GetByLeadIdAsync(int leadId, CancellationToken cancellationToken)
    {
        return await _context.Set<LeadActivity>()
            .AsNoTracking()
            .Where(x => x.LeadId == leadId)
            .OrderByDescending(x => x.ActivityDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<LeadActivity?> GetByIdAndLeadIdAsync(int leadId, int activityId, CancellationToken cancellationToken)
    {
        return await _context.Set<LeadActivity>()
            .FirstOrDefaultAsync(x => x.LeadId == leadId && x.LeadActivityId == activityId, cancellationToken);
    }
}