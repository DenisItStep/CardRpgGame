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
    /// Interaction logic for inGameTopInfo.xaml
    /// </summary>
    public partial class inGameTopInfo : UserControl
    {
          public string firstUserNickname
         {
             get
             {
                 return firstUserNick.Content.ToString();
             }
             set
             {
                 firstUserNick.Content = value;
             }
         }

        public string firstUserLevel
         {
             get
             {
                 return user1Level.DigitValue;
             }
             set
             {
                 user1Level.DigitValue = value;
             }
         }

        
        public string twoUserNickname
        {
            get
            {
                return twoUserNick.Content.ToString();
            }
            set
            {
                twoUserNick.Content = value;
            }
        }

        public string twoUserLevel
        {
            get
            {
                return user2Level.DigitValue;
            }
            set
            {
                user2Level.DigitValue = value;
            }
        }

        public string btnText
        {
            get
            {
                return turnLabel.Text;
            }
            set
            {
                turnLabel.Text = value;
            }
        }

        public inGameTopInfo()
        {
            InitializeComponent();
        }

        private void turnLabel_MouseEnter(object sender, MouseEventArgs e)
        {
            turnBtn.UserControl_MouseEnter(turnBtn, null);
        }
    }
}
