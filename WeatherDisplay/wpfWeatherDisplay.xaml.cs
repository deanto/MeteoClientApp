using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using MeteoServer.Objects;
using MeteoServer.Components.WeatherCalculating;

namespace WPFClient.WeatherDisplay
{
    /// <summary>
    /// Interaction logic for wpfWeatherDisplay.xaml
    /// </summary>
    public partial class wpfWeatherDisplay : Window
    {
        

        public wpfWeatherDisplay()
        {
            InitializeComponent();
            slider1.Maximum = 1000;
        }


        private IUserID user;
        public IUserID USER { set { user = value; } }

        string map;
        public string MAP { set { map = value; } }

        string weather;
        public string WEATHER { set { weather = value; } }


        private WeatherCalculating wc;
        public WeatherCalculating WC { set { wc = value; } }



        List<WeatherCadr> video;
        int videopos;

        void ShowCadr()
        { 
        // нарисовать текущий кадр

            canvas1.Children.Clear();

            WeatherCadr now = video[videopos];
            canvas1.Background = Brushes.WhiteSmoke;

            for (int i = 0; i < now.Land.Count; i++)
            {
                Ellipse tmpLand = new Ellipse();

                tmpLand.Height = now.Land[i].R*2;
                tmpLand.Width = now.Land[i].R * 2;
                tmpLand.Fill = Brushes.SlateGray;
                tmpLand.Margin = new Thickness(now.Land[i].X - now.Land[i].R, now.Land[i].Y - now.Land[i].R, 0, 0);
                canvas1.Children.Add(tmpLand);

                

                Label tmptext = new Label();
                tmptext.Content = "t = "+now.Land[i].V;
                tmptext.Margin = new Thickness(now.Land[i].X , now.Land[i].Y , 0, 0);

                canvas1.Children.Add(tmptext);
                
            }

            for (int i = 0; i < now.Weather.Count; i++)
            {
                Ellipse tmpLand = new Ellipse();

                tmpLand.Height = now.Weather[i].R * 2;
                tmpLand.Width = now.Weather[i].R * 2;
                tmpLand.Fill = Brushes.YellowGreen;
                tmpLand.Margin = new Thickness(now.Weather[i].X - now.Weather[i].R, now.Weather[i].Y - now.Weather[i].R, 0, 0);
                canvas1.Children.Add(tmpLand);

                Label tmptext = new Label();
                tmptext.Content = "t = " + now.Weather[i].V;
                tmptext.Margin = new Thickness(now.Weather[i].X, now.Weather[i].Y, 0, 0);

                canvas1.Children.Add(tmptext);
            }

            slider1.Value = videopos;

            videopos++;


            if (videopos == video.Count&& slider1.Value<slider1.Maximum)
            { 
                // если кончилось видео а ползунок еще нет - нужно подгрузить видео еще
              List<WeatherCadr> newvideo =  wc.GetWeatherFromCadr(video[video.Count - 1], video[video.Count-1].TIME + 1, user, weather);
              for (int i = 0; i < newvideo.Count; i++) video.Add(newvideo[i]);
            }
            
        }

        System.Windows.Threading.DispatcherTimer dispatcherTimer;
        void ShowVideo()
        {
           dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

           dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
           dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, Convert.ToInt32(textBox1.Text));
           dispatcherTimer.Start();

        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            ShowCadr();
            if (videopos == video.Count) dispatcherTimer.Stop();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            video = wc.GetWeatherFromBegin(user, map, weather);
            videopos = 0;
            ShowCadr();
            
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            ShowVideo();
        }
    }
}
