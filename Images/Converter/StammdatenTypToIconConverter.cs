using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using Common.Models;

namespace Images.Converter
{
    [ValueConversion(typeof(EnumStammdatenTyp), typeof(Uri))]
    public class StammdatenTypToIconConverter : IValueConverter
    {
        private const string URL_WASSER_ICON = "pack://application:,,,/Images;component/Images/Icon16/water_tap16x16.png";
        private const string URL_ENERGIE_ICON = "pack://application:,,,/Images;component/Images/Icon16/lightning16x16.png";
        private const string URL_TELEKOM_ICON = "pack://application:,,,/Images;component/Images/Icon16/phone16x16.png";
        private const string URL_OBJEKT_ICON = "pack://application:,,,/Images;component/Images/Icon16/house16x16.png";
        private const string URL_PERSON_ICON = "pack://application:,,,/Images;component/Images/Icon16/user16x16.png";

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(ImageSource))
                throw new InvalidOperationException("The target must be a EnumStammdatenTyp");

            var url = string.Empty;

            switch (value)
            {
                case EnumStammdatenTyp.PERSON:
                    url = URL_PERSON_ICON;
                    break;
                case EnumStammdatenTyp.OBJEKT:
                    url = URL_OBJEKT_ICON;
                    break;
                case EnumStammdatenTyp.VERSORGER_WASSER:
                    url = URL_WASSER_ICON;
                    break;
                case EnumStammdatenTyp.VERSORGER_ENERGIE:
                    url = URL_ENERGIE_ICON;
                    break;
                case EnumStammdatenTyp.VERSORGER_TELEKOM:
                    url = URL_TELEKOM_ICON;
                    break;
            }

            return new Uri(url);
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}
