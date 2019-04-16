using SecondDesktopAppManagerDll;
using SecondDesktopDll;
using SecondDesktopMessagerDll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SecondDesktopDesktopManagerDll
{
    class VMSubAppFrame : NotifyObject
    {
        public delegate void CloseDelegate();
        public event CloseDelegate CloseNotify;

        public MSubAppFrame Model { get; set; }
        private DesktopItem Item;
        public VMSubAppFrame(DesktopItem pItem)
        {
            Model = new MSubAppFrame();
            Item = pItem;

            foreach (var i in AppManager.GetInstance().AppItemList)
            {
                if (i.AppUID == pItem.AppUID)
                {
                    AppImage = i.Icon;
                    AppTitle = i.Name;
                    break;
                }
            }

            DesktopWindowManager.GetInstance().DesktopSettingsCallback += DesktopSettings;
            DesktopSettings(DesktopWindowManager.GetInstance().IsSettings);
        }

        public double Width
        {
            get { return Model.Width; }
            set
            {
                Model.Width = value;
                RaisePropertyChanged("Width");
            }
        }

        public double Height
        {
            get { return Model.Height; }
            set
            {
                Model.Height = value;
                RaisePropertyChanged("Height");
            }
        }

        public string AppImage
        {
            get { return Model.AppImage; }
            set
            {
                Model.AppImage = value;
                RaisePropertyChanged("AppImage");
            }
        }

        public string AppTitle
        {
            get { return Model.AppTitle; }
            set
            {
                Model.AppTitle = value;
                RaisePropertyChanged("AppTitle");
            }
        }

        public Visibility SettingsButtonVisibility
        {
            get { return Model.SettingsButtonVisibility; }
            set
            {
                Model.SettingsButtonVisibility = value;
                RaisePropertyChanged("SettingsButtonVisibility");
            }
        }

        private SDCommand<string> moveUpClickCommand;
        public SDCommand<string> MoveUpClickCommand
        {
            get
            {
                if (moveUpClickCommand == null)
                    moveUpClickCommand = new SDCommand<string>(
                        new Action<string>(e =>
                        {
                            int preIndex = -1;
                            for (int i = 0; i < DesktopDataManager.GetInstance().DesktopItemList.Count; i++)
                            {
                                if (DesktopDataManager.GetInstance().DesktopItemList.ElementAt(i).Page == Item.Page)
                                {
                                    if (DesktopDataManager.GetInstance().DesktopItemList[i].Config == Item.Config)
                                    {
                                        if (preIndex == -1) break;

                                        DesktopItem temp = DesktopDataManager.GetInstance().DesktopItemList[i];
                                        DesktopDataManager.GetInstance().DesktopItemList[i] = DesktopDataManager.GetInstance().DesktopItemList[preIndex];
                                        DesktopDataManager.GetInstance().DesktopItemList[preIndex] = temp;
                                        DesktopDataManager.GetInstance().SaveSubApp();
                                        break;
                                    }
                                    else
                                    {
                                        preIndex = i;
                                    }
                                }
                            }
                        }), null);
                return moveUpClickCommand;
            }
        }

        private SDCommand<string> moveDownClickCommand;
        public SDCommand<string> MoveDownClickCommand
        {
            get
            {
                if (moveDownClickCommand == null)
                    moveDownClickCommand = new SDCommand<string>(
                        new Action<string>(e =>
                        {
                            int nextIndex = -1;
                            for (int i = DesktopDataManager.GetInstance().DesktopItemList.Count - 1; i > -1; i--)
                            {
                                if (DesktopDataManager.GetInstance().DesktopItemList.ElementAt(i).Page == Item.Page)
                                {
                                    if (DesktopDataManager.GetInstance().DesktopItemList[i].Config == Item.Config)
                                    {
                                        if (nextIndex == -1) continue;

                                        DesktopItem temp = DesktopDataManager.GetInstance().DesktopItemList[i];
                                        DesktopDataManager.GetInstance().DesktopItemList[i] = DesktopDataManager.GetInstance().DesktopItemList[nextIndex];
                                        DesktopDataManager.GetInstance().DesktopItemList[nextIndex] = temp;
                                        DesktopDataManager.GetInstance().SaveSubApp();
                                        break;
                                    }
                                    else
                                    {
                                        nextIndex = i;
                                    }
                                }
                            }
                        }), null);
                return moveDownClickCommand;
            }
        }

        private SDCommand<string> moveLeftClickCommand;
        public SDCommand<string> MoveLeftClickCommand
        {
            get
            {
                if (moveLeftClickCommand == null)
                    moveLeftClickCommand = new SDCommand<string>(
                        new Action<string>(e =>
                        {
                            foreach (var item in DesktopDataManager.GetInstance().DesktopItemList)
                            {
                                if (item.Config == Item.Config)
                                {
                                    if (item.Page != 0)
                                    {
                                        item.Page--;
                                        DesktopDataManager.GetInstance().SaveSubApp();
                                        break;
                                    }
                                }
                            }
                        }), null);
                return moveLeftClickCommand;
            }
        }

        private SDCommand<string> moveRightClickCommand;
        public SDCommand<string> MoveRightClickCommand
        {
            get
            {
                if (moveRightClickCommand == null)
                    moveRightClickCommand = new SDCommand<string>(
                        new Action<string>(e =>
                        {
                            foreach (var item in DesktopDataManager.GetInstance().DesktopItemList)
                            {
                                if (item.Config == Item.Config)
                                {
                                    if (item.Page != (DesktopDataManager.GetInstance().PageList.Count - 1))
                                    {
                                        item.Page++;
                                        DesktopDataManager.GetInstance().SaveSubApp();
                                        break;
                                    }
                                }
                            }
                        }), null);
                return moveRightClickCommand;
            }
        }

        private SDCommand<string> appDeleteClickCommand;
        public SDCommand<string> AppDeleteClickCommand
        {
            get
            {
                if (appDeleteClickCommand == null)
                    appDeleteClickCommand = new SDCommand<string>(
                        new Action<string>(e =>
                        {
                            CloseNotify();
                        }), null);
                return appDeleteClickCommand;
            }
        }

        private void DesktopSettings(bool pIsSettings)
        {
            if (pIsSettings)
            {
                SettingsButtonVisibility = Visibility.Visible;
            }
            else
            {
                SettingsButtonVisibility = Visibility.Hidden;
            }
        }
    }
}
