using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XSunriseSunset
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Weather : ContentPage
    {
        public Weather()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ReadAPIAsync();
        }

        private async void ReadAPIAsync()
        {
            var lat = App.location.latitude;
            var lng = App.location.longtitude;
            //var name = myLoc.name;

            //await DisplayAlert("Location", "You have a location", "Ok");
            using (HttpClient client = new HttpClient())
            {
                try
                {

                    //Current Date, API can only do one date at a time, not a range
                    //DateTime now = DateTime.Now;
                    //string date = now.ToString("yyyy-MM-dd");
                    
                    var response2 = await client.GetAsync("https://api.openweathermap.org/data/2.5/weather?units=imperial&lat=" + lat + "&lon=" + lng + "&appid=70a389759cde3cbd0e38bcf1792cbbcc");
                    var json2 = await response2.Content.ReadAsStringAsync();

                    var results = JsonConvert.DeserializeObject<CityWeather>(json2);

                    string iconURL = "https://openweathermap.org/img/wn/";
                    string weatherICON = results.weather[0].icon + ".png";
                    string iconAddr = iconURL + weatherICON;
                    lblName.Text = results.name;
                    imageIcon.Source = iconAddr;                    
                    lblDesc.Text = "Conditions: " + results.weather[0].description;
                    lblTemp.Text = "Current Temperature " + results.main.temp.ToString();
                    lblFeelLike.Text = "Feels Like: " + results.main.feels_like.ToString();
                    lblTempMax.Text = "Maximum Temperature: " + results.main.temp_max.ToString();
                    lblTempMin.Text = "Minimun Temperature: " + results.main.temp_min.ToString();
                    lblBarometricPressure.Text = "Barometric Pressure: " + results.main.pressure.ToString();
                    lblHumidity.Text = "Humidity: " + results.main.humidity.ToString();
                    lblWindSpeed.Text = "Wind Speed: " + results.wind.speed.ToString();
                    lblWindDegrees.Text = "Wind Direction (Degrees): " + results.wind.deg.ToString();
                    lblWindGust.Text = "Wind Gusts: " + results.wind.gust.ToString();

                    // TODO - complete weather readout
                    // Icon Example: https://openweathermap.org/img/wn/01d.png

                }
                catch (Exception ex)
                {
                    _ = DisplayAlert("Error", "No response from API call. - " + ex.Message, "OK");
                }
            } // end using
        }

    }
}