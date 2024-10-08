using CRUDServiceLibrary;
using CRUDServiceLibrary.Contracts;
using Microsoft.EntityFrameworkCore;
using TESTMyLib.Contracts;
using TESTMyLib.Data;
using TESTMyLib.MappingProfiles;
using TESTMyLib.Services;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.

builder.Services
    .AddScoped(typeof(ICRUDService<,,,,>), typeof(CRUDService<,,,,>))
    .AddScoped<ICarService, CarService>()
    .AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddAutoMapper(
    typeof(CarMappingProfile).Assembly,
    typeof(EmployeeMappingProfile).Assembly);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
