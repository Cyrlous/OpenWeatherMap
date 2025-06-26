using System.Net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
namespace OpenWeatherMap;

public class OpenWeatherMapAPI
{
    public static async Task GetUSWeather(HttpClient client, IConfiguration config)
    {
        //get API key
        string apiKey = config["ApiSettings:MyApiKey"];

        //declare variables
        var city = "";
        var state = "";
        var weather = "";
        bool validInput = false;
        
        //receive and validate user input
        do
        {
            Console.WriteLine("Please enter the city name: ");
            city = Console.ReadLine() ?? "";

            Console.WriteLine("Please enter the two digit state code: ");
            state = Console.ReadLine() ?? "";

            var weatherAPI =
                $"https://api.openweathermap.org/data/2.5/weather?q={city},{state},us&appid={apiKey}&units=imperial";
            var weatherData = await client.GetAsync(weatherAPI);
            
            //proceed with weather data display if user input is valid
            if (weatherData.IsSuccessStatusCode)
            {
                weather = await weatherData.Content.ReadAsStringAsync();
                validInput = true;
            }
            //prompt user to try again if input is invalid
            else if (weatherData.StatusCode == HttpStatusCode.NotFound)
            {
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine("City with this state code is not valid.  Please try again.");
                Console.WriteLine("----------------------------------------------------------\n");
                validInput = false;
            }
            //prompt user to try again if something unexpected occurs while processing user input
            else
            {
                Console.WriteLine("-----------------------------------------------------");
                Console.WriteLine("Request did not process correctly.  Please try again.");
                Console.WriteLine("-----------------------------------------------------\n");
                validInput = false;
            }
        } while (!validInput);
        
        //call method to display weather data
        DisplayWeatherData(weather, city, state);
    }
    
    //see GetUSWeather method for details on each section of code
    public static async Task GetWorldWeather(HttpClient client, IConfiguration config)
    {
        string apiKey = config["ApiSettings:MyApiKey"];

        var city = "";
        var country = "";
        var weather = "";
        bool validInput = false;

        do
        {
            Console.WriteLine("Please enter the city name: ");
            city = Console.ReadLine() ?? "";

            Console.WriteLine("Please enter the two digit country code: ");
            Console.WriteLine("(Country codes may be found at https://countrycode.org/.  Use the two digit ISO code.)");
            country = Console.ReadLine() ?? "";

            var weatherAPI =
                $"https://api.openweathermap.org/data/2.5/weather?q={city},{country}&appid={apiKey}&units=imperial";
            var weatherData = await client.GetAsync(weatherAPI);

            if (weatherData.IsSuccessStatusCode)
            {
                weather = await weatherData.Content.ReadAsStringAsync();
                validInput = true;
            }
            else if (weatherData.StatusCode == HttpStatusCode.NotFound)
            {
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("City with this country code is not valid.  Please try again.");
                Console.WriteLine("------------------------------------------------------------\n");
                validInput = false;
            }
            else
            {
                Console.WriteLine("-----------------------------------------------------");
                Console.WriteLine("Request did not process correctly.  Please try again.");
                Console.WriteLine("-----------------------------------------------------\n");
                validInput = false;
            }
        } while (!validInput);
        
        DisplayWeatherData(weather, city, country);
    }
    
    //see GetUSWeather method for details on each section of code
    public static async Task GetWeatherByCoordinates(HttpClient client, IConfiguration config)
    {
        string apiKey = config["ApiSettings:MyApiKey"];

        var latitude = "";
        var longitude = "";
        var weather = "";
        var loc1 = "latitude";
        var loc2 = "longitude";
            
        bool validInput = false;

        do
        {
            Console.WriteLine("Please enter the latitude: ");
            Console.WriteLine("(Latitude must be a value between -90 and 90)");
            latitude = Console.ReadLine() ?? "";

            Console.WriteLine("Please enter the longitude: ");
            Console.WriteLine("(Longitude must be a value between -180 and 180)");
            longitude = Console.ReadLine() ?? "";

            var weatherAPI =
                $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={apiKey}&units=imperial";
            var weatherData = await client.GetAsync(weatherAPI);

            if (weatherData.IsSuccessStatusCode)
            {
                weather = await weatherData.Content.ReadAsStringAsync();
                validInput = true;
            }
            else if (weatherData.StatusCode == HttpStatusCode.NotFound)
            {
                Console.WriteLine("---------------------------------------------------");
                Console.WriteLine("These coordinates are not valid.  Please try again.");
                Console.WriteLine("---------------------------------------------------\n");
                validInput = false;
            }
            else
            {
                Console.WriteLine("-----------------------------------------------------");
                Console.WriteLine("Request did not process correctly.  Please try again.");
                Console.WriteLine("-----------------------------------------------------\n");
                validInput = false;
            }
        } while (!validInput);

        DisplayWeatherData(weather, latitude, longitude);
    }

    //method to display weather data to console
    public static void DisplayWeatherData(string weather, string input1, string input2)
    {
        var json = JObject.Parse(weather);
        
        var weatherMain = json.SelectToken("weather[0].description")?.ToString() ?? "Unknown";
        var temp = json.SelectToken("main.temp")?.ToString() ?? "Unknown";
        var feelsLike = json.SelectToken("main.feels_like")?.ToString() ?? "Unknown";
        var humidity = json.SelectToken("main.humidity")?.ToString() ?? "Unknown";
        var windSpeed = json.SelectToken("wind.speed")?.ToString() ?? "Unknown";
        
        Console.WriteLine($"\nWeather data for {input1}, {input2} is as follows:");
        Console.WriteLine("------------------------------------------------");
        Console.WriteLine($"Weather: \t{weatherMain}\n");
        Console.WriteLine($"Temperature: \t{temp}° F");
        Console.WriteLine($"Feels Like: \t{feelsLike}° F");
        Console.WriteLine($"Humidity: \t{humidity}%\n");
        Console.WriteLine($"Wind Speed: \t{windSpeed} mph");
        Console.WriteLine("------------------------------------------------\n");
        Console.WriteLine($"Please press enter to continue...");
        Console.ReadLine();
    }
}