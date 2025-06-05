using System.Collections.Immutable;
using System.Net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
namespace OpenWeatherMap;

public class OpenWeatherMapAPI
{
    public static void GetUSWeather(HttpClient client, IConfiguration config)
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
            city = Console.ReadLine();

            Console.WriteLine("Please enter the two digit state code: ");
            state = Console.ReadLine();

            var weatherAPI =
                $"https://api.openweathermap.org/data/2.5/weather?q={city},{state},us&appid=0f794640d6d7201d2ac496ca6ff3e06e&units=imperial";
            var weatherData = client.GetAsync(weatherAPI).Result;
            
            //proceed with weather data display if user input is valid
            if (weatherData.IsSuccessStatusCode)
            {
                weather = weatherData.Content.ReadAsStringAsync().Result;
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
    public static void GetWorldWeather(HttpClient client, IConfiguration config)
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
    public static void GetWeatherByCoordinates(HttpClient client, IConfiguration config)
    {
        string apiKey = config["ApiSettings:MyApiKey"];

        var latitude = "";
        var longitude = "";
        var weather = "";
        bool validInput = false;

        do
        {
            Console.WriteLine("Please enter the latitude: ");
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
        
        var weatherMain = json.SelectToken("weather[0].description").ToString();
        var temp = json.SelectToken("main.temp").ToString();
        var feelsLike = json.SelectToken("main.feels_like").ToString();
        var humidity = json.SelectToken("main.humidity").ToString();
        var windSpeed = json.SelectToken("wind.speed").ToString();
        
        Console.WriteLine($"\nWeather data for latitude {input1}, longitude {input2} is as follows:");
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

