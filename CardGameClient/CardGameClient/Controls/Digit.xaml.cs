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
    /// Interaction logic for Digit.xaml
    /// </summary>
    public partial class Digit : UserControl
    {
        string txt;

        public Digit()
        {
            InitializeComponent();
        }


        public string DigitValue
        {
            get
            {
                return txt;
            }
            set
            {
                txt = value;

                OneDig.Visibility = TwoDigs1.Visibility = TwoDigs2.Visibility = Visibility.Hidden;

                try
                {

                    if (txt.Length == 2)
                    {
                        TwoDigs1.Source = App.digitImages[Int32.Parse(txt[0].ToString())];
                        TwoDigs2.Source = App.digitImages[Int32.Parse(txt[1].ToString())];

                        TwoDigs1.Visibility = TwoDigs2.Visibility = Visibility.Visible;
                    }
                    else if (txt.Length == 1)
                    {
                        OneDig.Source = App.digitImages[Int32.Parse(txt[0].ToString())];

                        OneDig.Visibility = Visibility.Visible;
                    }
                }
                catch { }

            }
        }
    }
}
