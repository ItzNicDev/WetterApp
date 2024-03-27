using RestSharp;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Program
{
    public async static Task Main()
    {
        const string apiKey = "<YOUR-API-KEY>";

        Print();

        Console.Write("Enter your City: ");
        string city = Console.ReadLine();
        Console.Clear();

        var options = new RestClientOptions("https://api.openweathermap.org")
        {
            MaxTimeout = -1,
        };

        var client = new RestClient(options);
        var request =
            new RestRequest(
                $"/data/2.5/weather?q={city},DE&appid={apiKey}",
                Method.Get);

        for (;;)
        {
            RestResponse response = await client.ExecuteAsync(request);
            JObject jsonObject = JsonConvert.DeserializeObject<JObject>(response.Content);

            Console.WriteLine("Weather: " + jsonObject["weather"][0]["main"]);
            Console.WriteLine("Temperature Now: " + KelvinToCelcius(jsonObject["main"]["temp"].ToString()));
            Console.WriteLine("Temperature Min: " + KelvinToCelcius(jsonObject["main"]["temp_min"].ToString()));
            Console.WriteLine("Temperature Max: " + KelvinToCelcius(jsonObject["main"]["temp_max"].ToString()));
            Console.WriteLine("Feelded Temperature: " + KelvinToCelcius(jsonObject["main"]["feels_like"].ToString()));
            Console.WriteLine("Humidity: " + jsonObject["main"]["humidity"] + " %");
            Thread.Sleep(1 * 1000);
            Console.Clear();
        }
    }

    public static string KelvinToCelcius(string kelvin)
    {
        return (Convert.ToDecimal(kelvin) - 273.15m) + " °";
    }

    public static void Print()
    {
        Console.WriteLine("     .\n   \\ | /\n '-.;;;.-'\n-==;;;;;==-");
    }
}
