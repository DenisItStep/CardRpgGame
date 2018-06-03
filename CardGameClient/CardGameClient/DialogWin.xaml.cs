using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace CardGameClient
{
    /// <summary>
    ///     Interaction logic for DialogWin.xaml
    /// </summary>
    public partial class DialogWin : Window
    {
        public DialogWin(Window own, string txt, MessageBoxButton btns)
        {
            InitializeComponent();
            Opacity = 0;
            Owner = own;
            TextInfo.Text = txt;

            if (btns == MessageBoxButton.YesNo)
                AcceptBtn.Visibility = CancelBtn.Visibility = Visibility.Visible;
            else if (btns == MessageBoxButton.OK)
                AcceptBtnOnly.Visibility = Visibility.Visible;
        }

        //accept
        private void AcceptBtnOnly_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (AcceptBtnOnly.Visibility != Visibility.Visible)
                DialogResult = true;

            Close();
        }

        //cancel
        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (AcceptBtnOnly.Visibility != Visibility.Visible)
                DialogResult = false;

            Close();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            App.WindowList.Remove(Name);
        }

        private void DialogWnd_ContentRendered(object sender, EventArgs e)
        {
            var da = new DoubleAnimation();
            da.From = 0;
            da.To = 1;
            da.Duration = TimeSpan.FromMilliseconds(300);
            //da.FillBehavior = FillBehavior.Stop;
            da.BeginTime = TimeSpan.FromMilliseconds(100);
            BeginAnimation(OpacityProperty, da);
        }
    }
}