using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Xml.Linq;

namespace DataBaseClient
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        private static class Params
        {
            public const string IsReverse = "r";
            public const string UseHidden = "h";
        }

        public BooleanToVisibilityConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool val = System.Convert.ToBoolean(value, CultureInfo.InvariantCulture);
            bool useHidden = false;
            if (parameter != null)
            {
                foreach (var paramitem in (parameter as string).Split(';'))
                {
                    if (paramitem.ToLower() == Params.IsReverse)
                    {
                        val = !val;
                    }
                    if (paramitem.ToLower() == Params.UseHidden)
                    {
                        useHidden = true;
                    }
                }
            }

            if (val)
            {
                return Visibility.Visible;
            }
            return useHidden ? Visibility.Hidden : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility)
            {
                Visibility vsbl = (Visibility)value;
                return vsbl == Visibility.Visible;
            }
            else
            {
                throw new ArgumentException($"Transforming object must be of type Visibility (actual type: {value.GetType()})");
            }
        }
    }

    [ValueConversion(typeof(bool), typeof(bool))]
    public class BooleanNegationConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }
    }

    [ValueConversion(typeof(TypeDB), typeof(bool))]
    public class RadioButtonConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            TypeDB i = (TypeDB)value;
            TypeDB param = (TypeDB)parameter;
            var res = i == param;
            return  res;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return parameter;
        }
    }
    [ValueConversion(typeof(string), typeof(bool))]
    public class StringToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !String.IsNullOrEmpty((string)value);
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
