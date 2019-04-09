using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using SecondDesktopHotkeyManagerDll;

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
            test.Text = manager.GetText();
            double height = 0;
            if(double.TryParse(manager.GetText(), out height))
            {
                Height = height;
            }
        }

        [DllImport("kernel32.dll")]
        public static extern uint GetLastError();

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
