using Lead.Application.Interfaces;
using Lead.Domain.Entities;
using Lead.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lead.Infrastructure.Repositories
{
    public class LeadDocumentRepository : RepositoryBase<LeadDocument>, ILeadDocumentRepository
    {
        private readonly LeadDbContext _context;
        public LeadDocumentRepository(LeadDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<LeadDocument?> GetByIdAndLeadIdAsync(int leadId, int documentId, CancellationToken cancellationToken)
        {
            return await _context.Set<LeadDocument>()
             .FirstOrDefaultAsync(x => x.LeadId == leadId && x.DocumentId == documentId, cancellationToken);
        }

        public async Task<IReadOnlyList<LeadDocument>> GetByLeadIdAsync(int leadId, CancellationToken cancellationToken)
        {
            return await _context.Set<LeadDocument>()
                                  .AsNoTracking()
                                  .Where(x => x.LeadId == leadId)
                                  .OrderByDescending(x => x.CreatedDate)
                                  .ToListAsync(cancellationToken);
        }
    }
}
