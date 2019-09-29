using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinance.Web.Helpers.Tests
{
    [TestFixture]
    public class DateConventerTests
    {
        [Test]
        public void ConvertNumberToMonthTest_TakeNumber_ReturnMonth([Range(1, 12, 1)] int month)
        {
            var months = new Dictionary<int, string>()
            {
                { 1, "January" }, { 2, "February" }, {3, "March" }, {4, "April" }, {5, "May" }, {6, "June" },
                { 7, "July" }, {8, "August" }, {9, "September" }, {10, "October" }, {11, "November" }, {12, "December" }
            };

            var dateConventer = new DateConventer();

            Assert.That(dateConventer.ConvertNumberToMonth(month), Is.EqualTo(months[month]));
            Assert.That(dateConventer.ConvertNumberToMonth(0), Is.EqualTo(""));
            Assert.That(dateConventer.ConvertNumberToMonth(13), Is.EqualTo(""));
            Assert.That(dateConventer.ConvertNumberToMonth(-4), Is.EqualTo(""));
        }
    }
}
