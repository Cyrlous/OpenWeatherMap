namespace OpenWeatherMap;
using System;
using Microsoft.Extensions.Configuration;

class Program
{
    static void Main(string[] args)
    {
        var client = new HttpClient();

        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();
        
        UserAccess.MainMenu(client, config);
    }
}