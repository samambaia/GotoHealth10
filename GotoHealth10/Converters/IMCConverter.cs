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

            string result = (language == "pt-BR" ? "Sem Aferição" : "Without Verification");
            var imc = value.ToString();

            if (value != null || value.ToString() != "0")
            {
                var vlr = double.Parse(value.ToString(), CultureInfo.CurrentUICulture);
                if (vlr < 17)
                {
                    result = (language =="pt-BT" ? " - Muito abaixo do Peso" : " - Very Underweight");
                }
                else if (vlr >= 17 && vlr <= 18.49)
                {
                    result = (language == "pt-BT" ? " - Abaixo do Peso" : " - Under Weight");
                }
                else if (vlr >= 18.5 && vlr <= 24.99)
                {
                    result = (language == "pt-BT" ? " - Peso Normal" : " - Normal Weight");
                }
                else if (vlr >= 25 && vlr <= 29.99)
                {
                    result = (language == "pt-BT" ? " - Acima do Peso" : " - Overweight");
                }
                else if (vlr >= 30 && vlr <= 34.99)
                {
                    result = (language == "pt-BT" ? " - Obesidade Moderada" : " - Moderate Obesity");
                }
                else if (vlr >= 35 && vlr <= 39.99)
                {
                    result = (language == "pt-BT" ? " - Obesidade Severa" : " - Severe Obesity");
                }
                else if (vlr >= 40)
                {
                    result = (language == "pt-BT" ? " - Obesidade Morbida": " - Morbid Obesity");
                }
            }

            return (language == "pt-BT" ? "IMC: " : "BMI: ") + imc + result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
