using NDC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.Utility
{
    public class DateSettings
    {
        public string ConvertToNepaliDate(DateTime englishDate)
        {
            DateConverter nepaliDate = NDC.DateConverter.ConvertToNepali(englishDate.Year, englishDate.Month, englishDate.Day);
            string month = nepaliDate.Month.ToString();
            if (nepaliDate.Month < 10)
            {
                month = "0" + month;
            }
            string day = nepaliDate.Day.ToString();
            if (nepaliDate.Day < 10)
            {
                day = "0" + day;
            }
            return string.Format("{0}/{1}/{2}", nepaliDate.Year, month, day);
        }

        public string ConvertToEnglishDate(string nepaliDateStr)
        {
            if (string.IsNullOrEmpty(nepaliDateStr))
                return null;

            var data = SplitNepaliDate(nepaliDateStr);
            if (data == null)
                return null;

            return GetEnglishDate(data[0], data[1], data[2]);
        }

        public List<string> SplitNepaliDate(string nepaliDateStr)
        {
            nepaliDateStr = nepaliDateStr.Replace('/', '-');
            string[] str = nepaliDateStr.Split('-');
            if (str.Count() < 3)
            {
                return null;
            }
            string nepaliYear = str[0];
            string nepaliMonth = str[1];
            string nepaliDate = str[2];

            if (string.IsNullOrEmpty(nepaliMonth) || string.IsNullOrEmpty(nepaliDate) || string.IsNullOrEmpty(nepaliYear))
            {
                return null;
            }
            return str.ToList();
        }

        public string GetEnglishDate(string nepaliYear, string nepaliMonth, string nepaliDate)
        {
            DateConverter englishDate = NDC.DateConverter.ConvertToEnglish(Convert.ToInt32(nepaliYear),
                                                                            Convert.ToInt32(nepaliMonth),
                                                                            Convert.ToInt32(nepaliDate));

            int year=englishDate.Year;
            int month=englishDate.Month;
            int day=englishDate.Day;
            if (year == 0 || month == 0 || day == 0)
                return null;

            return string.Format("{0}/{1}/{2}", year, month, day);
        }

        public bool CheckNepaliDateValidity(string nepaliDateStr)
        {
            var split = SplitNepaliDate(nepaliDateStr);
            if (split == null)
                return false;
            var data = GetEnglishDate(split[0], split[1], split[2]);
            if (data == null || data == "0/0/0")
                return false;

            return true;
        }
    }
}