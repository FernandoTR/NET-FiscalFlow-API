using FiscalFlow.Application;
using FiscalFlow.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddApplicationServices();
builder.AddInfrastructureServices();

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Authentication / Authorization
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

// Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();