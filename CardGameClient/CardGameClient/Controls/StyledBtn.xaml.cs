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
        ImageBrush disabled = new ImageBrush(new BitmapImage(
            new Uri("pack://application:,,,/CardGameClient;component/Images/btn_ui_disabled.png")));

        Thickness normalM = new Thickness(0);
        Thickness downM = new Thickness(0, 1, 0, 0);

        public StyledBtn()
        {
            InitializeComponent();
        }


        public ImageBrush BackgroundNormal
        {
            get
            {
                return normal;
            }
            set
            {
                normal = value;
            }
        }

        public bool Enabled
        {
            get
            {
                return IsEnabled;
            }
            set
            {
                IsEnabled = value;
                Background = IsEnabled ? normal : disabled;
            }
        }

        public ImageBrush BackgroundHover
        {
            get
            {
                return hover;
            }
            set
            {
                hover = value;
            }
        }

        public Double TextFontSize
        {
            get
            {
                return textLabel.FontSize;
            }
            set
            {
                textLabel.FontSize = value;
            }
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

        public void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            Background = hover;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            Background = normal;
            textLabel.Margin = normalM;
        }

        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            textLabel.Margin = normalM;
        }
    }
}
