using Lead.Application.Services;
using Lead.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // Needed for Swagger
builder.Services.AddSwaggerGen();            // Needed for Swagger UI

builder.Services.AddInfrastructure(builder.Configuration); // Your DI + DbContext
builder.Services.AddScoped<LeadService>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
