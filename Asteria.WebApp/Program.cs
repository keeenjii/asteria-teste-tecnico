using Asteria.Domain;
using Asteria.Infra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestBodySize = 200_000_000;
});

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<IVendasRepository, VendasRepository>();
builder.Services.AddScoped<IVendasService, VendasService>();


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

app.MapPost("/addmigrations", () =>
{
    try
    {
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            dbContext.Database.Migrate();
        }
        return "Migration created";
    }
    catch (Exception ex)
    {
        return ex.Message;
    }
})
.WithName("addmigration")
.WithOpenApi();


app.MapPost("/upload",
async (IFormFile file, CancellationToken ct) =>
{
    if (file != null && file.Length > 0)
    {
        using (var scope = app.Services.CreateScope())
        {
            var vendasService = scope.ServiceProvider.GetRequiredService<IVendasService>();

            return await vendasService.Upload(file, ct);
        }
    }
    else
    {
        return "No file uploaded";
    }
})
.WithName("upload")
.WithOpenApi()
.DisableAntiforgery(); 
app.Run();
