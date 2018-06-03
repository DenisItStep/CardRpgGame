using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using CardGameServer;

namespace CardGameClient
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Dictionary<int, CardPlace> enemyCardPlases = new Dictionary<int, CardPlace>();

        private readonly Dictionary<int, CardPlace> myCardPlases = new Dictionary<int, CardPlace>();
        private CardPlace enemySelectedCardPlace;

        public Game game;

        private int lasthitCard = -1;
        private CardPlace mySelectedCardPlace;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void myCardPlace_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var cp = sender as CardPlace;

            if (mySelectedCardPlace != null && cp != mySelectedCardPlace)
                mySelectedCardPlace.selected = false;

            mySelectedCardPlace = cp;

            foreach (var item in enemyCardPlases.Values)
                if (item.ContainsCard && item.ThisCard.Enabled)
                    item.IsEnabled = true;
        }

        private void enemyCardPlace_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var cp = sender as CardPlace;

                if (enemySelectedCardPlace != null && enemySelectedCardPlace != cp)
                    enemySelectedCardPlace.selected = false;

                enemySelectedCardPlace = cp;

                foreach (var item in enemyCardPlases.Values) item.IsEnabled = false;

                foreach (var item in myCardPlases.Values) item.IsEnabled = false;

                //attack
                var isError = false;
                LastHitInfo lhi = null;
                App.ProxyMutex.WaitOne();
                try
                {
                    lhi = ServiceProxy.Proxy.DoAttack(App.NickName, mySelectedCardPlace.ThisCard.slot,
                        enemySelectedCardPlace.ThisCard.slot);
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

                //if success
                if (lhi != null)
                {
                    enemySelectedCardPlace.AnimateDmgAdtnl(lhi);
                    mySelectedCardPlace.AnimateTurn();
                }

                enemySelectedCardPlace.selected = mySelectedCardPlace.selected = false;

                foreach (var item in myCardPlases.Values)
                    if (item.ContainsCard && item.ThisCard.Enabled)
                        item.IsEnabled = true;
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


        public void DoGame()
        {
            try
            {
                while (true)
                {
                    Thread.Sleep(500);

                    var isError = false;

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
                        if (game.gameState == 2 || game.gameState == 3)
                        {
                            foreach (var item in game.firstGamerCards)
                                Dispatcher.Invoke(new Action(delegate
                                {
                                    if (game.fGamer.nick == App.NickName)
                                    {
                                        if (myCardPlases[item.slot].ThisCard != null && item.IsAttacked
                                                                                     && !myCardPlases[item.slot]
                                                                                         .ThisCard.IsAttacked)
                                        {
                                            enemyCardPlases[game.lastHitInfo.slot].AnimateTurn(true);
                                            lasthitCard = game.lastHitInfo.slot;
                                            myCardPlases[item.slot].AnimateDmgAdtnl(game.lastHitInfo);
                                        }

                                        myCardPlases[item.slot].ThisCard = item;
                                        myCardPlases[item.slot].Enabled = item.Enabled;
                                    }
                                    else
                                    {
                                        enemyCardPlases[item.slot].ThisCard = item;
                                    }
                                }));

                            foreach (var item in game.twoGamerCards)
                                Dispatcher.Invoke(new Action(delegate
                                {
                                    if (game.fGamer.nick != App.NickName)
                                    {
                                        if (myCardPlases[item.slot].ThisCard != null && item.IsAttacked
                                                                                     && !myCardPlases[item.slot]
                                                                                         .ThisCard.IsAttacked)
                                        {
                                            enemyCardPlases[game.lastHitInfo.slot].AnimateTurn(true);
                                            lasthitCard = game.lastHitInfo.slot;
                                            myCardPlases[item.slot].AnimateDmgAdtnl(game.lastHitInfo);
                                        }

                                        myCardPlases[item.slot].ThisCard = item;
                                        myCardPlases[item.slot].Enabled = item.Enabled;
                                    }
                                    else
                                    {
                                        enemyCardPlases[item.slot].ThisCard = item;
                                    }
                                }));

                            Dispatcher.Invoke(new Action(delegate
                            {
                                if (game.currUsr == App.NickName)
                                {
                                    menuTop.btnText = "Ваш\nХод";

                                    boardGrid.IsEnabled = true;

                                    foreach (var item in myCardPlases.Values)
                                        if (item.ContainsCard && item.ThisCard.Enabled)
                                            item.Enabled = true;
                                }
                                else
                                {
                                    menuTop.btnText = "Ход\nсоперника";

                                    /* if (!known)
                                     {
                                         lasthitCard = -1;
                                         known = true;
                                     }*/

                                    boardGrid.IsEnabled = false;

                                    foreach (var item in myCardPlases.Values) item.Enabled = false;
                                }
                            }));
                        }
                        else if (game.gameState == 4)
                        {
                            foreach (var item in game.firstGamerCards)
                                Dispatcher.Invoke(new Action(delegate
                                {
                                    if (game.fGamer.nick == App.NickName)
                                    {
                                        if (myCardPlases[item.slot].ThisCard != null && item.IsAttacked
                                                                                     && !myCardPlases[item.slot]
                                                                                         .ThisCard.IsAttacked)
                                        {
                                            enemyCardPlases[game.lastHitInfo.slot].AnimateTurn(true);
                                            lasthitCard = game.lastHitInfo.slot;
                                            myCardPlases[item.slot].AnimateDmgAdtnl(game.lastHitInfo);
                                        }

                                        myCardPlases[item.slot].ThisCard = item;
                                        myCardPlases[item.slot].Enabled = item.Enabled;
                                    }
                                    else
                                    {
                                        enemyCardPlases[item.slot].ThisCard = item;
                                    }
                                }));

                            foreach (var item in game.twoGamerCards)
                                Dispatcher.Invoke(new Action(delegate
                                {
                                    if (game.fGamer.nick != App.NickName)
                                    {
                                        if (myCardPlases[item.slot].ThisCard != null && item.IsAttacked
                                                                                     && !myCardPlases[item.slot]
                                                                                         .ThisCard.IsAttacked)
                                        {
                                            enemyCardPlases[game.lastHitInfo.slot].AnimateTurn(true);
                                            lasthitCard = game.lastHitInfo.slot;
                                            myCardPlases[item.slot].AnimateDmgAdtnl(game.lastHitInfo);
                                        }

                                        myCardPlases[item.slot].ThisCard = item;
                                        myCardPlases[item.slot].Enabled = item.Enabled;
                                    }
                                    else
                                    {
                                        enemyCardPlases[item.slot].ThisCard = item;
                                    }
                                }));

                            Dispatcher.Invoke(new Action(delegate
                            {
                                App.InGame = false;

                                GameResultWindow grw = null;

                                if (game.currUsr == App.NickName)
                                    grw = new GameResultWindow("Победа!", game.WinGamerReward.NewLevel,
                                        game.WinGamerReward.Exp,
                                        game.WinGamerReward.Score, game.WinGamerReward.NewCard);
                                else
                                    grw = new GameResultWindow("Поражение!", game.LooseGamerReward.NewLevel,
                                        game.LooseGamerReward.Exp,
                                        game.LooseGamerReward.Score, game.LooseGamerReward.NewCard);

                                App.WindowList.Add(grw.Name, grw);

                                grw.ShowDialog();

                                (App.WindowList["LobbyWnd"] as LobbyScreen).OnGameEnd();

                                //Hide();
                                //Close();
                            }));

                            Thread.Sleep(200);

                            Dispatcher.Invoke(new Action(delegate { App.WindowList["LobbyWnd"].Show(); }));

                            Thread.Sleep(1000);

                            Dispatcher.Invoke(new Action(delegate { Hide(); }));

                            return;
                        }
                        else if (game.gameState == 5)
                        {
                            Dispatcher.Invoke(new Action(delegate
                            {
                                App.InGame = false;
                                var grw = new GameResultWindow("Победа!", game.WinGamerReward.NewLevel,
                                    game.WinGamerReward.Exp,
                                    game.WinGamerReward.Score, game.WinGamerReward.NewCard);

                                App.WindowList.Add(grw.Name, grw);

                                grw.ShowDialog();

                                (App.WindowList["LobbyWnd"] as LobbyScreen).OnGameEnd();

                                //Hide();
                                //Close();
                            }));

                            Thread.Sleep(200);

                            Dispatcher.Invoke(new Action(delegate { App.WindowList["LobbyWnd"].Show(); }));

                            Thread.Sleep(1000);

                            Dispatcher.Invoke(new Action(delegate { Hide(); }));

                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
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

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (UIElement item in boardGrid.Children)
                {
                    var cp = item as CardPlace;

                    if (cp != null)
                    {
                        if (cp.Name.Contains("MyPlace"))
                            myCardPlases.Add(int.Parse(cp.Tag.ToString()), cp);
                        else if (cp.Name.Contains("EnemyPlace"))
                            enemyCardPlases.Add(int.Parse(cp.Tag.ToString()), cp);
                    }
                }

                OnWindowShow();


                /* this.Dispatcher.Invoke(new Action(() =>
                         Owner.Hide()
                     ), DispatcherPriority.ContextIdle, null);*/
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
                    game = null;
                    App.OnConnectionError();
                }
            }).Invoke();

            if (game != null)
            {
                if (game.fGamer == null || game.tGamer == null || game.Gamers == null || game.Gamers.Count < 2)
                {
                    Thread.Sleep(1000);
                    new Action(delegate
                    {
                        var isError = false;

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
                            game = null;
                            App.OnConnectionError();
                        }
                    }).Invoke();
                }

                foreach (var item in myCardPlases.Values) item.inGame = true;

                foreach (var item in enemyCardPlases.Values) item.inGame = false;


                if (App.NickName == game.fGamer.nick)
                {
                    menuTop.firstUserNickname = App.NickName;
                    menuTop.twoUserNickname = game.tGamer.nick;
                    menuTop.firstUserLevel = game.fGamer.level.ToString();
                    menuTop.twoUserLevel = game.tGamer.level.ToString();
                }
                else
                {
                    menuTop.firstUserNickname = App.NickName;
                    menuTop.twoUserNickname = game.fGamer.nick;
                    menuTop.firstUserLevel = game.tGamer.level.ToString();
                    menuTop.twoUserLevel = game.fGamer.level.ToString();
                }

                var gameThread = new Thread(DoGame) {IsBackground = true};
                gameThread.Start();
            }
            else
            {
                throw new Exception("game is null");
            }
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            var isError = false;
            if (App.isConnected && ServiceProxy.Proxy != null)
            {
                App.ProxyMutex.WaitOne();
                try
                {
                    ServiceProxy.Proxy.leaveGame(App.NickName);
                }
                catch
                {
                    App.OnException();
                    isError = true;
                }

                App.ProxyMutex.ReleaseMutex();
            }

            if (App.ForceClosing)
            {
                if (App.isConnected && ServiceProxy.Proxy != null)
                {
                    App.ProxyMutex.WaitOne();
                    try
                    {
                        ServiceProxy.Proxy.Logout(App.UserName);
                    }
                    catch
                    {
                    }

                    App.ProxyMutex.ReleaseMutex();
                }

                App.isConnected = false;
                Application.Current.Shutdown();
            }
            else if (isError)
            {
                App.OnConnectionError();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            App.ForceClosing = true;
        }

        private void CardPlace_MouseEnter(object sender, MouseEventArgs e)
        {
            /* CardPlace ccp = sender as CardPlace;
 
             if (!ccp.ContainsCard) return;
 
             if (ccp.IsMineCard)
             {
                 ccp.ToolTip = "Характеристики:\nАтака: " + ccp.ThisCard.dmg + "\nЗащита: " + ccp.ThisCard.def;
             }*/
        }


        private void EnemyCardPlace_MouseEnter(object sender, MouseEventArgs e)
        {
            /*CardPlace ccp = sender as CardPlace;

            if (!ccp.ContainsCard) return;

            if (!ccp.IsMineCard)
            {
                ccp.ToolTip = "Характеристики:\nАтака: " + ccp.ThisCard.dmg + "\nЗащита: " + ccp.ThisCard.def;
            }*/
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                var igmew = new InGameMenuEscWindow(this);
                App.WindowList.Add(igmew.Name, igmew);
                igmew.Show();
            }
        }

        private void MainWnd_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            /*if (Visibility == Visibility.Visible)
            {
                if (myCardPlases.Count() != 0)
                {
                    mySelectedCardPlace = null;
                    enemySelectedCardPlace = null;
                    game = null;

                    myCardPlases = new Dictionary<int, CardPlace>();
                    enemyCardPlases = new Dictionary<int, CardPlace>();
                   // Window_Loaded_1(this, null);
                }
            }*/
            if (Visibility == Visibility.Hidden)
            {
                menuTop.btnText = "";
                boardGrid.IsEnabled = false;
                menuTop.firstUserNickname = "";
                menuTop.twoUserNickname = "";
                mySelectedCardPlace = null;
                enemySelectedCardPlace = null;
                game = null;

                foreach (var item in myCardPlases.Values)
                {
                    item.inGame = false;
                    item.ContainsCard = false;
                    item.ThisCard = null;
                }

                foreach (var item in enemyCardPlases.Values)
                {
                    item.inGame = false;
                    item.ContainsCard = false;
                    item.ThisCard = null;
                }
            }
        }
    }
}