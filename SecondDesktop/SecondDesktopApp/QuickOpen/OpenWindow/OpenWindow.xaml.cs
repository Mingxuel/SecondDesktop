using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using SecondDesktopHotkeyManagerDll;

namespace QuickOpen
{
    /// <summary>
    /// Interaction logic for SubApp.xaml
    /// </summary>
    public partial class OpenWindow : UserControl
    {
        private VMOpenWindow ViewModel = null;
        public OpenWindow(string Config)
        {
            InitializeComponent();
            OpenDataConfigManager manager = new OpenDataConfigManager(Config);
            Height = 20 * manager.Count();

            ViewModel = new VMOpenWindow(Config);
            DataContext = ViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
			HotkeyManager.GetInstance().RegisterHotKey("Test", LKey.Ctrl, RKey.D0, HotKey);
        }

        private int HotKey()
        {
            MessageBox.Show("Yes");

            return 0;
        }
    }
}
