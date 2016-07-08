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
    public class TimeCardTests
    {
        //DateTime cannot be declared as const
        private readonly DateTime SUNDAY_START = new DateTime(2016, 7, 3);

        [TestMethod()]
        public void TestHourTypes()
        {
            //arrange
            TimeCard t = new TimeCard(SUNDAY_START);
            double sicktime = 14;
            double expected = 168;

            //act
            for (int i = 0; i < TimeCard.DAYS_IN_PAY_PERIOD; i++)
            {
                t.SetHours(i, Day.HourTypes.REGULAR, 12);
                t.SetHours(i, Day.HourTypes.SICK, 1);
            }

            //assert
            Assert.AreEqual(sicktime, t.GetHours(Day.HourTypes.SICK));
            Assert.AreEqual(expected, t.GetHours(Day.HourTypes.REGULAR));
        }

        [TestMethod()]
        public void TestOverTime()
        {
            //arrange
            TimeCard t = new TimeCard(SUNDAY_START);
            double[] expected = new double[] { 44, 44 };

            //act
            for (int i = 0; i < TimeCard.DAYS_IN_PAY_PERIOD; i++)
            {
                t.SetHours(i, Day.HourTypes.REGULAR, 12);
                t.SetHours(i, Day.HourTypes.SICK, 1);
            }
            double[] actual = t.CalculateOverTime();

            //assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TimeCardTestOnSunday()
        {
            DateTime expected = SUNDAY_START;

            //should not throw an exception 
            TimeCard t = new TimeCard(SUNDAY_START);

            Assert.AreEqual(expected.Date, t.StartDate.Date);
            
        }

        [TestMethod(), ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TimeCardTestNotOnSunday()
        {
            //should throw an exception 
            TimeCard t = new TimeCard(new DateTime(2016, 7, 1));

            //redundant, with the ExpectedException method annotation
            Assert.Fail("Expected ArgumentOutOfRangeException not thrown");

        }
    }
}