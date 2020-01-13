using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace metronome
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        MediaPlayer media;
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            //int bpm = Int16.Parse(textBox.Text);
            //  String path = "metronome.wav";

            int bpm =  Int16.Parse(textBox.Text);
            String path = "metronome.wav";
            media = new MediaPlayer();
          
            media.Open(new Uri(path, UriKind.RelativeOrAbsolute));

            /*   DispatcherTimer timerMusicTime = new DispatcherTimer();
               timerMusicTime.Interval = TimeSpan.FromSeconds(bpm);
               timerMusicTime.Tick += new EventHandler(timer_Tick);
               timerMusicTime.Start();*/
            /* Stopwatch s = new Stopwatch();
             using (var timer = new MultimediaTimer() { Interval = 1 })
             {
                 timer.Elapsed += (o, b) => {

                    // media.Position = TimeSpan.Zero;
                     media.Play();


                 };
                 s.Start();
                 timer.Start();

                 timer.Stop();
             }*/
            Multimedia.Timer time = new Multimedia.Timer();
            time.Mode = Multimedia.TimerMode.Periodic;
            time.Period = bpm;
            time.Resolution = 1;
          //  time.SynchronizingObject = this;
            time.Tick += (s, b) =>
            {
              //  media.Position = TimeSpan.Zero;
                media.Play();
              
            };
            time.Start();
            // Play();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
           
           
            media.Play();
            media.Position = TimeSpan.Zero;
        }



        /* async void PlayAsync()
         {

             await Task.Run(()=>Play());
         }*/
        private void button_Click(object sender, RoutedEventArgs e)
        {
            int numbpm = Int16.Parse(textBox.Text);
            numbpm += 1;
            textBox.Text = numbpm.ToString();
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {

            int numbpm = Int16.Parse(textBox.Text);
            numbpm -= 1;
            textBox.Text = numbpm.ToString();
        }
    }
}
