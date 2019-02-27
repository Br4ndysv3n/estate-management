namespace Common.Helper
{
    using System;
	using System.Reflection;
	using System.Windows;

    public static class PropertyHelper
    {

        /// <summary>
        /// Konvertiert ein String zu einem Objekt vom Typ T
        /// </summary>
        /// <typeparam name="T">Zieldatentyp</typeparam>
        /// <param name="value">Stringwert, der konvertiert werden soll</param>
        /// <param name="defaultValue">Standardwert, wenn beim konvertieren ein Fehler auftritt</param>
        /// <param name="converter">Hilfsmethode, um String in ein komplexeren Objekt vom Typ T zu wandeln</param>
        /// <returns>Konvertierter String zu einem Objekt vom Typ T</returns>
        public static T ConvertFromString<T>(string value, T defaultValue = default(T), Func<string, T> converter = null)
        {
            if (converter != null)
            {
                return converter(value);
            }
            else if (typeof(T) == typeof(int))
            {
                try
                {
                    return (T)(object)Int32.Parse(value);
                }
                catch
                {
                    return defaultValue;
                }
            }
            else if (typeof(T) == typeof(long))
            {
                try
                {
                    return (T)(object)Int64.Parse(value);
                }
                catch
                {
                    return defaultValue;
                }
            }
            else if (typeof(T) == typeof(double))
            {
                try
                {
                    return (T)(object)Double.Parse(value);
                }
                catch
                {
                    return defaultValue;
                }
            }
            else if (typeof(T) == typeof(bool))
            {
                try
                {
                    return (T)(object)(value != null && !value.Equals("0", StringComparison.OrdinalIgnoreCase) && !value.Equals("false", StringComparison.OrdinalIgnoreCase));
                }
                catch
                {
                    return defaultValue;
                }
            }
            else
            {
                return (T)(object)value;
            }
        }

        /// <summary>
        /// Konvertiert ein Objekt vom Typ T zu einem String, um ihn abspeichern zu können
        /// </summary>
        /// <typeparam name="T">Ausgangsdatentyp</typeparam>
        /// <param name="value">Wert des Ausgangsdatentyps</param>
        /// <param name="defaultValue">Standardwert, wenn beim Konvertieren ein Fehler auftritt</param>
        /// <param name="converter">Hilfsmethode, um komplexere Objekte zu einem String konvertieren zu können</param>
        /// <returns>Konvertiertes Objekt</returns>
        public static string ConvertToString<T>(T value, string defaultValue = null, Func<T, string> converter = null)
        {
            try
            {
                if (converter != null)
                {
                    return converter(value);
                }
                else
                {
                    return Convert.ToString(value);
                }
            }
            catch
            {
                return defaultValue;
            }
        }

        public static DependencyProperty GetDependencyPropertyByName(Type dependencyObjectType, string dpName)
        {
            DependencyProperty dp = null;

            var fieldInfo = dependencyObjectType.GetField(dpName, BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            if (fieldInfo != null)
            {
                dp = fieldInfo.GetValue(null) as DependencyProperty;
            }

            return dp;
        }
    }
}
