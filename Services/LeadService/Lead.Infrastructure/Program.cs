using Lead.Application.Interfaces;       // ILeadRepository
using Lead.Infrastructure.Persistence;   // LeadDbContext
using Lead.Infrastructure.Repositories;  // LeadRepository
using Microsoft.EntityFrameworkCore;     // UseSqlServer
using Microsoft.Extensions.Configuration; // GetConnectionString
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging; // AddDbContext, AddScoped

namespace Lead.Infrastructure;

public static class Program
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Get the connection string from appsettings.json
        var connectionString = configuration.GetConnectionString("Development");

        // Register EF Core DbContext with SQL Server
        //services.AddDbContext<LeadDbContext>(options =>
        //    options.UseSqlServer(connectionString));

        services.AddDbContextPool<LeadDbContext>(x => x.UseSqlServer(connectionString, a => a.MigrationsAssembly("Lead.Infrastructure")).EnableSensitiveDataLogging() // Optional for debugging
            .LogTo(Console.WriteLine, LogLevel.Information)); // Log SQL command);


        // Register repository implementations
        services.AddScoped<ILeadRepository, LeadRepository>();
        services.AddScoped<ILeadNoteRepository, LeadNoteRepository>();
        services.AddScoped<ILeadActivityRepository, LeadActivityRepository>();

        services.AddScoped<ILeadDocumentRepository, LeadDocumentRepository>();

        return services;
    }
}
