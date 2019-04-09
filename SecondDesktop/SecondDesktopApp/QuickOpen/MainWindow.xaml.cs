using System;
using System.Windows;
using System.Windows.Controls;

namespace QuickOpen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : UserControl
    {
        public MainWindow(string Config)
        {
            InitializeComponent();
            Height = 350;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string config = Factory.CreateSubAppConfig();
            OpenDataConfigManager manager = new OpenDataConfigManager(config);
            int count = int.Parse(cbCount.Text);
            manager.CreateData(count);
            Factory.CreateSubApp(AppUID.OpenWindow, config);
        }
    }
}
