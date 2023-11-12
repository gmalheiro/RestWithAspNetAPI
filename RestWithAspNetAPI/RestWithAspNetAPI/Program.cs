using Microsoft.EntityFrameworkCore;
using RestWithAspNetAPI.Business.Implementations;
using RestWithAspNetAPI.Business;
using RestWithAspNetAPI.Models.Context;
using RestWithAspNetAPI.Repository;
using RestWithAspNetAPI.Repository.Implementations;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string MySQLConnectionString = builder?.Configuration?.GetConnectionString("MySQLConnectionString") ?? "";

builder?.Services.AddApiVersioning();

builder?.Services.AddScoped<IPersonBusiness,PersonBusinessImplementation>();
builder?.Services.AddScoped<IPersonRepository, PersonRepositoryImplementation>();

builder?.Services.AddDbContext<MySQLContext>(options =>
                                            options.UseMySql(MySQLConnectionString,
                                            ServerVersion.AutoDetect(MySQLConnectionString)
                                            ));

builder?.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    loggerConfiguration
        .WriteTo.Console();
});

var app = builder?.Build();

// Configure the HTTP request pipeline.
if (app?.Environment?.IsDevelopment() ?? true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app?.UseHttpsRedirection();

app?.UseAuthorization();

app?.MapControllers();

app?.Run();
