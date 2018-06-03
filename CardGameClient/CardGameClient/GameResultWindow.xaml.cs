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
using System.Windows.Media.Animation;
using CardGameServer;

namespace CardGameClient
{
    /// <summary>
    /// Interaction logic for GameResultWindow.xaml
    /// </summary>
    public partial class GameResultWindow : Window
    {
        private Card newCard = null;
        public GameResultWindow(/*Window own, */string result, bool newLevel, int Exp, int Score, Card newCard = null)
        {
            InitializeComponent();
            //Owner = own;
            Opacity = 0;
            GameResultLabel.Content = result;
            LevelLabel.Visibility = newLevel ? Visibility.Visible : Visibility.Hidden;
            ExpLabel.Content = "Опыт: " + Exp;
            ScoreLabel.Content = "Очки: " + Score;

            if (result == "Победа!")
                PlaceHolder.Source = new BitmapImage(
                new Uri("pack://application:,,,/CardGameClient;component/Images/winner.png"));
            else PlaceHolder.Source = new BitmapImage(
                new Uri("pack://application:,,,/CardGameClient;component/Images/looser.png"));


            if (newCard != null)
                this.newCard = newCard;
        }
        

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (newCard != null)
            {
                NewCardWindow ncw = new NewCardWindow(newCard/*, Owner*/);
                App.WindowList.Add(ncw.Name, ncw);
                ncw.ShowDialog();
            }
            App.WindowList.Remove(this.Name);
            //App.ForceClosing = false;
            Close();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = 0;
            da.To = 1;
            da.Duration = TimeSpan.FromMilliseconds(300);
            //da.FillBehavior = FillBehavior.Stop;
            da.BeginTime = TimeSpan.FromMilliseconds(100);
            BeginAnimation(OpacityProperty, da);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            //App.ForceClosing = false;
            //Owner.Close();
        }
    }
}
