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
            string g = "";
            string b = "";
            blacks = new Rectangle[4];
            greys = new Rectangle[4];
            for (int i = 0; i < 4; i++)
            {
                b = "rectB" + (i + 1).ToString();
                g = "rectG" + (i + 1).ToString();
                blacks[i] = FindChild<Rectangle>(grid, b);
                greys[i] = FindChild<Rectangle>(grid, g);
            }
        }
        Rectangle[] blacks;
        Rectangle[] greys;
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
                for(int i=0;i<timesignature;i++)
                {

                    blacks[i].Visibility = Visibility.Visible;
                    greys[i].Visibility = Visibility.Hidden;
                }
               
            }
        }



      

    




    private void _timer_Tick(object sender,EventArgs e)
        {
            
            

            if (count == 0)
            { tok.Play();
              

            }
            else tik.Play();
            this.Dispatcher.Invoke(() =>
            {
                if (count != 0)
                {

                    blacks[count].Visibility = Visibility.Hidden;
                    greys[count].Visibility = Visibility.Visible;
                    blacks[count - 1].Visibility = Visibility.Visible;
                    greys[count - 1].Visibility = Visibility.Hidden;

                }
                else
                {
                    blacks[count].Visibility = Visibility.Hidden;
                    greys[count].Visibility = Visibility.Visible;
                    blacks[3].Visibility = Visibility.Visible;
                    greys[3].Visibility = Visibility.Hidden;

                }
            });

            count++;
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



        public static T FindChild<T>(DependencyObject parent, string childName)
   where T : DependencyObject
        {
            // Confirm parent and childName are valid. 
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                T childType = child as T;
                if (childType == null)
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child, childName);

                    // If the child is found, break so we do not overwrite the found child. 
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    // If the child's name is set for search
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        // if the child's name is of the request name
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    // child element found.
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }

    }
}
