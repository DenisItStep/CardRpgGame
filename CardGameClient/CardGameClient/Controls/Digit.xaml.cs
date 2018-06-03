using System.Windows;
using System.Windows.Controls;

namespace CardGameClient
{
    /// <summary>
    ///     Interaction logic for Digit.xaml
    /// </summary>
    public partial class Digit : UserControl
    {
        private string txt;

        public Digit()
        {
            InitializeComponent();
        }


        public string DigitValue
        {
            get => txt;
            set
            {
                txt = value;

                OneDig.Visibility = TwoDigs1.Visibility = TwoDigs2.Visibility = Visibility.Hidden;

                try
                {
                    if (txt.Length == 2)
                    {
                        TwoDigs1.Source = App.digitImages[int.Parse(txt[0].ToString())];
                        TwoDigs2.Source = App.digitImages[int.Parse(txt[1].ToString())];

                        TwoDigs1.Visibility = TwoDigs2.Visibility = Visibility.Visible;
                    }
                    else if (txt.Length == 1)
                    {
                        OneDig.Source = App.digitImages[int.Parse(txt[0].ToString())];

                        OneDig.Visibility = Visibility.Visible;
                    }
                }
                catch
                {
                }
            }
        }
    }
}