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
        VMSubAppFrame ViewModule = null;
        private DesktopItem DI = null;
        public SubAppFrame(DesktopItem Item)
        {
            InitializeComponent();
            ViewModule = new VMSubAppFrame(Item);
            this.DataContext = ViewModule;
            DI = Item;
            ViewModule.CloseNotify += CloseWindow;
        }

        public void Add(UserControl uc)
        {
            ContentArea.Children.Add(uc);
        }

        private void CloseWindow()
        {
            ((WrapPanel)this.Parent).Children.Remove(this);
            DesktopManager.GetInstance().DeleteSubApp(DI);
        }
    }
}
