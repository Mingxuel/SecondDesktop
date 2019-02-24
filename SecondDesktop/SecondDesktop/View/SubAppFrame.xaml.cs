using SecondDesktopDesktopManagerDll;
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
    /// Interaction logic for SubAppFrame.xaml
    /// </summary>
    public partial class SubAppFrame : UserControl
    {
        public delegate void CloseDelegate();
        public event CloseDelegate CloseNotify;

        private DesktopItem DI = null;
        public SubAppFrame(DesktopItem Item)
        {
            InitializeComponent();
            this.DataContext = new VMSubAppFrame();
            DI = Item;
            CloseNotify += CloseWindow;
        }

        public void Add(UserControl uc)
        {
            ContentArea.Children.Add(uc);
        }

        private void BtClose_Click(object sender, RoutedEventArgs e)
        {
            ((WrapPanel)this.Parent).Children.Remove(this);
            CloseNotify();
        }

        private void CloseWindow()
        {
            DesktopManager.GetInstance().DeleteSubApp(DI);
        }
    }
}
