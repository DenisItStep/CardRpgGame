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
using CardGameServer;
using System.Windows.Media.Animation;

namespace CardGameClient
{
    /// <summary>
    /// Interaction logic for NewCardWindow.xaml
    /// </summary>
    public partial class NewCardWindow : Window
    {
        public NewCardWindow(Card card/*, Window own*/)
        {
            InitializeComponent();
            //Owner = own;
            Opacity = 0;
            NewCardPlace.ThisCard = card;
            CardNameLabel.Content = card.card_name;
        }        

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            App.WindowList.Remove(this.Name);
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
    }
}
