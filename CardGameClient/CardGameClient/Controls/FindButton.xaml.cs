using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

//using System.Threading;

namespace CardGameClient
{
    /// <summary>
    ///     Interaction logic for FindButton.xaml
    /// </summary>
    public partial class FindButton : UserControl
    {
        private readonly ImageBrush disabled = new ImageBrush(new BitmapImage(
            new Uri("pack://application:,,,/CardGameClient;component/Images/findBtn_disabled.png")));

        private bool inProgress;

        private readonly ImageBrush normal = new ImageBrush(new BitmapImage(
            new Uri("pack://application:,,,/CardGameClient;component/Images/findBtn.png")));

        //Timer t1 = new Timer(1000);

        public FindButton()
        {
            InitializeComponent();
            //t1.Elapsed += new ElapsedEventHandler(t1_Elapsed);
        }

        public bool Enabled
        {
            get => IsEnabled;
            set
            {
                IsEnabled = value;
                mainBtn.BackgroundNormal = IsEnabled ? normal : disabled;
                mainBtn.Background = mainBtn.BackgroundNormal;
            }
        }

        public bool InProgress
        {
            get => inProgress;
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

        /*void t1_Elapsed(object sender, ElapsedEventArgs e)
        {
            //textLabel.Content
        }*/

        private void findBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
        }
    }
}