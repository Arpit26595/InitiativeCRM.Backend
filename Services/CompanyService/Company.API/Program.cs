using Company.Application.Interfaces;
using Company.Application.Services;
using Company.Infrastructure;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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




builder.Services.AddAutoMapper(typeof(Company.Application.Mappings.MappingProfile));

// ⭐ Register Infrastructure Layer (DbContext + Repositories)
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IContactService, ContactService>();


// ✅ Add CORS - Allow specific origins
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins(
            "http://localhost:3000",      // React default
            "http://localhost:5173",
            "http://localhost:5174",      // Vite default
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
        Title = "Company API",
        Version = "v1",
        Description = "Company Management Microservice API"
    });

    options.UseInlineDefinitionsForEnums();

});

var app = builder.Build();
Console.WriteLine($"Environment: {app.Environment.EnvironmentName}");


// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options => options.OpenApiVersion =
    Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0);
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ✅ CRITICAL: UseCors MUST come before UseAuthorization and MapControllers
app.UseCors("AllowAll");

app.UseAuthorization();
app.MapControllers();

Console.WriteLine("🚀 Company API is running...");

app.Run();

// ✅ Make the implicit Program class public so test projects can access it
public partial class Program { }