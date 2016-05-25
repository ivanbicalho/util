using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Extension
{
    public enum FormatDateTime
    {
        /// <summary>
        /// Fomato com barra: dd/MM/yyyy HH:mm:ss
        /// </summary>
        Bar_ddMMyyyyHHmmss = 1,

        /// <summary>
        /// Formato limpo: ddMMyyyyHHmmss
        /// </summary>
        Clean_ddMMyyyyHHmmss = 2,

        /// <summary>
        /// Formato com traço: dd-MM-yyyy HH:mm:ss
        /// </summary>
        Trace_ddMMyyyyHHmmss = 3,

        /// <summary>
        /// Formato limpo: yyyyMMddHHmmss
        /// </summary>
        Clean_yyyyMMddHHmmss = 4,

        /// <summary>
        /// Formato com traço: yyyy-MM-dd HH:mm:ss
        /// </summary>
        Trace_yyyyMMddHHmmss = 5,

        /// <summary>
        /// Fomato com barra: dd/MM/yyyy
        /// </summary>
        Bar_ddMMyyyy = 6,

        /// <summary>
        /// Somente hora com segundos: HH:mm:ss
        /// </summary>
        HHmmss = 7,

        /// <summary>
        /// Somente hora sem segundos: HH:mm
        /// </summary>
        HHmm = 8
    }
}
