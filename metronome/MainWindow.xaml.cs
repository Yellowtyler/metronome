using Multimedia;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Media;
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
           
            _time = new Multimedia.Timer(); 
            _time.Period = 250;
            System.Threading.Thread thisThread = System.Threading.Thread.CurrentThread;
            thisThread.Priority = System.Threading.ThreadPriority.Highest;
        }
       
        SoundPlayer tik;
        SoundPlayer tok;
        Multimedia.Timer _time;
        private  bool isPlaying = false;
        private int count = 0;
        private  int timesignature = 4;
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            int bpm =  Int16.Parse(textBox.Text);
            tik = new SoundPlayer(Properties.Resources.tik);
            tok = new SoundPlayer(Properties.Resources.tok);
           

        
            if (!isPlaying)
            {
                _time.Period = (int)(1000 * (60f / bpm));
                _time.Start();
                count = 0;
                _time.Tick += _timer_Tick;
                isPlaying = true;
                Start.Content = "Stop";
            }
            else
            {
                _time.Tick -= _timer_Tick ;
                _time.Stop();
                count = 0;
                isPlaying = false;
                Start.Content = "Start";
               
            }
        }



      

    




    private void _timer_Tick(object sender,EventArgs e)
        {
            count++;
            if (count == 1) tok.Play();
            else tik.Play();
           
            if (count == timesignature) count = 0;
         
        }

 

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

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch(comboBox.SelectedIndex)
            {
                case 0:
                    timesignature = 4;
                    break;
                case 1:
                    timesignature = 5;
                    break;
                case 2:
                    timesignature = 6;
                    break;
            }
        }
    }
}
