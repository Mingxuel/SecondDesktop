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
using SecondDesktopMessagerDll;

namespace AppDemo
{
    /// <summary>
    /// Interaction logic for UCDemo.xaml
    /// </summary>
    public partial class UCDemo : UserControl
    {
        public UCDemo(string Config)
        {
            InitializeComponent();
            this.DataContext = new VMUCDemo();
            Height = 350;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string config = Factory.CreateSubAppConfig();
            Factory.CreateSubApp(AppUID.SubApp, config);
        }
    }
}
