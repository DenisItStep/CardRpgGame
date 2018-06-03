using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using CardGameServer;

namespace CardGameClient
{
    /// <summary>
    ///     Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        //login and password temp
        private string login, passw;
        //LoadingScreen ldscreen;

        public LoginScreen()
        {
            InitializeComponent();

            //ldscreen = new LoadingScreen();
            //ldscreen.Show();
        }


        private void Login()
        {
            try
            {
                var cf =
                    new ChannelFactory<Servicegame>("MyEndpoint");

                ServiceProxy.Proxy = cf.CreateChannel();

                //0 success
                //1 incorrect pass
                //2 already online 
                //3 hacking attempt

                var res = ServiceProxy.Proxy.Login(login, passw);

                Thread.Sleep(2000); //emulate conntion procc

                Dispatcher.Invoke(new Action(delegate { loginBtn.IsEnabled = true; }));

                if (res == 0)
                {
                    App.UserName = login;
                    App.isConnected = true;

                    App.iamonlineTread = new Thread(App.iAmOnline) {IsBackground = true};
                    App.iamonlineTread.Start();

                    var isError = false;

                    var contains = false;

                    App.ProxyMutex.WaitOne();
                    try
                    {
                        contains = ServiceProxy.Proxy.isAccountContainsAnyCharacter(login);
                    }
                    catch
                    {
                        App.OnException();
                        isError = true;
                    }

                    App.ProxyMutex.ReleaseMutex();

                    if (isError) return;


                    if (!contains)
                        Dispatcher.Invoke(new Action(delegate
                        {
                            if (!App.WindowList.ContainsKey("CharacterCreateWnd"))
                            {
                                var ccs = new CharacterCreateScreen();
                                App.WindowList.Add(ccs.Name, ccs);
                            }
                            else
                            {
                                (App.WindowList["CharacterCreateWnd"] as CharacterCreateScreen).OnWindowShow();
                            }

                            App.WindowList["CharacterCreateWnd"].Show();
                            //loginTextBox.Text = "";
                            passwordTextBox.Password = "";
                        }));
                    else
                        Dispatcher.Invoke(new Action(delegate
                        {
                            if (!App.WindowList.ContainsKey("LobbyWnd"))
                            {
                                var ls = new LobbyScreen();
                                App.WindowList.Add(ls.Name, ls);
                            }
                            else
                            {
                                (App.WindowList["LobbyWnd"] as LobbyScreen).OnWindowShow();
                            }

                            App.WindowList["LobbyWnd"].Show();
                            //loginTextBox.Text = "";
                            passwordTextBox.Password = "";
                        }));

                    Thread.Sleep(1000);

                    Dispatcher.Invoke(new Action(delegate
                    {
                        errorPopupInfo.HideWaitInfo();
                        Hide();
                    }));
                }

                else if (res == 1)
                {
                    Dispatcher.Invoke(new Action(delegate
                    {
                        errorPopupInfo.HideWaitInfoAndShowError("Неправильный логин или пароль!");
                    }));
                }
                else if (res == 2)
                {
                    Dispatcher.Invoke(new Action(delegate
                    {
                        errorPopupInfo.HideWaitInfoAndShowError("Кто-то другой использует ваш аккаунт!");
                    }));
                }
                else if (res == 3)
                {
                    Dispatcher.Invoke(new Action(delegate
                    {
                        errorPopupInfo.HideWaitInfoAndShowError("Поля заполнены некорректено!");
                    }));
                }


                if (!App.isConnected)
                    Dispatcher.Invoke(new Action(delegate
                    {
                        var da = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(100));
                        da.BeginTime = TimeSpan.FromMilliseconds(2000);
                        da.FillBehavior = FillBehavior.Stop;
                        da.Completed += da_Completed;
                        LoginErrorInfo.BeginAnimation(OpacityProperty, da);
                    }));
            }
            catch (CommunicationException)
            {
                Dispatcher.Invoke(new Action(delegate
                {
                    App.isConnected = false;
                    loginBtn.IsEnabled = true;
                    errorPopupInfo.HideWaitInfoAndShowError(
                        "Ошибка подключения!\r\nПопробуйте повторить попытку позже..."
                    );
                }));
            }
            catch (Exception exc)
            {
                Dispatcher.Invoke(new Action(delegate
                {
                    App.isConnected = false;
                    loginBtn.IsEnabled = true;
                    App.dumpException(exc);
                    MessageBox.Show(exc.Message, "Критическая ошибка!");
                }));
            }
        }

        private void da_Completed(object sender, EventArgs e)
        {
            LoginErrorInfo.Content = "";
            LoginErrorInfo.Opacity = 1;
        }

        private void loginBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                login = loginTextBox.Text;
                passw = passwordTextBox.Password;

                if (login == "" || passw == ""
                                || sqlInjection.Words.Any(word =>
                                    login.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0
                                    || passw.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0))
                {
                    errorPopupInfo.ShowError("Поля заполнены некорректено!");
                    return;
                }

                loginBtn.IsEnabled = false;
                errorPopupInfo.ShowWaitInfo("Попытка авторизации. Ожидайте...");

                var loginThread = new Thread(Login);
                loginThread.Start();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Критическая ошибка!");
            }
        }

        private void exitBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // ldscreen.Owner = this;
            try
            {
                App.WindowList.Add(Name, this);
                foreach (var item in Directory.GetFiles("Images/Cards/", "*.png", SearchOption.AllDirectories))
                {
                    var img = new BitmapImage();
                    img.BeginInit();
                    img.UriSource = new Uri(item, UriKind.Relative);
                    img.CacheOption = BitmapCacheOption.OnLoad;
                    img.EndInit();


                    App.cardImages.Add(int.Parse(Path.GetFileNameWithoutExtension(item)), img);
                }


                foreach (var item in Directory.GetFiles("Images/Numeric/", "*.png", SearchOption.AllDirectories))
                {
                    var img = new BitmapImage();
                    img.BeginInit();
                    img.UriSource = new Uri(item, UriKind.Relative);
                    img.CacheOption = BitmapCacheOption.OnLoad;
                    img.EndInit();


                    App.digitImages.Add(int.Parse(Path.GetFileNameWithoutExtension(item)), img);
                }

                App.rarityDictionary.Add(0, new Rarity("Герой", Brushes.PaleGoldenrod));
                App.rarityDictionary.Add(1, new Rarity("Обычное существо", Brushes.MintCream));
                App.rarityDictionary.Add(2, new Rarity("Необычное существо", Brushes.LightBlue));
                App.rarityDictionary.Add(3, new Rarity("Редкое существо", Brushes.DodgerBlue));
                App.rarityDictionary.Add(4, new Rarity("Мистическое существо", Brushes.DarkMagenta));
                App.rarityDictionary.Add(5, new Rarity("Легендарное существо", Brushes.DarkOrange));

                App.loginScreen = this;
            }
            catch (Exception exc)
            {
                Dispatcher.Invoke(new Action(delegate
                {
                    MessageBox.Show(exc.Message, "Критическая ошибка!");
                    App.dumpException(exc);
                    Application.Current.Shutdown();
                }));
            }
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
        }

        private void LoginWnd_ContentRendered(object sender, EventArgs e)
        {
        }
    }
}