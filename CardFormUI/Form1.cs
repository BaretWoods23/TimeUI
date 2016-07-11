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

namespace CardFormUI
{
    public partial class CardUI : Form
    {
        public int TypeChecker = 0;
        TimeSheet.Day.HourTypes HourType;
        public CardUI()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void Week1Lbl_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Week2Lbl_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Save_Click(object sender, EventArgs e)
        {
            HourType = TimeSheet.Day.HourTypes.REGULAR;
            foreach (TextBox tb in this.Controls.OfType<TextBox>().OrderBy(ord => ord.TabIndex))
                {
                    if (tb.Text != string.Empty)
                    {
                    TimeCard.days[TypeChecker].SetHours(HourType, int.Parse(tb.Text));
                    }
                    TypeChecker += 1;
                if (TypeChecker > 6)
                {
                    HourType = TimeSheet.Day.HourTypes.SICK;
                }else if (TypeChecker > 13)
                {
                    HourType = TimeSheet.Day.HourTypes.VACATION;
                }
            }
        }
        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
