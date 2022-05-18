using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace web
{
    public class NumberSettings
    {
        public string CommaSeparate(decimal number)
        {
            var strNum = String.Format(new CultureInfo("en-IN", false), "{0:n}", Convert.ToDouble(number));
            return strNum;

        }
    }
}