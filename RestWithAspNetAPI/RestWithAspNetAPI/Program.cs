using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using RestWithAspNetAPI.Business;
using RestWithAspNetAPI.Business.Implementations;
using RestWithAspNetAPI.Configuration;
using RestWithAspNetAPI.Data;
using RestWithAspNetAPI.Data.Converter.Implementations;
using RestWithAspNetAPI.Hypermedia.Enricher;
using RestWithAspNetAPI.Hypermedia.Filters;
using RestWithAspNetAPI.Models.Context;
using RestWithAspNetAPI.Repository;
using RestWithAspNetAPI.Repository.Generic;
using RestWithAspNetAPI.Services;
using RestWithAspNetAPI.Services.Implementations;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string PostgreSQLConnectionString= builder?.Configuration?.GetConnectionString("PostgreConnectionString") ?? "";

var filterOptions = new HyperMediaFilterOptions();

filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());

filterOptions.ContentResponseEnricherList.Add(new BookEnricher());

builder?.Services.AddSingleton(filterOptions);

builder?.Services.AddApiVersioning();

builder?.Services.AddCors(options => options.AddDefaultPolicy(builder =>
{
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
}));

//Dependency injection
builder?.Services.TryAddSingleton<IHttpContextAccessor,HttpContextAccessor>();

builder?.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();

builder?.Services.AddScoped<IBookBusiness, BookBusinessImplementation>();

builder?.Services.AddScoped<ILoginBusiness,LoginBusinessImplementation>();

builder?.Services.AddScoped<IUserRepository, UserRepository>();

builder?.Services.AddScoped<IPersonRepository, PersonRepository>();

builder?.Services.AddScoped<IFileBusiness,FileBusinessImplementation>();

builder?.Services.AddTransient<ITokenService, TokenService>();

builder?.Services?.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

builder?.Services.AddScoped<PersonConverter>();
builder?.Services.AddScoped<BookConverter>();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder?.Services.AddDbContext<PostgreSQLContext>(options =>options.UseNpgsql(PostgreSQLConnectionString));

var tokenConfigurations = new TokenConfiguration();

new ConfigureFromConfigurationOptions<TokenConfiguration>(
        builder!.Configuration.GetSection("TokenConfigurations")
    )
    .Configure(tokenConfigurations);

builder?.Services.AddSingleton(tokenConfigurations);

builder?.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = tokenConfigurations.Issuer,
        ValidAudience = tokenConfigurations.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigurations?.Secret ?? ""))
    };
});

builder?.Services.AddAuthorization(auth =>
{
    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser().Build());
});

builder?.Services.AddMvc(options =>
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
    MigrateDatabase.MigrateDb(PostgreSQLConnectionString);
    app.UseSwagger();
    app.UseSwaggerUI();
}

app?.UseHttpsRedirection();

app?.UseCors();

app?.UseAuthorization();

app?.MapControllers();
app?.MapControllerRoute("DefaultApi", "{controller=values}/v{version=apiVersion}/{id?}");

app?.Run();
