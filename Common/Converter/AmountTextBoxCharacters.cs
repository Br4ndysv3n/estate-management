using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Converter
{
    using System.Globalization;
    using System.Windows.Controls;
    using System.Windows.Data;

    /// <summary>
    /// The amount textbox characters (MaxLength - Text.Length).
    /// </summary>
    public class AmountTextBoxCharacters : IMultiValueConverter
    {
        /// <summary>
        /// The convert textbox text and textbox maxlenght to string amount of textbox characters
        /// </summary>
        /// <param name="values">
        /// The values: textbox text and textbox maxlenght
        /// </param>
        /// <param name="targetType">
        /// The target type.
        /// </param>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        /// <param name="culture">
        /// The culture.
        /// </param>
        /// <returns>
        /// The System.Object.
        /// </returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null && values.Count() >= 2 && values[0] != null && values[1] != null)
            {
                int a, b;

                if (int.TryParse(values[1].ToString(), out a))
                {
                    b = values[0].ToString().Length;
                    return (a - b).ToString();
                }

            }

            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
