using SecondDesktopAppManagerDll;
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
    /// Interaction logic for PopupFrame.xaml
    /// </summary>
    public partial class AppPanelFrame : UserControl
    {
        public delegate void CloseDelegate();
        public event CloseDelegate CloseNotify;

        public AppPanelFrame()
        {
            InitializeComponent();
            this.DataContext = new VMAppPanelFrame();
            CloseNotify += CloseWindow;
        }

        public void Add(UserControl pUserControl, string pAppUID)
        {
            ContentArea.Children.Add(pUserControl);
            foreach(var item in AppManager.GetInstance().AppItemList)
            {
                if(item.AppUID == pAppUID)
                {
                    ((VMAppPanelFrame)this.DataContext).Title = item.Name;
                    break;
                }
            }
        }

        private void BtClose_Click(object sender, RoutedEventArgs e)
        {
            ((WrapPanel)this.Parent).Children.Remove(this);
            CloseNotify();
        }

        public void CloseWindow()
        {

        }
    }
}
