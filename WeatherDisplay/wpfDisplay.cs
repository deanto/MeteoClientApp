using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MeteoServer.Components.WeatherDisplay;
using MeteoServer.Objects;


namespace WPFClient.WeatherDisplay
{
    class wpfDisplay:ADisplayer
    {
        // это своя реализация показа погоды
        // внутри есть 

        public wpfDisplay() : base() { }

        public void ShowWeather(IUserID user, string map, string weather)
        {
            wpfWeatherDisplay wpfwd = new wpfWeatherDisplay();
            wpfwd.USER = user;
            wpfwd.MAP = map;
            wpfwd.WEATHER = weather;
            wpfwd.WC = wc;
            wpfwd.ShowDialog();
        }

    }
}
