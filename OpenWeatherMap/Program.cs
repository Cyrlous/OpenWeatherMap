namespace OpenWeatherMap;
using System;
using Microsoft.Extensions.Configuration;

class Program
{
    private static async Task Main(string[] args)
    {
        using var client = new HttpClient();
        
        client.Timeout = TimeSpan.FromSeconds(30);

        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();
        
        await UserAccess.MainMenu(client, config);
    }
}