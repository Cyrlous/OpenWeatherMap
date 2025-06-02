using System.Collections.Immutable;
using System.Net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
namespace OpenWeatherMap;

public class OpenWeatherMapAPI
{
    static public void GetUSWeather(HttpClient client, IConfiguration config)
    {
        string apiKey = config["ApiSettings:MyApiKey"];

        var city = "";
        var state = "";
        var country = "us";
        var weather = "";
        bool validInput = false;

        do
        {
            Console.WriteLine("Please enter the city name: ");
            city = Console.ReadLine();

            Console.WriteLine("Please enter the two digit state code: ");
            state = Console.ReadLine();

            var weatherAPI =
                $"https://api.openweathermap.org/data/2.5/weather?q={city},{state},{country}&appid=0f794640d6d7201d2ac496ca6ff3e06e&units=imperial";
            var weatherData = client.GetAsync(weatherAPI).Result;

            if (weatherData.IsSuccessStatusCode)
            {
                weather = weatherData.Content.ReadAsStringAsync().Result;
                validInput = true;
            }
            else if (weatherData.StatusCode == HttpStatusCode.NotFound)
            {
                Console.WriteLine("City with this state code is not valid.  Please try again.\n");
                validInput = false;
            }
            else
            {
                Console.WriteLine($"Request did not process correctly.  Please try again.\n");
                validInput = false;
            }
        } while (!validInput);

        var json = JObject.Parse(weather);
        
        var weatherMain = json.SelectToken("weather[0].description").ToString();
        var temp = json.SelectToken("main.temp").ToString();
        var feelsLike = json.SelectToken("main.feels_like").ToString();
        var humidity = json.SelectToken("main.humidity").ToString();
        var windSpeed = json.SelectToken("wind.speed").ToString();
        
        Console.WriteLine($"Weather data for {city}, {state} is as follows:");
        Console.WriteLine("------------------------------------------------");
        Console.WriteLine($"Weather: \t{weatherMain}\n");
        Console.WriteLine($"Temperature: \t{temp}° F");
        Console.WriteLine($"Feels Like: \t{feelsLike}° F");
        Console.WriteLine($"Humidity: \t{humidity}%\n");
        Console.WriteLine($"Wind Speed: \t{windSpeed} mph");
    }
    
    static public void GetWorldWeather(HttpClient client, IConfiguration config)
    {
        string apiKey = config["ApiSettings:MyApiKey"];

        var city = "";
        var country = "";
        var weather = "";
        bool validInput = false;

        do
        {
            Console.WriteLine("Please enter the city name: ");
            city = Console.ReadLine();

            Console.WriteLine("Please enter the two digit country code: ");
            Console.WriteLine("(Country codes may be found at https://countrycode.org/.  Use the two digit ISO code.)");
            country = Console.ReadLine();

            var weatherAPI =
                $"https://api.openweathermap.org/data/2.5/weather?q={city},{country}&appid=0f794640d6d7201d2ac496ca6ff3e06e&units=imperial";
            var weatherData = client.GetAsync(weatherAPI).Result;

            if (weatherData.IsSuccessStatusCode)
            {
                weather = weatherData.Content.ReadAsStringAsync().Result;
                validInput = true;
            }
            else if (weatherData.StatusCode == HttpStatusCode.NotFound)
            {
                Console.WriteLine("City with this country code is not valid.  Please try again.\n");
                validInput = false;
            }
            else
            {
                Console.WriteLine($"Request did not process correctly.  Please try again.\n");
                validInput = false;
            }
        } while (!validInput);

        var json = JObject.Parse(weather);
        
        var weatherMain = json.SelectToken("weather[0].description").ToString();
        var temp = json.SelectToken("main.temp").ToString();
        var feelsLike = json.SelectToken("main.feels_like").ToString();
        var humidity = json.SelectToken("main.humidity").ToString();
        var windSpeed = json.SelectToken("wind.speed").ToString();
        
        Console.WriteLine($"Weather data for {city}, {country} is as follows:");
        Console.WriteLine("------------------------------------------------");
        Console.WriteLine($"Weather: \t{weatherMain}\n");
        Console.WriteLine($"Temperature: \t{temp}° F");
        Console.WriteLine($"Feels Like: \t{feelsLike}° F");
        Console.WriteLine($"Humidity: \t{humidity}%\n");
        Console.WriteLine($"Wind Speed: \t{windSpeed} mph");
    }
    
    static public void GetWeatherByCoordinates(HttpClient client, IConfiguration config)
    {
        string apiKey = config["ApiSettings:MyApiKey"];

        var latitude = "";
        var longitude = "";
        var weather = "";
        bool validInput = false;

        do
        {
            Console.WriteLine("Please enter the latitude name: ");
            Console.WriteLine("(Latitude must be a value between -90 and 90)");
            latitude = Console.ReadLine();

            Console.WriteLine("Please enter the longitude: ");
            Console.WriteLine("(Longitude must be a value between -180 and 180)");
            longitude = Console.ReadLine();

            var weatherAPI =
                $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid=0f794640d6d7201d2ac496ca6ff3e06e&units=imperial";
            var weatherData = client.GetAsync(weatherAPI).Result;

            if (weatherData.IsSuccessStatusCode)
            {
                weather = weatherData.Content.ReadAsStringAsync().Result;
                validInput = true;
            }
            else if (weatherData.StatusCode == HttpStatusCode.NotFound)
            {
                Console.WriteLine("City with this country code is not valid.  Please try again.\n");
                validInput = false;
            }
            else
            {
                Console.WriteLine($"Request did not process correctly.  Please try again.\n");
                validInput = false;
            }
        } while (!validInput);

        var json = JObject.Parse(weather);
        
        var weatherMain = json.SelectToken("weather[0].description").ToString();
        var temp = json.SelectToken("main.temp").ToString();
        var feelsLike = json.SelectToken("main.feels_like").ToString();
        var humidity = json.SelectToken("main.humidity").ToString();
        var windSpeed = json.SelectToken("wind.speed").ToString();
        
        Console.WriteLine($"Weather data for latitude {latitude}, longitude {longitude} is as follows:");
        Console.WriteLine("------------------------------------------------");
        
        Console.WriteLine($"Weather: \t{weatherMain}\n");
        Console.WriteLine($"Temperature: \t{temp}° F");
        Console.WriteLine($"Feels Like: \t{feelsLike}° F");
        Console.WriteLine($"Humidity: \t{humidity}%\n");
        Console.WriteLine($"Wind Speed: \t{windSpeed} mph");
    }
}