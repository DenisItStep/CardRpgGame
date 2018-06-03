using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ServiceModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using CardGameServer;

namespace CardGameClient
{
    /// <summary>
    ///     Interaction logic for LobbyScreen.xaml
    /// </summary>
    public partial class LobbyScreen : Window
    {
        private int curr_page = 1;
        private Thread findGameThread;

        private CardPlace selectedCardPlace;

        public LobbyScreen()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            var isError = false;
            if (App.isConnected && ServiceProxy.Proxy != null)
            {
                App.isConnected = false;
                App.ProxyMutex.WaitOne();
                try
                {
                    if (findBtn.InProgress) ServiceProxy.Proxy.cancelSearch(App.NickName);
                    ServiceProxy.Proxy.Logout(App.UserName);
                }
                catch
                {
                    App.OnException();
                    isError = true;
                }

                App.ProxyMutex.ReleaseMutex();
            }

            if (App.ForceClosing)
                Application.Current.Shutdown();
            else if (isError) App.OnConnectionError();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            App.ForceClosing = true;
        }

        private void exitBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //App.ForceClosing = false;

            App.WindowList["LoginWnd"].Show();
            Hide();
            //Close();
        }

        public void UpdateInfo()
        {
            var isError = false;

            App.ProxyMutex.WaitOne();
            try
            {
                App.charInfo = ServiceProxy.Proxy.EnterWorld(App.UserName);
            }
            catch
            {
                App.OnException();
                isError = true;
            }

            App.ProxyMutex.ReleaseMutex();

            if (isError)
            {
                App.OnConnectionError();
                return;
            }

            NickNameLevel.Text = App.charInfo.nickname + ", " + App.charInfo.heroname + " " +
                                 App.charInfo.character_level
                                 + "-го уровня";
            Exp.Text = "Опыт: " + App.charInfo.exp;
            Games.Text = "Кол-во Игр: " + App.charInfo.games;
            Wins.Text = "Кол-во Побед: " + App.charInfo.wins;
            CardsScore.Text = Rating.Text = "Очки: " + App.charInfo.score;


            var updateRankingThread = new Thread(UpdateRanking) {IsBackground = true};
            updateRankingThread.Start();
        }

        private void UpdateRanking()
        {
            var mrg = new Thickness(5, 5, 0, 5);


            var isError = false;
            List<CharInfo> RankingList = null;

            App.ProxyMutex.WaitOne();
            try
            {
                RankingList = ServiceProxy.Proxy.getRanking();
            }
            catch
            {
                App.OnException();
                isError = true;
            }

            App.ProxyMutex.ReleaseMutex();

            if (isError)
            {
                App.OnConnectionError();
                return;
            }

            Dispatcher.Invoke(new Action(delegate
            {
                ratingGrid.Children.RemoveRange(6, ratingGrid.Children.Count - 6);

                for (var i = 0; i < RankingList.Count; i++)
                {
                    var currCharInf = RankingList[i];

                    var tx = new Label
                    {
                        Content = (i + 1).ToString(),
                        Foreground = Brushes.Wheat,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    ratingGrid.Children.Add(tx);
                    Grid.SetRow(tx, i + 1);
                    Grid.SetColumn(tx, 0);

                    tx = new Label
                    {
                        Content = currCharInf.nickname,
                        Foreground = Brushes.Wheat,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    ratingGrid.Children.Add(tx);
                    Grid.SetRow(tx, i + 1);
                    Grid.SetColumn(tx, 1);


                    tx = new Label
                    {
                        Content = currCharInf.heroname + " "
                                                       + currCharInf.character_level + "-го уровня",
                        Foreground = Brushes.Wheat,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    ratingGrid.Children.Add(tx);
                    Grid.SetRow(tx, i + 1);
                    Grid.SetColumn(tx, 2);


                    tx = new Label
                    {
                        Content = currCharInf.games.ToString(),
                        Foreground = Brushes.Wheat,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    ratingGrid.Children.Add(tx);
                    Grid.SetRow(tx, i + 1);
                    Grid.SetColumn(tx, 3);


                    tx = new Label
                    {
                        Content = currCharInf.wins.ToString(),
                        Foreground = Brushes.Wheat,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    ratingGrid.Children.Add(tx);
                    Grid.SetRow(tx, i + 1);
                    Grid.SetColumn(tx, 4);
                }
            }));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                OnWindowShow();
            }
            catch (Exception exc)
            {
                Dispatcher.Invoke(new Action(delegate
                {
                    MessageBox.Show(exc.Message, "Критическая ошибка!");
                    App.isConnected = false;
                    App.dumpException(exc);
                    Application.Current.Shutdown();
                }));
            }
        }


        public void OnWindowShow()
        {
            new Action(delegate
            {
                var isError = false;

                App.ProxyMutex.WaitOne();
                try
                {
                    App.charInfo = ServiceProxy.Proxy.EnterWorld(App.UserName);
                }
                catch
                {
                    App.OnException();
                    isError = true;
                }

                App.ProxyMutex.ReleaseMutex();

                if (isError) App.OnConnectionError();
            }).Invoke();

            if (App.charInfo != null)
            {
                App.NickName = App.charInfo.nickname;

                NickNameLevel.Text = App.charInfo.nickname + ", " + App.charInfo.heroname + " " +
                                     App.charInfo.character_level + "-го уровня";
                Exp.Text = "Опыт: " + App.charInfo.exp;
                Games.Text = "Кол-во Игр: " + App.charInfo.games;
                Wins.Text = "Кол-во Побед: " + App.charInfo.wins;
                Rating.Text = "Очки: " + App.charInfo.score;

                var updateRankingThread = new Thread(UpdateRanking) {IsBackground = true};
                updateRankingThread.Start();
            }
        }

        public void OnGameEnd()
        {
            findBtn.Enabled = true;
            MainLobbyBackBtn.ToolTip = null;
            AllCardsBtn.ToolTip = null;
            MainLobbyBackBtn.Enabled = true;
            AllCardsBtn.Enabled = true;
            App.InGame = false;
            //Show();
            UpdateInfo();
        }

        private void FindGame()
        {
            try
            {
                var isError = false;
                Game game = null;

                App.ProxyMutex.WaitOne();
                try
                {
                    game = ServiceProxy.Proxy.findGame(App.NickName);
                }
                catch
                {
                    App.OnException();
                    isError = true;
                }

                App.ProxyMutex.ReleaseMutex();

                if (isError)
                {
                    App.OnConnectionError();
                    return;
                }


                if (game == null)
                {
                    Dispatcher.Invoke(new Action(delegate
                    {
                        MessageBox.Show("Некорректные данные или была обнаружена попытка взлома.\n"
                                        + "Действие было записано...");
                        findBtn.InProgress = false;
                    }));

                    return;
                }

                App.InGame = true;

                Dispatcher.Invoke(new Action(delegate
                {
                    findBtn.Enabled = true;
                    findBtn.InProgress = true;
                }));

                if (game.gameState == 2)
                {
                    Dispatcher.Invoke(new Action(delegate
                    {
                        findBtn.Enabled = false;
                        findBtn.textLabel.Content = "Противник найден...";
                    }));

                    Thread.Sleep(2000); //emulate find proccess

                    Dispatcher.Invoke(new Action(delegate
                    {
                        findBtn.InProgress = false;

                        if (!App.WindowList.ContainsKey("MainWnd"))
                        {
                            var mw = new MainWindow();
                            App.WindowList.Add(mw.Name, mw);
                        }
                        else
                        {
                            (App.WindowList["MainWnd"] as MainWindow).OnWindowShow();
                        }

                        findBtn.Enabled = true;
                        App.WindowList["MainWnd"].Show();
                    }));

                    Thread.Sleep(1000);

                    Dispatcher.Invoke(new Action(delegate { Hide(); }));
                }
                else if (game.gameState == 1)
                {
                    while (true)
                    {
                        isError = false;

                        App.ProxyMutex.WaitOne();
                        try
                        {
                            game = ServiceProxy.Proxy.getGame(App.NickName);
                        }
                        catch
                        {
                            App.OnException();
                            isError = true;
                        }

                        App.ProxyMutex.ReleaseMutex();

                        if (isError)
                        {
                            App.OnConnectionError();
                            return;
                        }

                        if (game != null)
                        {
                            if (game.gameState == 2)
                            {
                                Dispatcher.Invoke(new Action(delegate
                                {
                                    findBtn.Enabled = false;
                                    findBtn.textLabel.Content = "Противник найден...";
                                }));

                                Thread.Sleep(2000); //emulate find proccess

                                Dispatcher.Invoke(new Action(delegate
                                {
                                    findBtn.InProgress = false;

                                    if (!App.WindowList.ContainsKey("MainWnd"))
                                    {
                                        var mw = new MainWindow();
                                        App.WindowList.Add(mw.Name, mw);
                                    }
                                    else
                                    {
                                        (App.WindowList["MainWnd"] as MainWindow).OnWindowShow();
                                    }

                                    findBtn.Enabled = true;

                                    App.WindowList["MainWnd"].Show();
                                }));

                                Thread.Sleep(1000);

                                Dispatcher.Invoke(new Action(delegate { Hide(); }));
                                break;
                            }

                            if (game.gameState == 7) return;
                        }
                        else
                        {
                            return;
                        }

                        Thread.Sleep(500);
                    }
                }
            }
            catch (CommunicationException exc)
            {
                Dispatcher.Invoke(new Action(delegate
                {
                    MessageBox.Show(exc.Message, "Критическая ошибка!");
                    App.isConnected = false;
                    App.dumpException(exc);
                    Application.Current.Shutdown();
                }));
            }
        }

        private void findBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (findBtn.InProgress)
            {
                //findGameThread.Abort();
                findBtn.Enabled = false;

                var isError = false;

                App.ProxyMutex.WaitOne();
                try
                {
                    ServiceProxy.Proxy.cancelSearch(App.NickName);
                }
                catch
                {
                    App.OnException();
                    isError = true;
                }

                App.ProxyMutex.ReleaseMutex();

                if (isError)
                {
                    App.OnConnectionError();
                    return;
                }

                findBtn.InProgress = false;
                findBtn.Enabled = true;
                MainLobbyBackBtn.Enabled = true;
                AllCardsBtn.Enabled = true;
                MainLobbyBackBtn.ToolTip = null;
                AllCardsBtn.ToolTip = null;
                App.InGame = false;
                return;
            }

            findBtn.Enabled = false;
            MainLobbyBackBtn.Enabled = false;
            AllCardsBtn.Enabled = false;
            MainLobbyBackBtn.ToolTip = "Идёт поиск игры. Для того, чтобы выйти требуется его отменить...";
            AllCardsBtn.ToolTip =
                "Идёт поиск игры. Для того, чтобы выполнить данное действуе требуется его отменить...";
            findBtn.textLabel.Content = "Поиск противника...";

            findGameThread = new Thread(FindGame) {IsBackground = true};
            findGameThread.Start();
        }

        private void Window_ContentRendered_1(object sender, EventArgs e)
        {
        }

        private void AllCardPlace_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var cp = sender as CardPlace;

            foreach (CardPlace item in SlotGrid.Children)
            {
                if (item.ContainsCard) continue;

                var oslot = cp.ThisCard.slot;
                var nslot = int.Parse(item.Tag.ToString());

                var isError = false;

                var res = false;

                App.ProxyMutex.WaitOne();
                try
                {
                    res = ServiceProxy.Proxy.ChangeCardslot(App.UserName, oslot, nslot);
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
                    return;
                }


                if (res)
                {
                    cp.selected = false;
                    var fillMyCardGridThread = new Thread(GetAllCard) {IsBackground = true};
                    fillMyCardGridThread.Start();
                }

                return;
            }

            var dw = new DialogWin(this,
                "Все боевые слоты уже заняты\nЧтобы переместить туда эту карту необходимо освободить один...",
                MessageBoxButton.OK);
            App.WindowList.Add(dw.Name, dw);
            dw.ShowDialog();
        }

        private void SlotCardPlace_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var cp = sender as CardPlace;

            var oslot = cp.ThisCard.slot;
            var nslot = -1;

            var isError = false;

            var res = false;

            App.ProxyMutex.WaitOne();
            try
            {
                nslot = ServiceProxy.Proxy.GetFreeSlotNumberAllCards(App.UserName);
                res = ServiceProxy.Proxy.ChangeCardslot(App.UserName, oslot, nslot);
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
                return;
            }


            if (res)
            {
                cp.selected = false;
                var fillMyCardGridThread = new Thread(GetAllCard) {IsBackground = true};
                fillMyCardGridThread.Start();
            }
        }

        public void GetAllCard()
        {
            var isError = false;
            List<Card> allCards = null;

            App.ProxyMutex.WaitOne();
            try
            {
                allCards = ServiceProxy.Proxy.GetAllCard(App.UserName, curr_page);
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
                return;
            }

            if (allCards != null)
                Dispatcher.Invoke(new Action(delegate
                {
                    //clear 
                    foreach (CardPlace item in AllCardsGrid.Children)
                    {
                        item.ContainsCard = false;
                        item.ToolTip = null;

                        item.sellBtn.IsEnabled = false;

                        //item.CardContextMenu.Visibility = Visibility.Hidden;
                    }

                    foreach (CardPlace item in SlotGrid.Children)
                    {
                        item.ContainsCard = false;
                        item.ToolTip = null;
                    }

                    //fill

                    foreach (var item in allCards)
                    {
                        CardPlace cp;
                        if (item.slot >= 10)
                        {
                            var index = curr_page == 1 ? item.slot - 10 : item.slot - (curr_page - 1) * 24 - 10;
                            cp = AllCardsGrid.Children[index] as CardPlace;
                            cp.ThisCard = item;

                            var ci = new CardInfo();
                            ci.CardName.Content = item.card_name;
                            ci.Rarity.Content = App.rarityDictionary[item.cardRarity].RarityName;
                            ci.Rarity.Foreground = App.rarityDictionary[item.cardRarity].RarityColor;
                            ci.Dmg.Content = "Атака: " + item.dmg;
                            ci.Def.Content = "Защита: " + item.def;
                            ci.Hp.Content = "Здоровье: " + item.hp;
                            ci.Level.Content = "Уровень: " + item.min_level;

                            cp.ToolTip = new ToolTip
                            {
                                Background = new SolidColorBrush(Color.FromArgb(230, 0, 0, 0)),
                                BorderThickness = new Thickness(0),
                                Content = ci
                            };

                            cp.sellBtn.IsEnabled = true;

                            //cp.CardContextMenu.Visibility = Visibility.Visible;
                            //cp.CardContextMenuSellBtn.Click += new RoutedEventHandler(CardContextMenuSellBtn_Click);
                        }
                        else
                        {
                            cp = SlotGrid.Children[item.slot - 1] as CardPlace;
                            cp.ThisCard = item;

                            var ci = new CardInfo();
                            ci.CardName.Content = item.card_name;
                            ci.Rarity.Content = App.rarityDictionary[item.cardRarity].RarityName;
                            ci.Rarity.Foreground = App.rarityDictionary[item.cardRarity].RarityColor;
                            ci.Dmg.Content = "Атака: " + item.dmg;
                            ci.Def.Content = "Защита: " + item.def;
                            ci.Hp.Content = "Здоровье: " + item.hp;
                            ci.Level.Content = "Уровень: " + item.min_level;

                            cp.ToolTip = new ToolTip
                            {
                                Background = new SolidColorBrush(Color.FromArgb(230, 0, 0, 0)),
                                BorderThickness = new Thickness(0),
                                Content = ci
                            };
                        }
                    }
                }));
        }

        //Context Menu Sell Click
        /*void CardContextMenuSellBtn_Click(object sender, RoutedEventArgs e)
        {
            bool isError = false;
            bool res = false;

            App.ProxyMutex.WaitOne();
            try
            {
                //res = ServiceProxy.Proxy.SellCard(App.UserName, sender as Card
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
                return;
            }
        }*/

        private void MyCardsGrid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (MyCardsGrid.Visibility == Visibility.Visible)
            {
                var fillMyCardGridThread = new Thread(GetAllCard) {IsBackground = true};
                fillMyCardGridThread.Start();
            }
        }

        private void AllCardsBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //if (findBtn.InProgress) return;

            MyCardsGrid.Visibility = Visibility.Visible;
            MainLobbyGrid.Visibility = Visibility.Hidden;

            CardsScore.Text = "Очки: " + App.charInfo.score;
        }

        private void CardsExitBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MyCardsGrid.Visibility = Visibility.Hidden;
            MainLobbyGrid.Visibility = Visibility.Visible;
        }

        private void MainLobbyBackBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //if (findBtn.InProgress) return;

            //App.isConnected = false;

            App.ForceClosing = false;
            Window_Closing(this, null);
            Window_Closed(this, null);

            App.WindowList["LoginWnd"].Show();

            new Action(delegate
            {
                Thread.Sleep(1000);

                Dispatcher.Invoke(new Action(delegate { Hide(); }));
            }).BeginInvoke(delegate { }, null);
        }

        private void CardPlace_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            var cp = sender as CardPlace;
            if (selectedCardPlace != null && selectedCardPlace != cp) selectedCardPlace.selected = false;
            selectedCardPlace = cp;
        }

        //paginator --
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (curr_page > 1) curr_page--;
            currPageLabel.Content = curr_page;

            if (selectedCardPlace != null) selectedCardPlace.selected = false;

            if (curr_page >= 10) currPageLabel.Margin = new Thickness(5, 6.5, 50, 0);
            else currPageLabel.Margin = new Thickness(0, 6.5, 55, 0);

            new Action(delegate { GetAllCard(); }).BeginInvoke(delegate { }, null);
        }

        //paginator++
        private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            curr_page++;
            currPageLabel.Content = curr_page;

            if (selectedCardPlace != null) selectedCardPlace.selected = false;

            if (curr_page >= 10) currPageLabel.Margin = new Thickness(0, 6.5, 50, 0);
            else currPageLabel.Margin = new Thickness(0, 6.5, 55, 0);

            new Action(delegate { GetAllCard(); }).BeginInvoke(delegate { }, null);
        }

        private void CardsShopBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //MyCardsGrid.Visibility = Visibility.Hidden;
            ShopGrid.Visibility = Visibility.Visible;
        }

        /// <summary>
        ///     Покупка карты
        /// </summary>
        /// <param name="number"></param>
        public void BuyCards(int number)
        {
            var price = string.Empty;

            var da = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(250));
            da.FillBehavior = FillBehavior.Stop;
            da.Completed += delegate { ShopGrid.Visibility = Visibility.Hidden; };
            ShopGrid.BeginAnimation(OpacityProperty, da);


            if (number == 1) price = "2000";
            else if (number == 2) price = "4000";
            else if (number == 3) price = "8000";

            var dw = new DialogWin(this,
                "Внимание!\nПокупка карты будет стоить " + price + " очков\nЖелаете продолжить?",
                MessageBoxButton.YesNo);

            App.WindowList.Add(dw.Name, dw);
            if (dw.ShowDialog() == true)
                new Action(delegate
                {
                    List<Card> card = null;
                    var isError = false;

                    App.ProxyMutex.WaitOne();
                    try
                    {
                        card = ServiceProxy.Proxy.BuyCard(App.UserName, number);
                        App.charInfo = ServiceProxy.Proxy.EnterWorld(App.UserName);
                        GetAllCard();
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
                        return;
                    }

                    if (card.Count == 0)
                        Dispatcher.Invoke(new Action(delegate
                        {
                            var dw2 = new DialogWin(this,
                                "Недостаточно средств.\nДля покупки необходимо не менее " + price + " очков",
                                MessageBoxButton.OK);
                            App.WindowList.Add(dw2.Name, dw2);
                            dw2.ShowDialog();
                        }));
                    else
                        Dispatcher.Invoke(new Action(delegate
                        {
                            Rating.Text = "Очки: " + App.charInfo.score;
                            CardsScore.Text = "Очки: " + App.charInfo.score;

                            var cpw = new CardPackWindow(card);
                            App.WindowList.Add(cpw.Name, cpw);
                            ShopGrid.Visibility = Visibility.Hidden;

                            cpw.ShowDialog();
                        }));
                }).BeginInvoke(delegate { }, null);
        }

        private void LobbyWnd_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Hidden)
            {
                findBtn.Enabled = true;
                findBtn.InProgress = false;
                MainLobbyBackBtn.ToolTip = null;
                AllCardsBtn.ToolTip = null;
                MainLobbyBackBtn.Enabled = true;
                AllCardsBtn.Enabled = true;
                ShopGrid.Visibility = Visibility.Hidden;
                MyCardsGrid.Visibility = Visibility.Hidden;
                MainLobbyGrid.Visibility = Visibility.Visible;
                //Opacity = 0;
            }

            /*else if (Visibility == Visibility.Visible)
            {
                DoubleAnimation da = new DoubleAnimation();
                da.From = 0;
                da.To = 1;
                da.Duration = TimeSpan.FromMilliseconds(250);
                da.FillBehavior = FillBehavior.Stop;
                da.BeginTime = TimeSpan.FromMilliseconds(100);
                da.Completed += delegate(object s, EventArgs se)
                {
                    Opacity = 1;
                };
                BeginAnimation(OpacityProperty, da);
            }*/
        }

        private void ShopExitBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MyCardsGrid.Visibility = Visibility.Visible;
            ShopGrid.Visibility = Visibility.Hidden;
        }

        private void nabor1Btn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            BuyCards(1);
        }

        private void nabor2Btn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            BuyCards(2);
        }

        private void nabor3Btn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            BuyCards(3);
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ShopGrid.Visibility = Visibility.Hidden;
        }
    }
}