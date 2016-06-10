using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace GotoHealth10.Converters
{
    class IMCConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string result = "Sem Aferição";

            if (value != null)
            {
                //var imc = value.ToString().Replace('.',',');
                var imc = value.ToString();

                var vlr = double.Parse(value.ToString(), CultureInfo.CurrentUICulture);
                if (vlr < 17)
                {
                    result = imc + " - Muito abaixo do Peso";
                }
                else if (vlr >= 17 && vlr <= 18.49)
                {
                    result = imc + " - Abaixo do Peso";
                }
                else if (vlr >= 18.5 && vlr <= 24.99)
                {
                    result = imc + " - Peso Normal";
                }
                else if (vlr >= 25 && vlr <= 29.99)
                {
                    result = imc + " - Acima do Peso";
                }
                else if (vlr >= 30 && vlr <= 34.99)
                {
                    result = imc + " - Obesidade Moderada";
                }
                else if (vlr >= 35 && vlr <= 39.99)
                {
                    result = imc + " - Obesidade Severa";
                }
                else if (vlr >= 40)
                {
                    result = imc + " - Obesidade Morbida";
                }
            }

            return "IMC: " + result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
