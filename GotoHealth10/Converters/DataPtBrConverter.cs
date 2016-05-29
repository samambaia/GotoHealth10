using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace GotoHealth10.Converters
{
    class DataPtBrConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                return string.Empty;
            }

            var dataPtBr = ((DateTime)value).Day.ToString().PadLeft(2, 'O') + "/" + ((DateTime)value).Month.ToString().PadLeft(2, '0');

            return dataPtBr;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
