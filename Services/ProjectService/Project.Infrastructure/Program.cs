using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.Application.Interfaces;
using Project.Infrastructure.Persistence;
using Project.Infrastructure.Repositories;

namespace Project.Infrastructure
{
    public static class Program
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Get the connection string from appsettings.json
            var connectionString = configuration.GetConnectionString("Development");

            // Register EF Core DbContext with SQL Server
            services.AddDbContext<ProjectDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Register repository implementations
            services.AddScoped<IProjectRepository, ProjectRepository>();

            return services;
        }
    }
}
