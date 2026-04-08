using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// Make sure you have these namespaces
using Microsoft.Extensions.Configuration;
using PasificKodeA.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
// Add Swagger/OpenAPI support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register repositories for DI
builder.Services.AddSingleton<DepartmentRepository>();
builder.Services.AddSingleton<EmployeeRepository>();

// Add CORS policy so frontend (React) can call backend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline
// Enable Swagger in Development so you can test the API via the UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowReactApp");  // Enable CORS

app.UseAuthorization();

app.MapControllers();

app.Run();