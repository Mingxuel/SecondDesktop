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
		public static readonly BitmapImage AddImage = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resource/App/Add.png", UriKind.Absolute));
		public static readonly BitmapImage AppsImage = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resource/App/Apps.png", UriKind.Absolute));
		public static readonly BitmapImage DeleteImage = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resource/App/Delete.png", UriKind.Absolute));
		public static readonly BitmapImage DesktopImage = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resource/App/Desktop.png", UriKind.Absolute));
		public static readonly BitmapImage UpImage = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resource/App/Up.png", UriKind.Absolute));
		public static readonly BitmapImage DownImage = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resource/App/Down.png", UriKind.Absolute));
		public static readonly BitmapImage LogoImage = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resource/App/Logo.png", UriKind.Absolute));
		public static readonly BitmapImage MoveBottomImage = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resource/App/MoveBottom.png", UriKind.Absolute));
		public static readonly BitmapImage MoveTopImage = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resource/App/MoveTop.png", UriKind.Absolute));
		public static readonly BitmapImage MoveLeftImage = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resource/App/MoveLeft.png", UriKind.Absolute));
		public static readonly BitmapImage MoveRightImage = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resource/App/MoveRight.png", UriKind.Absolute));
		public static readonly BitmapImage SaveImage = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resource/App/Save.png", UriKind.Absolute));
		public static readonly BitmapImage SettingsImage = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resource/App/Settings.png", UriKind.Absolute));
	}
}
