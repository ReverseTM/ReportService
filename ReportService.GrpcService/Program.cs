using FluentValidation;
using Microsoft.EntityFrameworkCore;

using ReportService.GrpcService.Services;
using ReportService.Data.DbContexts;
using ReportService.Data.Entities;
using ReportService.Data.Interfaces;
using ReportService.Data.Repositories;
using ReportService.Proto.Validators;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddGrpc();

builder.Services.AddDbContext<ApplicationDbContext>(
    options =>
    {
        options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        
    }
);
builder.Services.AddTransient<DbContextFactory>();

builder.Services.AddValidatorsFromAssemblyContaining<ReportRequestValidator>();

builder.Services.AddScoped<IRepository<ReportEntity>, ReportRepository>();
builder.Services.AddScoped<IRepository<AuthorEntity>, AuthorRepository>();

var app = builder.Build();

app.MapGrpcService<ReportServiceImpl>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();