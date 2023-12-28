using BusinessRegistrationService.Data.Context;
using BusinessRegistrationService.Data.Interfaces;
using BusinessRegistrationService.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessRegistrationService.Data;

public static class Extensions
{
    public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
    {
        //Repositories
        services.AddScoped<IBusinessDetailRepository, BusinessDetailRepository>();
        
        //Unit of work
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        //Database context connection
        services.AddDbContext<AppDbContext>(ctx => 
            ctx.UseNpgsql(
                configuration.GetConnectionString("DB"),
                x =>
                {
                    x.MigrationsHistoryTable("__MyMigrationsHistory");
                })
            );
        
        return services;
    }

    public static IServiceProvider RunMigrations(this IServiceProvider provider)
    {
        // run migrations
        Console.WriteLine( $"Starting Migrations" );
        using (var scope = provider.CreateScope())
        {
            try
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                context.Database.Migrate();
                Console.WriteLine("Migrations Completed");
            }
            catch (Exception e)
            {
                Console.WriteLine("Migration Failed: " + e.Message);
            }
    
        }
        return provider;
    }

}