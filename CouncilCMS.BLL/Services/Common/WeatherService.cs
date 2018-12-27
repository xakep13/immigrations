using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Bissoft.CouncilCMS.BLL.ViewModels;

namespace Bissoft.CouncilCMS.BLL.Services
{
   public class WeatherService : BaseService
    {
        public async Task<CmsWeatherItem> GetCurrentWeather(double Lat, double Lng, string lang = "uk")
        {
            Uri weatherApi = new Uri("http://api.openweathermap.org/data/2.5/weather?" + "lat=" + Lat.ToString() + "&lon=" + Lng.ToString() +
                 "&units=metric&lang=" + lang + "&appid=468fbea3b4742c947af91ad1dbae54de");

            var client = new HttpClient();
            var response = await client.GetAsync(weatherApi);
            string text = await response.Content.ReadAsStringAsync();

            dynamic stuff =  JsonConvert.DeserializeObject<dynamic>(text);

            CmsWeatherItem weather = new CmsWeatherItem();

            weather.Temperature = (int)Math.Round((double)stuff.main.temp.Value);
            weather.Description = stuff.weather[0].description.Value;
            weather.Icon = stuff.weather[0].icon.Value;

            return weather;
        }
    }
}
