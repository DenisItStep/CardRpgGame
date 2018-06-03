using System;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using CardGameServer;

namespace CardGameClient
{
    /// <summary>
    ///     Interaction logic for CardPlace.xaml
    /// </summary>
    public partial class CardPlace : UserControl
    {
        private readonly ThicknessAnimation animation =
            new ThicknessAnimation(new Thickness(-7), new Thickness(-5), TimeSpan.FromMilliseconds(600));

        private bool containsCard;

        private readonly ImageBrush enemyCardBg = new ImageBrush(new BitmapImage(
            new Uri("pack://application:,,,/CardGameClient;component/Images/cardmin_enemy_bg.png")));

        private readonly BitmapImage enemyCardBorder = new BitmapImage(
            new Uri("pack://application:,,,/CardGameClient;component/Images/cardmin_enemy_border.png"));

        private bool isMineCard;

        private bool isSelected;

        private readonly ImageBrush myCardBg = new ImageBrush(new BitmapImage(
            new Uri("pack://application:,,,/CardGameClient;component/Images/cardmin_my_bg.png")));

        private readonly BitmapImage myCardBorder = new BitmapImage(
            new Uri("pack://application:,,,/CardGameClient;component/Images/cardmin_my_border.png"));

        private Card thisCard;

        public CardPlace()
        {
            InitializeComponent();
            GridMain.Background = IsMineCard ? myCardBg : enemyCardBg;
            animation.RepeatBehavior = RepeatBehavior.Forever;
            animation.AutoReverse = true;
            inGame = true;
            InjuredToolTip.Content = "Тяжелая рана.\nПоказатель атаки снижен на 30%.";
        }

        public bool inGame { get; set; }

        public bool Enabled
        {
            get => IsEnabled;
            set => IsEnabled = value;
        }

        public Card ThisCard
        {
            get => thisCard;
            set
            {
                if (value == null)
                {
                    thisCard = value;
                    return;
                }

                if (thisCard != null && thisCard.hp <= 0) return;


                /*if (thisCard != null && value.hp < thisCard.hp && isMineCard && inGame)
                    AnimateDmg((thisCard.hp - value.hp).ToString());*/

                thisCard = value;

                if (value != null)
                {
                    CardImage = App.cardImages[thisCard.id];
                    if (thisCard.hp <= 0) CardDeath();
                    Health = thisCard.hp.ToString();
                    AttakDig.DigitValue = thisCard.dmg.ToString();
                    DefDig.DigitValue = thisCard.def.ToString();
                    InjuryEffect.Visibility = thisCard.IsInjury ? Visibility.Visible : Visibility.Hidden;
                }
            }
        }

        public string Health
        {
            get => HpDig.DigitValue;
            set => HpDig.DigitValue = value;
        }

        public bool selected
        {
            get => isSelected;
            set
            {
                isSelected = value;

                if (!isSelected)
                {
                    sellBtn.Visibility = Visibility.Hidden;
                    borderGrid.Visibility = Visibility.Hidden;
                    animation.BeginTime = null;
                    borderGrid.BeginAnimation(MarginProperty, animation);
                }
            }
        }

        public ImageSource CardImage
        {
            get => Portrait.Source;
            set
            {
                Portrait.Source = value;
                if (value != null)
                {
                    ContainsCard = true;
                    borderImg.Source = isMineCard ? myCardBorder : enemyCardBorder;
                }
            }
        }

        public bool IsMineCard
        {
            get => isMineCard;
            set
            {
                GridMain.Background = (isMineCard = value) ? myCardBg : enemyCardBg;
                borderImg.Source = isMineCard ? myCardBorder : enemyCardBorder;
            }
        }

        public bool ContainsCard
        {
            get => containsCard;
            set
            {
                containsCard = value;
                CardCrid.Visibility = containsCard ? Visibility.Visible : Visibility.Hidden;
            }
        }


        public void setCard(BitmapImage img)
        {
            CardImage = img;
            ContainsCard = true;
        }

        public void CardDeath()
        {
            var da = new DoubleAnimation();
            da.From = 1;
            da.To = 0;
            da.Duration = TimeSpan.FromMilliseconds(500);
            da.BeginTime = TimeSpan.FromMilliseconds(250);
            da.FillBehavior = FillBehavior.Stop;
            da.Completed += delegate
            {
                CardImage = null;
                ContainsCard = false;
            };
            BeginAnimation(OpacityProperty, da);
        }


        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            if (ContainsCard && !selected)
            {
                borderGrid.Visibility = Visibility.Visible;
                animation.BeginTime = TimeSpan.FromMilliseconds(0);
                animation.Duration = TimeSpan.FromMilliseconds(600);
                animation.From = new Thickness(-7);
                animation.To = new Thickness(-5);
                borderGrid.BeginAnimation(MarginProperty, animation);
            }
        }

