using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace GotoHealth10.Converters
{
    class WorseWeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string result = (language == "pt-BR" ? "Pior Peso: " : "Worse Weight: ");
            var weight = value.ToString();

            if (value != null || value.ToString() != "0")
            {
                var vlr = double.Parse(value.ToString(), CultureInfo.CurrentUICulture);
                result = result + vlr.ToString();
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
