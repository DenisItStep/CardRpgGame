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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;
//using System.Threading;

namespace CardGameClient
{
    /// <summary>
    /// Interaction logic for FindButton.xaml
    /// </summary>
    public partial class FindButton : UserControl
    {
        ImageBrush normal = new ImageBrush(new BitmapImage(
            new Uri("pack://application:,,,/CardGameClient;component/Images/findBtn.png")));

        ImageBrush disabled = new ImageBrush(new BitmapImage(
            new Uri("pack://application:,,,/CardGameClient;component/Images/findBtn_disabled.png")));

        private bool inProgress = false;

        public bool Enabled
        {
            get
            {
                return IsEnabled;
            }
            set
            {
                IsEnabled = value;
                mainBtn.BackgroundNormal = IsEnabled ? normal : disabled;
                mainBtn.Background = mainBtn.BackgroundNormal;
            }
        }

        public bool InProgress
        {
            get 
            { 
                return inProgress; 
            }
            set 
            {
                inProgress = value; 

                if (inProgress)
                {
                    infoGrid.Visibility = Visibility.Visible;
                    mainBtn.Text = "Отмена";
                }
                else
                {
                    infoGrid.Visibility = Visibility.Hidden;
                    mainBtn.Text = "Начать игру";
                }
            }
        }

        //Timer t1 = new Timer(1000);

        public FindButton()
        {
            InitializeComponent();
            //t1.Elapsed += new ElapsedEventHandler(t1_Elapsed);
        }

        /*void t1_Elapsed(object sender, ElapsedEventArgs e)
        {
            //textLabel.Content
        }*/

        private void findBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
           
        }
    }
}
