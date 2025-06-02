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
        
        OpenWeatherMapAPI.GetUSWeather(client, config);
        OpenWeatherMapAPI.GetWorldWeather(client, config);
        OpenWeatherMapAPI.GetWeatherByCoordinates(client, config);
    }
}