using Lead.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Lead.Tests.Fixtures;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove the app's DbContext registration
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<LeadDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Add DbContext using an in-memory database for testing
            services.AddDbContext<LeadDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryTestDb");
            });

            // Build the service provider
            var sp = services.BuildServiceProvider();

            // Create a scope to obtain a reference to the database context
            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<LeadDbContext>();
                
                try
                {
                    // Ensure the database is created
                    db.Database.EnsureCreated();
                    
                    // Seed test data
                    SeedTestData(db);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error seeding test database: {ex.Message}");
                }
            }
        });

        builder.UseEnvironment("Testing");
    }

    private static void SeedTestData(LeadDbContext context)
    {
        if (!context.Leads.Any())
        {
            var testLeads = LeadTestData.GenerateLeads(10);
            context.Leads.AddRange(testLeads);
            context.SaveChanges();
        }
    }
}
