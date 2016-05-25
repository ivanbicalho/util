using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Util.Extension
{
    public static class StringExtension
    {
        public static T ToJsonObject<T>(this string s)
        {
            return JsonConvert.DeserializeObject<T>(s);
        }

        public static short ToInt16(this string s)
        {
            return Convert.ToInt16(s);
        }

        public static short? TryInt16(this string s)
        {
            short number;
            if (short.TryParse(s, out number))
            {
                return number;
            }

            return null;
        }

        public static int ToInt32(this string s)
        {
            return Convert.ToInt32(s);
        }

        public static int? TryInt32(this string s)
        {
            int number;
            if (int.TryParse(s, out number))
            {
                return number;
            }

            return null;
        }

        public static long ToInt64(this string s)
        {
            return Convert.ToInt64(s);
        }

        public static long? TryInt64(this string s)
        {
            long number;
            if (long.TryParse(s, out number))
            {
                return number;
            }

            return null;
        }

        public static double ToDouble(this string s)
        {
            return Convert.ToDouble(s);
        }

        public static double? TryDouble(this string s)
        {
            double number;
            if (double.TryParse(s, out number))
            {
                return number;
            }

            return null;
        }

        public static decimal ToDecimal(this string s)
        {
            return Convert.ToDecimal(s);
        }

        public static decimal? TryDecimal(this string s)
        {
            decimal number;
            if (decimal.TryParse(s, out number))
            {
                return number;
            }

            return null;
        }
    }
}
