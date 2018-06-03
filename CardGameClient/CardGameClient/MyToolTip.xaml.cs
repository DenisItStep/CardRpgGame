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
    /// Логика взаимодействия для MyToolTip.xaml
    /// </summary>
    public partial class MyToolTip : UserControl
    {
        public MyToolTip()
        {
            InitializeComponent();
        }

        public string Attack
        {
            get
            {
                return AttackLabel.Content.ToString();
            }
            set
            {
                AttackLabel.Content = value;
            }
        }


        public string Defence
        {
            get
            {
                return DefenceLabel.Content.ToString();
            }
            set
            {
                DefenceLabel.Content = value;
            }
        }
    }
}
