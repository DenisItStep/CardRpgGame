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
using System.Threading;

namespace CardGameClient
{
    /// <summary>
    /// Interaction logic for InGameMenuEscWindow.xaml
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
            App.WindowList.Remove(this.Name);
            Close();
        }

        private void LeaveBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {

            bool isError = false;
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

                this.Dispatcher.Invoke(new Action(delegate
                {
                    //App.ForceClosing = false;
                    Owner.Hide();
                    App.WindowList.Remove(this.Name);
                    Close();
                }));

            }).BeginInvoke(new AsyncCallback(delegate(IAsyncResult ar) { }), null);
        }

        private void ExitBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            App.ForceClosing = true;
            App.WindowList["MainWnd"].Close();
            Close();
        }
    }
}
