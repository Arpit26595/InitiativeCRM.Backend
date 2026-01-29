using Lead.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lead.Infrastructure.Persistence;


public class LeadDbContext : DbContext
{
    public LeadDbContext(DbContextOptions<LeadDbContext> options)
        : base(options) { }

    public DbSet<Leads> Leads => Set<Leads>();
}
