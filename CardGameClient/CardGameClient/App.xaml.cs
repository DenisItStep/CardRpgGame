using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using CardGameServer;
using System.Threading;
using System.IO;

namespace CardGameClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static bool ForceClosing = true;
        public static string UserName { get; set; }
        public static string NickName { get; set; }
        public static LoginScreen loginScreen { get; set; }

        public static Mutex ProxyMutex = new Mutex();

        public static bool InGame = false;

        public static Thread iamonlineTread;

        public static bool isConnected = false;

        public static Dictionary<string, Window> WindowList = new Dictionary<string, Window>();

        public static Dictionary<int, BitmapImage> cardImages = new Dictionary<int, BitmapImage>();
        public static Dictionary<int, BitmapImage> digitImages = new Dictionary<int, BitmapImage>();

        public static CharInfo charInfo;

        public static Dictionary<int, Rarity> rarityDictionary = new Dictionary<int, Rarity>();


        public static void iAmOnline()
        {
            while (true)
            {
                if (!App.InGame && App.isConnected)
                {
                    bool isError = false;

                    App.ProxyMutex.WaitOne();
                    try
                    {
                        ServiceProxy.Proxy.iAmOnline(App.UserName);
                    }
                    catch (Exception exc)
                    {
                        isError = true;
                        App.OnException();
                    }
                    App.ProxyMutex.ReleaseMutex();

                    if (isError)
                    {
                        App.OnConnectionError();
                        return;
                    }
                }
                Thread.Sleep(1000);
            }
        }

        public static void OnException()
        {
            App.loginScreen.Dispatcher.Invoke(new Action(delegate
            {
                App.isConnected = false;
                App.loginScreen.loginBtn.IsEnabled = true;
                App.loginScreen.errorPopupInfo.ShowError("Связь с сервером неожиданно прервана...");
                App.loginScreen.Show();
            }));
        }

        public static void OnConnectionError()
        {
            Thread.Sleep(2000);
            App.loginScreen.Dispatcher.Invoke(new Action(delegate
            {
                App.isConnected = false;
                for (int i = App.WindowList.Count - 1; i > 0; i--)
                {
                    Window window = App.WindowList.Values.ElementAt(i);

                    if (window.Name == "LoginWnd") continue;
                    else if (window.Name == "DialogWnd" || window.Name == "NewCardWnd"
                        || window.Name == "GameResultWnd" || window.Name == "InGameMenuEscWnd" || window.Name == "CardPackWnd")
                        window.Close();
                    else
                        window.Hide();
                }


                /*foreach (Window window in App.WindowList.Values)
                {
                    try
                    {
                        //App.ForceClosing = false;
                        if (window.Name == "LoginWnd") continue;
                        else if (window.Name == "DialogWnd" || window.Name == "NewCardWnd"
                            || window.Name == "GameResultWnd" || window.Name == "InGameMenuEscWnd" || window.Name == "CardPackWnd")
                            window.Close();
                        else
                            window.Hide();
                    }
                    catch { }
                }*/
            }));
        }


        public static void dumpException(Exception exc)
        {
            try
            {
                if (!Directory.Exists("Log")) Directory.CreateDirectory("Log");

                string dir_name = "Log/" + DateTime.Now.ToString("dd_MMM");
                if (!Directory.Exists(dir_name))
                    Directory.CreateDirectory(dir_name);

                File.WriteAllText(dir_name + "/error_" + DateTime.Now.ToString("dd_MMM_HH_mm_ss") + ".log", exc.Message + "\r\n" + exc.StackTrace);
            }
            catch { }
        }
    }
}
