﻿using System;
using System.Globalization;
using Xamarin.Forms;

namespace WiringDiagramConstructionMachine.Converters
{
    public class ShowDeleteByRoleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
