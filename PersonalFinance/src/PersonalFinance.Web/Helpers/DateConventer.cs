using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalFinance.Web.Helpers
{
    public class DateConventer
    {
        public string ConvertNumberToMonth(int number)
        {
            var months = new Dictionary<int, string>()
            {
                { 1, "January" }, { 2, "February" }, {3, "March" }, {4, "April" }, {5, "May" }, {6, "June" },
                { 7, "July" }, {8, "August" }, {9, "September" }, {10, "October" }, {11, "November" }, {12, "December" }
            };

            if (number > 0 && number < 13)
            {
                return months[number];
            }
            else
            {
                return "";
            }
        }
    }
}
