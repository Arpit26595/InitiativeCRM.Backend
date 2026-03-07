using Lead.Application.Interfaces;       // ILeadRepository
using Lead.Infrastructure.Persistence;   // LeadDbContext
using Lead.Infrastructure.Repositories;  // LeadRepository
using Microsoft.EntityFrameworkCore;     // UseSqlServer
using Microsoft.Extensions.Configuration; // GetConnectionString
using Microsoft.Extensions.DependencyInjection; // AddDbContext, AddScoped

namespace Lead.Infrastructure;

public static class Program
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Get the connection string from appsettings.json
        var connectionString = configuration.GetConnectionString("Development");

        // Register EF Core DbContext with SQL Server
        services.AddDbContext<LeadDbContext>(options =>
            options.UseSqlServer(connectionString));

        // Register repository implementations
        services.AddScoped<ILeadRepository, LeadRepository>();

        return services;
    }
}
