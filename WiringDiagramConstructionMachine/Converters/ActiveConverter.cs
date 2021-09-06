using System;
using System.Globalization;
using Xamarin.Forms;

namespace WiringDiagramConstructionMachine.Converters
{
    public class ActiveConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value == true)
            {
                return "#4E82BD";
            }
            else
            {
                return "#80808080";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
