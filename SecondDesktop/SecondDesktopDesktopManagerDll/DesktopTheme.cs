using MaterialDesignColors;
using System.Runtime.Serialization;

namespace SecondDesktopDesktopManagerDll
{
	[DataContract]
	public class DesktopTheme
	{
		[DataMember]
		public bool IsDark { get; set; }
		[DataMember]
		public int Color { get; set; }

		public DesktopTheme Clone()
		{
			DesktopTheme item = new DesktopTheme();
			item.Color = Color;
			item.IsDark = IsDark;
			return item;
		}
	}
}