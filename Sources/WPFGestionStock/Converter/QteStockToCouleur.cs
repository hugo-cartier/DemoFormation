using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPFGestionStock.Converter
{
    public class QteStockToCouleur : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //Rectangle TestRect = new Rectangle();
            //TestRect.Fill =  new SolidColorBrush(Colors.Red);
            if (value is float)
            {
                if ((float)value > 0 && (float)value < 10)
                    return new SolidColorBrush(Colors.Red);
                if ((float)value > 90 && (float)value <= 100)
                    return new SolidColorBrush(Colors.Green);
            }
            return new SolidColorBrush(Colors.Transparent);
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
