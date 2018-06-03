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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CardGameClient
{
    /// <summary>
    /// Interaction logic for StyledBtn.xaml
    /// </summary>
    public partial class StyledBtn : UserControl
    {
        ImageBrush normal = new ImageBrush(new BitmapImage(
           new Uri("pack://application:,,,/CardGameClient;component/Images/btn_ui.png")));
        ImageBrush hover = new ImageBrush(new BitmapImage(
            new Uri("pack://application:,,,/CardGameClient;component/Images/btn_ui_hover.png")));

        Thickness normalM = new Thickness(37, 1, 0, 0);
        Thickness downM = new Thickness(37, 2, 0, 0);

        public StyledBtn()
        {
            InitializeComponent();
        }

        public string Text
        {
            get
            {
                return textLabel.Content.ToString();
            }
            set
            {
                textLabel.Content = value;
            }
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Background = hover;
            textLabel.Margin = downM;
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            Background = hover;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            Background = normal;
        }

        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            textLabel.Margin = normalM;
        }
    }
}
