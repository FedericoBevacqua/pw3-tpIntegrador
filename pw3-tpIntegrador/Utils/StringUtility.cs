﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pw3_tpIntegrador.Utils
{
    public static class StringUtility
    {
        //es un extension method
        public static string Truncar(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }
    }
}