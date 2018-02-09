using System;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace EasyStore.converters
{
    public class Nan2Visible : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.ToString().Contains("Visible") ? Visibility.Hidden : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class Visible2Enable : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.ToString().Contains("Visible");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class Visible2NDisable : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value != null && !value.ToString().Contains("Visible");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class NotNull2Enable : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class Null2Bool : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (value.ToString() != "") ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class NotEmpty2Visible : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return string.IsNullOrEmpty(value.ToString()) ? Visibility.Hidden : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
    public class EnumToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.Equals(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.Equals(true) ? parameter : Binding.DoNothing;
        }
    }
    [ValueConversion(typeof(object), typeof(string))]
    public class ConcatenateFieldsMultiValueConverter : IMultiValueConverter
    {
        public object Convert(
                    object[] values,
                    Type targetType,
                    object parameter,
                    System.Globalization.CultureInfo culture
                 )
        {
            string strDelimiter;
            StringBuilder sb = new StringBuilder();

            if (parameter != null)
            {
                //Use the passed delimiter.
                strDelimiter = parameter.ToString();
            }
            else
            {
                //Use the default delimiter.
                strDelimiter = ", ";
            }

            //Concatenate all fields
            foreach (object value in values)
            {
                if (value != null && value.ToString().Trim().Length > 0)
                {
                    if (sb.Length > 0) sb.Append(strDelimiter);
                    sb.Append(value.ToString());
                }
            }

            return sb.ToString();
        }

        public object[] ConvertBack(
                    object value,
                    Type[] targetTypes,
                    object parameter,
                    System.Globalization.CultureInfo culture
              )
        {
            throw new NotImplementedException("ConcatenateFieldsMultiValueConverter cannot convert back (bug)!");
        }

    }


}

