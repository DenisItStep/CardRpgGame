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

namespace CardGameClient
{
    /// <summary>
    /// Interaction logic for CardPlace.xaml
    /// </summary>
    public partial class CardPlace : UserControl
    {

        private bool containsCard = false;
        private bool isMineCard;
        
        ImageBrush myCardBg = new ImageBrush(new BitmapImage(
            new Uri("pack://application:,,,/CardGameClient;component/Images/cardmin_my_bg.png" )));
        ImageBrush enemyCardBg = new ImageBrush(new BitmapImage(
            new Uri("pack://application:,,,/CardGameClient;component/Images/cardmin_enemy_bg.png")));

        BitmapImage myCardBorder = new BitmapImage(
            new Uri("pack://application:,,,/CardGameClient;component/Images/cardmin_my_border.png"));
        BitmapImage enemyCardBorder = new BitmapImage(
            new Uri("pack://application:,,,/CardGameClient;component/Images/cardmin_enemy_border.png"));

        public ImageSource Card
        {
            get
            {
                return Portrait.Source;
            }
            set
            {
                Portrait.Source = value;
                ContainsCard = true;
                borderImg.Source = isMineCard ? myCardBorder : enemyCardBorder;
            }
        }

        public bool IsMineCard
        {
            get
            {
                return isMineCard;
            }
            set
            {
                GridMain.Background = (isMineCard = value) ? myCardBg : enemyCardBg;
            }
        }

        public bool ContainsCard
        {
            get
            {
                return containsCard;
            }
            set
            {
                containsCard = value;
                CardCrid.Visibility = containsCard ? Visibility.Visible : Visibility.Hidden;
            }
        }

        public CardPlace()
        {
            InitializeComponent();
            GridMain.Background = IsMineCard ? myCardBg : enemyCardBg;
        }


        public void setCard(BitmapImage img)
        {
            Card = img;
            ContainsCard = true;
        }

        public void removeCard()
        {
            Card = null;
            ContainsCard = false;
        }


        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            //BorderThickness = new Thickness(2);
            if (ContainsCard)
                Margin = new Thickness(-2.5, 0, -2.5, 0);
        }

        private void GridMain_MouseLeave(object sender, MouseEventArgs e)
        {
           // BorderThickness = new Thickness(0);
            if (ContainsCard)
                Margin = new Thickness(0, 2.5, 0, 2.5);
        }
    }
}
