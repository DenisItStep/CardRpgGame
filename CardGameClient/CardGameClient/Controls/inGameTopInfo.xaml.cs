using System.Windows.Controls;
using System.Windows.Input;

namespace CardGameClient
{
    /// <summary>
    ///     Interaction logic for inGameTopInfo.xaml
    /// </summary>
    public partial class inGameTopInfo : UserControl
    {
        public inGameTopInfo()
        {
            InitializeComponent();
        }

        public string firstUserNickname
        {
            get => firstUserNick.Content.ToString();
            set => firstUserNick.Content = value;
        }

        public string firstUserLevel
        {
            get => user1Level.DigitValue;
            set => user1Level.DigitValue = value;
        }


        public string twoUserNickname
        {
            get => twoUserNick.Content.ToString();
            set => twoUserNick.Content = value;
        }

        public string twoUserLevel
        {
            get => user2Level.DigitValue;
            set => user2Level.DigitValue = value;
        }

        public string btnText
        {
            get => turnLabel.Text;
            set => turnLabel.Text = value;
        }

        private void turnLabel_MouseEnter(object sender, MouseEventArgs e)
        {
            turnBtn.UserControl_MouseEnter(turnBtn, null);
        }
    }
}