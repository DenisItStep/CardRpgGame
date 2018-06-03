using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CardGameClient
{
    /// <summary>
    ///     Interaction logic for StyledBtn.xaml
    /// </summary>
    public partial class StyledBtn : UserControl
    {
        private readonly ImageBrush disabled = new ImageBrush(new BitmapImage(
            new Uri("pack://application:,,,/CardGameClient;component/Images/btn_ui_disabled.png")));

        private readonly Thickness downM = new Thickness(0, 1, 0, 0);

        private readonly Thickness normalM = new Thickness(0);

        public StyledBtn()
        {
            InitializeComponent();
        }


        public ImageBrush BackgroundNormal { get; set; } = new ImageBrush(new BitmapImage(
            new Uri("pack://application:,,,/CardGameClient;component/Images/btn_ui.png")));

        public bool Enabled
        {
            get => IsEnabled;
            set
            {
                IsEnabled = value;
                Background = IsEnabled ? BackgroundNormal : disabled;
            }
        }

        public ImageBrush BackgroundHover { get; set; } = new ImageBrush(new BitmapImage(
            new Uri("pack://application:,,,/CardGameClient;component/Images/btn_ui_hover.png")));

        public double TextFontSize
        {
            get => textLabel.FontSize;
            set => textLabel.FontSize = value;
        }


        public string Text
        {
            get => textLabel.Content.ToString();
            set => textLabel.Content = value;
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Background = BackgroundHover;
            textLabel.Margin = downM;
        }

        public void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            Background = BackgroundHover;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            Background = BackgroundNormal;
            textLabel.Margin = normalM;
        }

        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            textLabel.Margin = normalM;
        }
    }
}