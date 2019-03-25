using System;
using System.Windows;
using System.Windows.Controls;

namespace AppDemo
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
            SubAppConfigManager manager = new SubAppConfigManager(config);
            manager.SetText(Message.Text);
            Factory.CreateSubApp(AppUID.AppWindow, config);
        }
    }
}
