using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CardGameClient
{
    /// <summary>
    ///     Логика взаимодействия для sellmenu.xaml
    /// </summary>
    public partial class sellmenu : UserControl
    {
        public sellmenu()
        {
            InitializeComponent();
        }

        private void UserControl_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            sellLabel.Margin = new Thickness(0, 1, 0, 0);
        }

        private void UserControl_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            sellLabel.Margin = new Thickness(0);
        }

        private void UserControl_MouseEnter_1(object sender, MouseEventArgs e)
        {
            Background = Brushes.DarkSlateGray;
        }

        private void UserControl_MouseLeave_1(object sender, MouseEventArgs e)
        {
            Background = Brushes.Black;
        }
    }
}