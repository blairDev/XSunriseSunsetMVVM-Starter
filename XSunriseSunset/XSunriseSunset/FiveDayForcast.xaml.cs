
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XSunriseSunset
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FiveDayForcast : ContentPage
    {
        public FiveDayForcast()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _ = ReadAPIForcastWeatherDataAsync();
        }

        private async Task ReadAPIForcastWeatherDataAsync()
        {
            var lat = App.location.latitude;
            var lng = App.location.longtitude;
            //var name = myLoc.name;

            //await DisplayAlert("Location", "You have a location", "Ok");
            using (HttpClient client = new HttpClient())
            {
                try
                {                    
                    var response2 = await client.GetAsync("https://api.openweathermap.org/data/2.5/onecall?lat=" + App.location.latitude + "&lon=" + App.location.longtitude + "&units=imperial&exclude=hourly,minutely&appid={YOUR KEY HERE}");
                    var json2 = await response2.Content.ReadAsStringAsync();

                    var results = JsonConvert.DeserializeObject<Forcast>(json2);

                    List<DisplayWeather> displayWeather = new List<DisplayWeather>();
                    int cnt = 1;

                    foreach(var day in results.daily)
                    {
                        DisplayWeather tempWeather = new DisplayWeather();

                        DateTime localDateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(day.dt).DateTime.ToLocalTime();
                        string dayOfWeek = localDateTimeOffset.DayOfWeek.ToString();


                        if (cnt % 2 == 0)
                        {
                            tempWeather.bgColor = "AliceBlue";
                        }                        
                        else
                        {
                            tempWeather.bgColor = "Aquamarine";
                        }

                        tempWeather.localDateTimeOffset = dayOfWeek + " " +localDateTimeOffset.ToShortDateString();
                        tempWeather.min = "Low: " + day.temp.min;
                        tempWeather.max = "High: " + day.temp.max;
                        tempWeather.pressure = "hPa: " + day.pressure;
                        tempWeather.humidity = "Humidity: " + day.humidity;
                        tempWeather.windSpeed = "Wind: " + day.wind_speed;
                        tempWeather.windDirection = "Dir: " + day.wind_deg;
                        tempWeather.windGust = "Gust: " + day.wind_gust;
                        tempWeather.desc = day._weather[0].description;

                        displayWeather.Add(tempWeather);

                        cnt++;
                    }
                                                                                                 
                    lstForcast.ItemsSource = displayWeather;
                }
                catch (Exception ex)
                {
                    _ = DisplayAlert("Error", "No response from API call. - " + ex.Message, "OK");
                }
            } // end using
        }

    }
}