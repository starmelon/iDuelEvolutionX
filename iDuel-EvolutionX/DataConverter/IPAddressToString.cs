using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Data;

namespace iDuel_EvolutionX.DataConverter
{
    /// <summary>
    ///IPAddress与String的转换器
    /// </summary>
    [ValueConversion(typeof(IPAddress), typeof(string))]
    public class IPAddressToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            //value.ToString();
            //MainWindow mw = Application.Current.MainWindow as MainWindow;
            //MyCanvas mcv = mw.FindName(parameter.ToString()) as MyCanvas;
            IPAddress ip = (IPAddress)value;
            return ip.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string ip = (string)value;
            IPAddress _ip;
            if (IPAddress.TryParse(ip, out _ip))
            {
                return _ip;
            }
            return null;
        }
    }
}
