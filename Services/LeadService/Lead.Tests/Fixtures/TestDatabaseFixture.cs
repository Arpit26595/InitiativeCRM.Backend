using Lead.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;

namespace Lead.Tests.Fixtures;

public class TestDatabaseFixture : IDisposable
{
    public LeadDbContext Context { get; private set; }

    public TestDatabaseFixture()
    {
        var options = new DbContextOptionsBuilder<LeadDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        Context = new LeadDbContext(options);
        Context.Database.EnsureCreated();

        // Seed test data
        SeedTestData();
    }

    private void SeedTestData()
    {
        var testLeads = LeadTestData.GenerateLeads(20);
        Context.Leads.AddRange(testLeads);
        Context.SaveChanges();
    }

    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();
    }
}
