using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AppDemo
{
    /// <summary>
    /// Interaction logic for SubApp.xaml
    /// </summary>
    public partial class AppWindow : UserControl
    {
        public AppWindow(string Config)
        {
            InitializeComponent();
            SubAppConfigManager manager = new SubAppConfigManager(Config);
            test.Content = manager.GetText();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Height = 500;
        }
    }
}
