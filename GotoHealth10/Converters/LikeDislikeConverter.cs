using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace GotoHealth10.Converters
{
    class LikeDislikeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                var imgPath = value.ToString() == "0" ? "ms-appx:///Assets/like_Light.png" : "ms-appx:///Assets/dislike_Light.png";
                return imgPath;
            }
            else
                return "\\Assets\\like_Light.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
