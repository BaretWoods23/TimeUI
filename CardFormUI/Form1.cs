using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimeSheet;
using System.Diagnostics;

namespace CardFormUI
{
    public partial class CardUI : Form
    {
        static DateTime BeginDate = new DateTime(2016, 7, 3);
        TimeCard TC = new TimeCard(BeginDate);
        public int TypeChecker = 0;
        public int DayChecker = 0;
        public int WeekNum = 0;
        public int SecondSunOnList = 7;
        public int i = 0;
        TimeSheet.Day.HourTypes HourType = TimeSheet.Day.HourTypes.REGULAR;
        public CardUI()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetTextOnForm();
        }
        private void Week1Lbl_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.Checked) //This code and above is just here to make sure this radio button isn't activated twice
                {
                     SetDates((WeekNum - WeekNum) - 1);
                     SetTextOnForm();
                }
            }
        }
        private void Week2Lbl_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.Checked)
                {
                    SetDates((WeekNum - WeekNum) + 1);
                    SetTextOnForm();
                }
            }
        }
        private void SetTextOnForm()
        {
            foreach (TextBox tb in this.Controls.OfType<TextBox>().OrderBy(ord => ord.TabIndex))
            {
                    if (Week1Lbl.Checked)
                    {
                        if (TC.days[DayChecker].GetHours(HourType).ToString() != "0")
                        {
                            tb.Text = TC.days[DayChecker].GetHours(HourType).ToString();
                        }
                        else
                        {
                            tb.Text = "";
                        }
                    }
                    else if (Week2Lbl.Checked)
                    {
                        if(TC.days[DayChecker + TimeCard.DAYS_IN_WEEK].GetHours(HourType).ToString() != "0")
                        {
                            tb.Text = TC.days[DayChecker + TimeCard.DAYS_IN_WEEK].GetHours(HourType).ToString();
                        }
                        else
                        {
                            tb.Text = "";
                        }
                    }
                    SetHourType();
                    DayCheckTypeLoop();
            }
        }
        private void Save_Click(object sender, EventArgs e)
        {
            foreach (TextBox tb in this.Controls.OfType<TextBox>().OrderBy(ord => ord.TabIndex))
                {
                    if (tb.Text != string.Empty)
                    {
                        if (Week1Lbl.Checked)
                        {
                            TC.days[DayChecker].SetHours(HourType, double.Parse(tb.Text));
                        }
                        else if (Week2Lbl.Checked)
                        {
                            TC.days[DayChecker+TimeCard.DAYS_IN_WEEK].SetHours(HourType, double.Parse(tb.Text));
                        }
                    }
                SetHourType();
                DayCheckTypeLoop();
            }
            Debug.WriteLine("HELLO");
            Debug.WriteLine(TC.days[0].GetHours(TimeSheet.Day.HourTypes.REGULAR).ToString());
            Debug.WriteLine(TC.days[0].GetHours(TimeSheet.Day.HourTypes.SICK).ToString());
            Debug.WriteLine(TC.days[0].GetHours(TimeSheet.Day.HourTypes.VACATION).ToString());
            Debug.WriteLine(TC.days[1].GetHours(TimeSheet.Day.HourTypes.REGULAR).ToString());
            Debug.WriteLine(TC.days[1].GetHours(TimeSheet.Day.HourTypes.SICK).ToString());
            Debug.WriteLine(TC.days[1].GetHours(TimeSheet.Day.HourTypes.VACATION).ToString());
            Debug.WriteLine(TC.days[2].GetHours(TimeSheet.Day.HourTypes.REGULAR).ToString());
            Debug.WriteLine(TC.days[2].GetHours(TimeSheet.Day.HourTypes.SICK).ToString());
            Debug.WriteLine(TC.days[2].GetHours(TimeSheet.Day.HourTypes.VACATION).ToString());
            Debug.WriteLine(TC.days[3].GetHours(TimeSheet.Day.HourTypes.REGULAR).ToString());
            Debug.WriteLine(TC.days[3].GetHours(TimeSheet.Day.HourTypes.SICK).ToString());
            Debug.WriteLine(TC.days[3].GetHours(TimeSheet.Day.HourTypes.VACATION).ToString());
        }
        private void DayCheckTypeLoop()
        {
            DayChecker ++;
            if(DayChecker > 6)
            {
                DayChecker = 0;
            }
        }
        private void SetHourType()
        {
            TypeChecker ++;
            if (TypeChecker > 6 && TypeChecker <= 13) //Loops through Regular hours first, then goes to the second row after 6 loops.
            {
                HourType = TimeSheet.Day.HourTypes.SICK;
            }
            else if (TypeChecker > 13) //Now loops through third row.
            {
                HourType = TimeSheet.Day.HourTypes.VACATION;
            }
        }
        private void SetDates(int WeekNumber)
        {
            foreach(Label lbl in this.Controls.OfType<Label>())
                {
                if (lbl.Text.ToString().Contains("/"))
                {
                    DateTime dt = Convert.ToDateTime(lbl.Text.ToString());
                    dt = dt.AddDays(WeekNumber * TimeCard.DAYS_IN_WEEK);
                    String Date = dt.ToString().Remove(dt.ToString().IndexOf(" "));
                    String Year = Date.Substring(Date.Length-2);
                    lbl.Text = Date.Substring(0, Date.Length - 4) + Year;
                }
            }
            WeekNum = WeekNumber;
        }
        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
