using Application;
using Business.Common.IoC;
using Data.Context;
using DomainChecker;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Shared.Mapping;
using Test.Data.Common.IoC;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

IWebHostEnvironment environment = builder.Environment;
MyConfiguration.Configuration = configuration;


builder.Services.AddControllers();
builder.Services.AddBusinessLayer();
builder.Services.AddDataLayer();
builder.Services.AddHealthChecks();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v2", new OpenApiInfo
    {
        Version = "v2",
        Title = "DomainChecker Swagger",
        Description = "DomainChecker Swagger",
        Contact = new OpenApiContact()
        {
            Name = "Swagger Implementation (DomainChecker API Developers)",
        },
    });
});

builder.Services.AddCors(options =>
{
      options.AddPolicy("AllowAllOrigins",
       builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddDbContext<DomainCheckDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "DomainChecker Swagger");
    });
}

MappingConfiguration.Init();

app.UseStaticFiles();

app.UseRouting();

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
