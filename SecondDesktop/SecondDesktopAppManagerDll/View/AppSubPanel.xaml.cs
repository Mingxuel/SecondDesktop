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

namespace SecondDesktopAppManagerDll
{
    /// <summary>
    /// Interaction logic for AppSubPanel.xaml
    /// </summary>
    public partial class AppSubPanel : UserControl
    {
        public AppSubPanel()
        {
            InitializeComponent();
            this.DataContext = new VMAppSubPanel();
        }

        private void Button_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Button_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void Button_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
