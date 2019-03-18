using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SecondDesktop
{
    class MSubAppFrame
    {
        public double Width;
        public double Height;

        public string AppImage;
        public string AppTitle;

        public string MoveUpImage;
        public string MoveDownImage;
        public string MoveLeftImage;
        public string MoveRightImage;
        public string AppDeleteImage;

        public Visibility MoveUpVisibility;
        public Visibility MoveDownVisibility;
        public Visibility MoveLeftVisibility;
        public Visibility MoveRightVisibility;
        public Visibility AppDeleteVisibility;
    }
}
