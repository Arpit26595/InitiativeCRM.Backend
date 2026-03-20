using Lead.Application.Interfaces;
using Lead.Domain.Entities;
using Lead.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Lead.Infrastructure.Repositories;

public class LeadNoteRepository : RepositoryBase<LeadNote>, ILeadNoteRepository
{
    private readonly LeadDbContext _context;

    public LeadNoteRepository(LeadDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<LeadNote>> GetByLeadIdAsync(int leadId, CancellationToken cancellationToken)
    {
        return await _context.Set<LeadNote>()
            .AsNoTracking()
            .Where(x => x.LeadId == leadId)
            .OrderByDescending(x => x.CreatedDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<LeadNote?> GetByIdAndLeadIdAsync(int leadId, int noteId, CancellationToken cancellationToken)
    {
        return await _context.Set<LeadNote>()
            .FirstOrDefaultAsync(x => x.LeadId == leadId && x.LeadNoteId == noteId, cancellationToken);
    }
}