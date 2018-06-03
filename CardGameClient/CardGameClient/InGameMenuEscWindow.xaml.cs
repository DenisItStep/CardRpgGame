using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using CardGameServer;

namespace CardGameClient
{
    /// <summary>
    ///     Interaction logic for InGameMenuEscWindow.xaml
    /// </summary>
    public partial class InGameMenuEscWindow : Window
    {
        public InGameMenuEscWindow(Window own)
        {
            InitializeComponent();
            Owner = own;
        }

        private void CancelBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            App.WindowList.Remove(Name);
            Close();
        }

        private void LeaveBtn_MouseUp(object sender, MouseButtonEventArgs e)
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

            if (isError)
            {
                App.OnConnectionError();
                return;
            }

            App.InGame = false;


            App.WindowList["LobbyWnd"].Show();


            new Action(delegate
            {
                Thread.Sleep(1000);

                Dispatcher.Invoke(new Action(delegate
                {
                    //App.ForceClosing = false;
                    Owner.Hide();
                    App.WindowList.Remove(Name);
                    Close();
                }));
            }).BeginInvoke(delegate { }, null);
        }

        private void ExitBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            App.ForceClosing = true;
            App.WindowList["MainWnd"].Close();
            Close();
        }
    }
}