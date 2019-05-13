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

namespace SecondDesktopAppStore
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : UserControl
	{
        private VMMainWindow ViewModel = null;
        public MainWindow(string pConfig)
        {
            InitializeComponent();
            ViewModel = new VMMainWindow();
            this.DataContext = ViewModel;
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            ViewModel.Loaded();
        }
    }
}