        private void GridMain_MouseLeave(object sender, MouseEventArgs e)
        {
            if (ContainsCard && !selected)
            {
                borderGrid.Visibility = Visibility.Hidden;
                animation.BeginTime = null;
                borderGrid.BeginAnimation(MarginProperty, animation);
            }
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ContainsCard)
            {
                selected = true;
                animation.Duration = TimeSpan.FromMilliseconds(200);
                animation.From = new Thickness(-6);
                animation.To = new Thickness(-5);
                borderGrid.BeginAnimation(MarginProperty, animation);
            }
        }

        public void AnimateDmg(string dmg)
        {
            dmgLabel.Content = dmg;

            dmgLabel.Opacity = 1;

            var db = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(250));
            db.FillBehavior = FillBehavior.Stop;
            db.AutoReverse = true;
            dmgImgEffect.BeginAnimation(OpacityProperty, db);

            var th = new ThicknessAnimation();
            th.From = dmgLabel.Margin;
            th.FillBehavior = FillBehavior.Stop;
            th.To = new Thickness(dmgLabel.Margin.Left, dmgLabel.Margin.Top - 70, dmgLabel.Margin.Right,
                dmgLabel.Margin.Bottom);
            th.Duration = TimeSpan.FromMilliseconds(500);
            th.Completed += th_Completed;
            dmgLabel.BeginAnimation(MarginProperty, th);
        }

        public void AnimateDmgAdtnl(LastHitInfo lhi)
        {
            if (lhi.IsCritical)
            {
                dmgLabeladtnl.Content = "Крит!";
                dmgLabel.Foreground = Brushes.CornflowerBlue;

                dmgLabeladtnl.Opacity = 1;
            }

            if (lhi.isMissed)
            {
                dmgLabeladtnl.Content = "Промах!";
                dmgLabeladtnl.Foreground = Brushes.DimGray;

                dmgLabeladtnl.Opacity = 1;
            }

            if (!lhi.isMissed)
            {
                dmgLabel.Content = lhi.dmg;
                dmgLabel.Opacity = 1;


                var db = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(350));
                db.FillBehavior = FillBehavior.Stop;
                db.AutoReverse = true;
                dmgImgEffect.BeginAnimation(OpacityProperty, db);

                var th = new ThicknessAnimation();
                th.From = dmgLabel.Margin;
                th.FillBehavior = FillBehavior.Stop;
                th.To = new Thickness(dmgLabel.Margin.Left, dmgLabel.Margin.Top - 70, dmgLabel.Margin.Right,
                    dmgLabel.Margin.Bottom);
                th.Duration = TimeSpan.FromMilliseconds(700);
                th.Completed += th_Completed;
                dmgLabel.BeginAnimation(MarginProperty, th);
            }

            var tha = new ThicknessAnimation();
            tha.From = dmgLabeladtnl.Margin;
            tha.FillBehavior = FillBehavior.Stop;
            tha.To = new Thickness(dmgLabeladtnl.Margin.Left, dmgLabeladtnl.Margin.Top - 70, dmgLabeladtnl.Margin.Right,
                dmgLabeladtnl.Margin.Bottom);
            tha.Duration = TimeSpan.FromMilliseconds(700);
            tha.Completed += tha_Completed;
            dmgLabeladtnl.BeginAnimation(MarginProperty, tha);
        }

        public void AnimateTurn(bool enemy = false)
        {
            var th = new ThicknessAnimation();
            th.From = CardCrid.Margin;
            th.FillBehavior = FillBehavior.Stop;

            if (!enemy)
                th.To = new Thickness(20, 0, -20, 0);
            else
                th.To = new Thickness(-20, 0, 20, 0);


            th.Duration = TimeSpan.FromMilliseconds(250);
            th.AutoReverse = true;
            CardCrid.BeginAnimation(MarginProperty, th);
        }

        private void th_Completed(object sender, EventArgs e)
        {
            dmgLabel.Opacity = 0;
            dmgLabel.Content = "";
            dmgLabel.Margin = new Thickness(0, 6, 6, 0);
            dmgLabel.Foreground = Brushes.Red;
        }


        private void tha_Completed(object sender, EventArgs e)
        {
            dmgLabeladtnl.Opacity = 0;
            dmgLabeladtnl.Content = "";
            dmgLabeladtnl.Margin = new Thickness(0, 35, -40, 0);
            dmgLabeladtnl.Foreground = Brushes.Red;
        }

        private void sellBtn_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            var dw = new DialogWin(App.WindowList["LobbyWnd"], "Вы точно хотите продать эту карту за 350 очков?",
                MessageBoxButton.YesNo);
            App.WindowList.Add(dw.Name, dw);

            if (dw.ShowDialog() == true)
            {
                var res = false;

                new Action(delegate
                    {
                        var isError = false;

                        App.ProxyMutex.WaitOne();
                        try
                        {
                            res = ServiceProxy.Proxy.SellCard(App.UserName, thisCard.slot);
                        }
                        catch (CommunicationException exc)
                        {
                            App.OnException();
                            isError = true;
                        }

                        App.ProxyMutex.ReleaseMutex();

                        if (isError)
                        {
                            App.OnConnectionError();
                        }
                    }
                ).Invoke();

                if (res)
                {
                    selected = false;
                    CardDeath();
                    //CardContextMenu.Visibility = Visibility.Hidden;
                    (App.WindowList["LobbyWnd"] as LobbyScreen).GetAllCard();
                    (App.WindowList["LobbyWnd"] as LobbyScreen).UpdateInfo();
                }
            }

            sellBtn.Visibility = Visibility.Hidden;
        }

        private void GridMain_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!inGame && sellBtn.IsEnabled)
                sellBtn.Visibility = Visibility.Visible;
        }
    }
}