using Company.Application.Interfaces;
using Company.Infrastructure.Persistence;
using Company.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;     // UseSqlServer
using Microsoft.Extensions.Configuration; // GetConnectionString
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging; // AddDbContext, AddScoped

namespace Company.Infrastructure;

public static class Program
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Get the connection string from appsettings.json
        var connectionString = configuration.GetConnectionString("Development");

        // Register EF Core DbContext with SQL Server
        //services.AddDbContext<LeadDbContext>(options =>
        //    options.UseSqlServer(connectionString));

        services.AddDbContextPool<CompanyDbContext>(x => x.UseSqlServer(connectionString, a => a.MigrationsAssembly("Company.Infrastructure")).EnableSensitiveDataLogging() // Optional for debugging
            .LogTo(Console.WriteLine, LogLevel.Information)); // Log SQL command);


        // Register repository implementations
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IContactRepository, ContactRepository>();


        return services;
    }
}
