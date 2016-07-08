using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeSheet
{
    //TODO: edit hours after
    public class Day
    {
        public enum HourTypes {REGULAR, SICK, VACATION};
        
        //Examine the HourTypes enum dynamically to set the array size
        private double[] times = new double[Enum.GetValues(typeof(HourTypes)).Length];
        
        public void SetHours(HourTypes timeType, double hour)
        {
            if (hour >= 0 && hour <= 24)
            {
                //round _down_ to the nearest .25 hour increment
                hour = hour - (hour % .25);

                times[(int)timeType] = hour;
            }
            else
            {
                throw new ArgumentOutOfRangeException("hour", "Hours must be between 0 and 24 inclusive.");
            }
        }

        public double GetHours(HourTypes types)
        { 
            return times[(int)types]; 
        }

        public bool Validate()
        {
            if (GetTotalHours() > 24)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public double GetTotalHours()
        {
            double total = 0;
            foreach(double t in times)
            {
                total += t;
            }
            return total;
            
        }
    }
}
