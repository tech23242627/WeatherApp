using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net;

namespace WeatherApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string APIkey = "f6f1a6e78e8d4dba0d76d93eb56ba551";

        private void btn_search_Click(object sender, EventArgs e)
        {
            getWeather();
        }

        private void getWeather()
        {
            using (WebClient web = new WebClient())
            {
                string url = string.Format ("https://api.openweathermap.org/data/2.5/weather?q={0}&APPID={1}", TbCity.Text, APIkey);
                var json = web.DownloadString(url);
                WeatherInfo.root Info = JsonConvert.DeserializeObject<WeatherInfo.root>(json);
                pic_icon.ImageLocation = "https://openweathermap.org/img/w/" + Info.weather[0].icon + ".png";
                lab_condition.Text = Info.weather[0].main;
                lab_detail.Text = Info.weather[0].description;
                lab_sunset.Text = Info.sys.sunset.ToString();
                lab_sunrise.Text = Info.sys.sunrise.ToString();
                lab_windspeed.Text = Info.wind.speed.ToString();
                lab_pressure.Text = Info.main.pressure.ToString();

            }                        
        }

        DateTime convertDateTime(long millisec)
        {
            DateTime day = new DateTime(2024, 3, 10, 15, 24, 16, DateTimeKind.Utc);
            day = day.AddSeconds(millisec).ToLocalTime();
            return day;
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }



}
