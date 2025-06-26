using Microsoft.Extensions.Configuration;

namespace OpenWeatherMap;

public class UserAccess
{
    public static async Task MainMenu(HttpClient client, IConfiguration config)
    {
        var answer = "";
        Console.WriteLine("Welcome to Weather Finder.\n\n");
        do
        {
            //display main menu
            Console.WriteLine("Please select from the following options:");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("1. Get Weather Data By City");
            Console.WriteLine("2. Get Weather Data By Global Coordinates");
            Console.WriteLine("3. Exit");

            answer = Console.ReadLine() ?? "";

            switch (answer)
            {
                //runs the CitySubMenu method
                case "1":
                {
                    await CitySubMenu(client, config);
                    break;
                }
                //runs the GetWeatherByCoordinates method from OpenWeatherMapAPI class
                case "2":
                {
                    await OpenWeatherMapAPI.GetWeatherByCoordinates(client, config);
                    break;
                }
                //exits program
                case "3":
                {
                    Console.WriteLine("Thank you for using the Weather Finder.  Have a great day!");
                    break;
                }
                //returns user to main and prompts to try again
                default:
                {
                    Console.WriteLine("Invalid Choice.  Please try again.");
                    break;
                }
            }
        } while (answer != "3");
    }

    public static void CitySubMenu(HttpClient client, IConfiguration config)
    {
        var answer = "";
        do
        {
            //display city sub menu
            Console.WriteLine("Please select from the following options:");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("1. Select City within the United States");
            Console.WriteLine("2. Select City outside of the United States");
            Console.WriteLine("3. Return to Main Menu");
            
            answer = Console.ReadLine();

            switch(answer)
            {
                //runs the GetUSWeather method from OpenWeatherMapAPI class
                case "1":
                {
                    OpenWeatherMapAPI.GetUSWeather(client, config);
                    answer = "3";
                    break;
                }
                //runs the GetWorldWeather method from OpenWeatherMapAPI class
                case "2":
                {
                    OpenWeatherMapAPI.GetWorldWeather(client, config);
                    answer = "3";
                    break;
                }
                //returns user to main menu
                case "3":
                {
                    break;
                }
                //returns user to city sub menu and prompts to try again
                default:
                {
                    Console.WriteLine("Invalid Choice.  Please try again.\n");
                    break;
                }
            }
        } while(answer != "3");
    }
}