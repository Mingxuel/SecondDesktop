using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SecondDesktopDll
{
	public class SDResource
	{
		public static readonly string AddImage = ConfigManager.GetInstance().ApplicationDirectory + "Resource/App/Add.png";
		public static readonly string AppsImage = ConfigManager.GetInstance().ApplicationDirectory + "Resource/App/Apps.png";
		public static readonly string DeleteImage = ConfigManager.GetInstance().ApplicationDirectory + "Resource/App/Delete.png";
		public static readonly string DesktopImage = ConfigManager.GetInstance().ApplicationDirectory + "Resource/App/Desktop.png";
		public static readonly string UpImage = ConfigManager.GetInstance().ApplicationDirectory + "Resource/App/Up.png";
		public static readonly string DownImage = ConfigManager.GetInstance().ApplicationDirectory + "Resource/App/Down.png";
		public static readonly string LogoImage = ConfigManager.GetInstance().ApplicationDirectory + "Resource/App/Logo.png";
		public static readonly string MoveBottomImage = ConfigManager.GetInstance().ApplicationDirectory + "Resource/App/MoveBottom.png";
		public static readonly string MoveTopImage = ConfigManager.GetInstance().ApplicationDirectory + "Resource/App/MoveTop.png";
		public static readonly string MoveLeftImage = ConfigManager.GetInstance().ApplicationDirectory + "Resource/App/MoveLeft.png";
		public static readonly string MoveRightImage = ConfigManager.GetInstance().ApplicationDirectory + "Resource/App/MoveRight.png";
		public static readonly string SaveImage = ConfigManager.GetInstance().ApplicationDirectory + "Resource/App/Save.png";
		public static readonly string SettingsImage = ConfigManager.GetInstance().ApplicationDirectory + "Resource/App/Settings.png";
	}
}
