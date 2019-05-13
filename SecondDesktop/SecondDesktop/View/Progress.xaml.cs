using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SecondDesktop
{
    /// <summary>
    /// Interaction logic for MessageBoxYesNo.xaml
    /// </summary>
    public partial class Progress : UserControl
    {
        bool direct = true;
        public Progress()
        {
            InitializeComponent();
        }

        public void SetValue(int Value)
        {

        }
    }
}
