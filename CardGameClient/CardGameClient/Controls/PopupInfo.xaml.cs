using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace CardGameClient
{
    /// <summary>
    ///     Логика взаимодействия для PopupInfo.xaml
    /// </summary>
    public partial class PopupInfo : UserControl
    {
        public bool CompleateAnim = true;

        public PopupInfo()
        {
            InitializeComponent();
        }

        public void ShowError(string errorText)
        {
            Opacity = 0;
            Visibility = Visibility.Visible;
            if (CompleateAnim)
            {
                CompleateAnim = false;
                TextInfo.Text = errorText;

                var anim_appear = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(200));
                //anim_appear.FillBehavior = FillBehavior.Stop;
                //anim_appear.Completed += anim_appear_Completed;
                BeginAnimation(OpacityProperty, anim_appear);
                OkBtn.Visibility = Visibility.Visible;
            }
        }

        public void ShowWaitInfo(string waitText)
        {
            Visibility = Visibility.Visible;
            Opacity = 1;
            TextInfo.Text = waitText;
            waitAnim.Visibility = Visibility.Visible;
        }

        public void HideWaitInfo()
        {
            Visibility = Visibility.Hidden;
            Opacity = 0;
            TextInfo.Text = "";
            waitAnim.Visibility = Visibility.Hidden;
        }

        public void HideWaitInfoAndShowError(string errorText)
        {
            waitAnim.Visibility = Visibility.Hidden;
            OkBtn.Visibility = Visibility.Visible;
            Opacity = 1;
            // if (CompleateAnim)
            //{
            //  CompleateAnim = false;
            TextInfo.Text = errorText;

            /*DoubleAnimation anim_appear = new DoubleAnimation(0.99, 1, TimeSpan.FromMilliseconds(1));
            anim_appear.BeginTime = TimeSpan.FromMilliseconds(200);
            anim_appear.FillBehavior = FillBehavior.Stop;
            anim_appear.Completed += anim_appear_Completed;
            BeginAnimation(OpacityProperty, anim_appear);               */
            //}
        }


        private void anim_disappear_Completed(object sender, EventArgs e)
        {
            Visibility = Visibility.Hidden;
            Opacity = 0;
            CompleateAnim = true;
        }

        private void anim_appear_Completed(object sender, EventArgs e)
        {
            //Opacity = 1;
            OkBtn.Visibility = Visibility.Hidden;

            var anim_disappear = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(200));
            //anim_disappear.BeginTime = TimeSpan.FromMilliseconds(2000);
            anim_disappear.FillBehavior = FillBehavior.Stop;
            anim_disappear.Completed += anim_disappear_Completed;
            BeginAnimation(OpacityProperty, anim_disappear);
        }

        private void Label_MouseEnter_1(object sender, MouseEventArgs e)
        {
            var lb = sender as Label;
            lb.Foreground = Brushes.WhiteSmoke;
        }

        private void Label_MouseLeave_1(object sender, MouseEventArgs e)
        {
            var lb = sender as Label;
            lb.Foreground = Brushes.LightGray;
        }

        private void Label_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            var lb = sender as Label;
            lb.Margin = new Thickness(0, 6, 5, 0);
        }

        private void Label_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            var lb = sender as Label;
            lb.Margin = new Thickness(0, 5, 5, 0);
        }

        private void OkBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            anim_appear_Completed(this, null);
        }
    }
}