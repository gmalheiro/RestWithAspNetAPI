using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using RestWithAspNetAPI.Business;
using RestWithAspNetAPI.Business.Implementations;
using RestWithAspNetAPI.Data;
using RestWithAspNetAPI.Data.Converter.Implementations;
using RestWithAspNetAPI.Models;
using RestWithAspNetAPI.Models.Context;
using RestWithAspNetAPI.Repository;
using RestWithAspNetAPI.Repository.Generic;
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

//Dependency injection
builder?.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
builder?.Services.AddScoped<IBookBusiness, BookBusinessImplementation>();

builder?.Services?.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

builder?.Services.AddScoped<PersonConverter>();
builder?.Services.AddScoped<BookConverter>();

builder?.Services.AddDbContext<MySQLContext>(options =>
                                            options.UseMySql(MySQLConnectionString,
                                            ServerVersion.AutoDetect(MySQLConnectionString)
                                            ));

builder.Services.AddMvc(options =>
{
    options.RespectBrowserAcceptHeader = true;

    options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
    options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
})
.AddXmlSerializerFormatters();

builder?.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    loggerConfiguration
        .WriteTo.Console();
});

var app = builder?.Build();

// Configure the HTTP request pipeline.
if (app?.Environment?.IsDevelopment() ?? true)
{
    MigrateDatabase.MigrateDb(MySQLConnectionString);
    app.UseSwagger();
    app.UseSwaggerUI();
}

app?.UseHttpsRedirection();

app?.UseAuthorization();

app?.MapControllers();

app?.Run();
