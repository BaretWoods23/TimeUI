using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeSheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeSheet.Tests
{
    [TestClass()]
    public class DayTests
    {

        [TestMethod()]
        public void TotalHoursTestUnder24()
        {
            Day d = new Day();
            d.SetHours(Day.HourTypes.VACATION, 23.75);

            Assert.IsTrue(d.Validate());

        }

        [TestMethod()]
        public void TotalHoursTestAt24()
        {
            Day d = new Day();
            d.SetHours(Day.HourTypes.REGULAR, 8);
            d.SetHours(Day.HourTypes.SICK, 8);
            d.SetHours(Day.HourTypes.VACATION, 8);

            Assert.IsTrue(d.Validate());
            
        }

        [TestMethod()]
        public void TotalHoursTestOver24()
        {
            Day x = new Day();
            x.SetHours(Day.HourTypes.REGULAR, 8);
            x.SetHours(Day.HourTypes.SICK, 12);
            x.SetHours(Day.HourTypes.VACATION, 8);

            Assert.IsFalse(x.Validate());

        }

        [TestMethod(), ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TotalHoursNegative()
        {
            Day r = new Day();

            //should throw exception
            r.SetHours(Day.HourTypes.REGULAR, -1);

        }


        [TestMethod()]
        public void GetTotalHoursTestExact()
        {
            Day d = new Day();
            d.SetHours(Day.HourTypes.REGULAR, 8.25);
            Assert.AreEqual(d.GetTotalHours(), 8.25);

        }

        [TestMethod()]
        public void GetTotalHoursTestUnder()
        {
            Day d = new Day();
            d.SetHours(Day.HourTypes.REGULAR, 8.249);
            Assert.AreEqual(8.00, d.GetTotalHours());
        }


        [TestMethod()]
        public void GetTotalHoursTestOver()
        {
            Day d = new Day();
            d.SetHours(Day.HourTypes.REGULAR, 8.251);
            Assert.AreEqual(8.25, d.GetTotalHours());
        }
    }
}