using infrastructure.DataSources;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.OpenApi.Models;
using service.Services;

namespace api;

public static class ServiceCollectionExtensions
{
    
    public static void AddDataSource(this IServiceCollection services)
    {
        services.AddSingleton<IDataSource>(provider =>
        {
            const string name = "WebApiDatabase";
            var config = provider.GetService<IConfiguration>()!;
            var connectionString = config.GetConnectionString(name);

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException($"Connection string named '{name}'");

            if (connectionString.StartsWith("postgres://"))
            {
                var uri = new Uri(connectionString);
                return new PostgresDataSource(
                    $"""
                     Host={uri.Host};
                     Database={uri.AbsolutePath.Trim('/')};
                     User Id={uri.UserInfo.Split(':')[0]};
                     Password={uri.UserInfo.Split(':')[1]};
                     Port={(uri.Port > 0 ? uri.Port : 5432)};
                     Pooling=true;
                     MaxPoolSize=3
                     """
                );
            }

            throw new InvalidOperationException($"Unsupported connection string: ${connectionString}");
        });
    }

    public static void AddJwtService(this IServiceCollection services)
    {
        services.AddSingleton<JwtOptions>(services =>
        {
            var configuration = services.GetRequiredService<IConfiguration>();
            var options = configuration.GetRequiredSection("JWT").Get<JwtOptions>()!;

            if (string.IsNullOrEmpty(options?.Address))
            {
                var server = services.GetRequiredService<IServer>();
                var addresses = server.Features.Get<IServerAddressesFeature>()?.Addresses;
                options.Address = addresses?.FirstOrDefault();
            }

            return options;
        });
        services.AddSingleton<JwtService>();
    }
}