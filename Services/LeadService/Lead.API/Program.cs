using Lead.Application.Interfaces;
using Lead.Application.Services;
using Lead.Infrastructure;
using Lead.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
// ✅ Add Controllers with JSON options
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Convert enum values to strings in JSON
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

        // Handle property name case-insensitivity
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

builder.Services.AddAutoMapper(typeof(Lead.Application.Mappings.MappingProfile).Assembly);
// ⭐ Register Infrastructure Layer (DbContext + Repositories)
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<ILeadService, LeadService>();

// ✅ Add CORS - Allow specific origins
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins(
            "http://localhost:3000",      // React default
            "http://localhost:5173",
                        "http://localhost:5174",      // Vite default
                                                      // Vite default
            "http://localhost:4200"       // Angular default
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Lead API",
        Version = "v1",
        Description = "Lead Management Microservice API"
    });

    options.UseInlineDefinitionsForEnums();

});

var app = builder.Build();

// ⭐ Apply migrations on startup (Development only)
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<LeadDbContext>();
            await context.Database.MigrateAsync();
            Console.WriteLine("✅ Database migrations applied successfully");
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "❌ An error occurred while migrating the database");
        }
    }
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ✅ CRITICAL: UseCors MUST come before UseAuthorization and MapControllers
app.UseCors("AllowAll");

app.UseAuthorization();
app.MapControllers();

Console.WriteLine("🚀 Lead API is running...");

app.Run();

// ✅ Make the implicit Program class public so test projects can access it
public partial class Program { }