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

		VMAppPanelFrame ViewModel = null;
		public AppPanelFrame()
        {
            InitializeComponent();
			ViewModel = new VMAppPanelFrame();
			this.DataContext = ViewModel;
			ViewModel.CloseNotify += CloseWindow;
		}

        public void Add(UserControl pUserControl, string pAppUID)
        {
            ContentArea.Children.Add(pUserControl);
            foreach(var item in AppManager.GetInstance().AppItemList)
            {
                if(item.AppUID == pAppUID)
                {
					ViewModel.Title = item.Name;
					ViewModel.Icon = item.Icon;

					break;
                }
            }
        }

        public void CloseWindow()
        {
			((WrapPanel)this.Parent).Children.Remove(this);
			CloseNotify();
		}
    }
}
