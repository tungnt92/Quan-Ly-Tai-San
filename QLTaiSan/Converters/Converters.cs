using QLTaiSan.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace QLTaiSan.Converters
{
    public sealed class DateTimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            DateTime datetime = (DateTime)value;
            return datetime.ToString("MM/dd/yyyy");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public sealed class PriceKhauHaoToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            double priceKhauHao = 0;
            taisan tsan = (taisan)value;
            DateTime timeNow = DateTime.Now.Date;
            int sonammua = ((timeNow - tsan.datebuy).Days) / 365;

            if(sonammua <= tsan.timeuse)
            {
                priceKhauHao = double.Parse(tsan.price) - double.Parse(tsan.price) * (tsan.tylehaomon/100) * sonammua;
            }
            else
            {
                priceKhauHao = 0;
            }

            return priceKhauHao;
            //Debug.WriteLine(timeNow);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}