using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Workers;
using Worker;

namespace Workers
{
   
        public class StringFormatConverter : IValueConverter
        {
       

        public object Convert(object value, System.Type targetType, object parameter, string language)
        {
            return string.Format(parameter as string, value);
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, string language)
        {
            return null;
        }
    }
    }

