using Asteria.Domain;
using Asteria.Infra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestBodySize = 200_000_000;
});

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<IVendasRepository, VendasRepository>();
builder.Services.AddScoped<IVendasService, VendasService>();
builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAntiforgery();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseAntiforgery();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();

