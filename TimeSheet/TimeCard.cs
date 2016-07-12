using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeSheet
{
    public class TimeCard
    {
        public const int DAYS_IN_WEEK = 7;
        public const int WEEK_COUNT = 2;
        public const int DAYS_IN_PAY_PERIOD = WEEK_COUNT * DAYS_IN_WEEK; //must be multiple DAYS_IN_WEEK;

        public double[] overtime = new double[WEEK_COUNT]; //one array element for each week in the pay period

        public List<Day> days;

        public DateTime StartDate { get; private set; }

        public TimeCard(DateTime start)
        {
            if (start.DayOfWeek == DayOfWeek.Sunday)
            {
                StartDate = start;
            } else
            {
                throw new ArgumentOutOfRangeException("start", "TimeCard start date must be a Sunday.");
            }
            days = new List<Day>();

            for (int i = 0; i < DAYS_IN_PAY_PERIOD; i++)
            {
                days.Add(new Day());
            }
        }
        public double[] CalculateOverTime()
        {
            
            //reset the array
            for (int i = 0; i < overtime.Length; i++)
            {
                overtime[i] = 0;
            }

            //sum the regular hours for each week
            for (int i = 0; i < DAYS_IN_PAY_PERIOD; i++)
            {
                overtime[i / DAYS_IN_WEEK] += days[i].GetHours(Day.HourTypes.REGULAR);
            }

            //calculate overtime
            for (int i = 0; i < overtime.Length; i++)
            {
                if (overtime[i] > 40)
                {
                    overtime[i] -= 40;
                }
            }
            return overtime;
        }

        public double GetHours(Day.HourTypes type)
        {
            double hours = 0;
            for (int i = 0; i < DAYS_IN_PAY_PERIOD; ++i)
            {
                hours += days[i].GetHours(type);
            }
            return hours;
        }

        public void SetHours(int day, Day.HourTypes timeType, double hour)
        {
            days[day].SetHours(timeType, hour);
        }

    }
}
