using BensWorkbench.Models;
using BensWorkbench.General.Weather;

namespace BensWorkbench.Services;

public class WeatherService
{
    private HttpClient InternalHttpClient { get; set; }

    public WeatherService()
    {
        InternalHttpClient = new HttpClient();
        InternalHttpClient.BaseAddress = new Uri("https://api.weather.gov/");
    }

    public async Task<Result<String>> GetWeather(double lng, double lat)
    {
        var weatherApiRequest = await InternalHttpClient.GetAsync($"Points/{lng},{lat}");
        return await weatherApiRequest.Content.ReadAsStringAsync();
    }
}
