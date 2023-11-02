using Microsoft.EntityFrameworkCore;
using RestWithAspNetAPI.Models.Context;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string MySQLConnectionString = builder?.Configuration?.GetConnectionString("MySQLConnectionString") ?? "";

builder?.Services.AddDbContext<MySQLContext>(options =>
                                            options.UseMySql(MySQLConnectionString,
                                            ServerVersion.AutoDetect(MySQLConnectionString)
                                            ));

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
