using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Asteria.Infra;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
                                   options.UseMySQL(configuration.GetSection("ConnectionStrings")["MySqlConnection"] ?? "",
                                    mysqlOptions => {
                                    mysqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                                    mysqlOptions.CommandTimeout(380);
                                   }));

        return services;
    }
}
