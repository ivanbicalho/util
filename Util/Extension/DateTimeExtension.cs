using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Extension
{
    public static class DateTimeExtension
    {
        public static string Format(this DateTime date, FormatDateTime format)
        {
            switch (format)
            {
                case FormatDateTime.Bar_ddMMyyyyHHmmss:
                    return date.ToString("dd/MM/yyyy HH:mm:ss");
                case FormatDateTime.Trace_ddMMyyyyHHmmss:
                    return date.ToString("dd-MM-yyyy HH:mm:ss");
                case FormatDateTime.Clean_ddMMyyyyHHmmss:
                    return date.ToString("ddMMyyyyHHmmss");

                case FormatDateTime.Trace_yyyyMMddHHmmss:
                    return date.ToString("yyyy-MM-dd HH:mm:ss");
                case FormatDateTime.Clean_yyyyMMddHHmmss:
                    return date.ToString("yyyyMMddHHmmss");

                case FormatDateTime.Bar_ddMMyyyy:
                    return date.ToString("dd/MM/yyyy");
                case FormatDateTime.HHmmss:
                    return date.ToString("HH:mm:ss");
                case FormatDateTime.HHmm:
                    return date.ToString("HH:mm");
            }

            return date.ToString();
        }

        public static bool IsWeekend(this DateTime date)
        {
            return (date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday);            
        }

        public static bool Between(this DateTime date, DateTime initialDate, DateTime endDate)
        {
            return (date >= initialDate && date <= endDate);
        }
    }
}
