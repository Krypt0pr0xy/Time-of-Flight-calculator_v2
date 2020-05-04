using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TOFCALC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public class Constants
        {
            public const double e = 1.602176462e-19;
            public const double m = 5.48579909065e-4;

        }

        private void b_CALC_Click(object sender, EventArgs e)
        {
            double E1 = (double)((num_Pot2.Value - num_Pot1.Value) / (num_d1.Value*100));

            double q = Constants.e;

                if(rb1.Checked)
                {
                    q *= 1;
                }
                else if(rb2.Checked)
                {
                    q *= 2;
                }

            double m = Constants.m * (double)num_mass.Value;
            
            double va1 = (q * E1) / m;

            double t1 = Math.Sqrt((2 * va1 * (double)(num_d1.Value * 100) - 2 * va1 * (double)(num_x.Value * 100) + (double)(num_V0.Value)) / Math.Pow(va1, 2)) - ((double)(num_V0.Value) / va1);

            double V1 = va1 * t1+ (double)num_V0.Value;

            double E2 = (double)((num_Pot2.Value) / (num_d2.Value * 100));

            double va2 = (q * E2) / m;

            double t2 = Math.Sqrt((2 * va2 * (double)(num_d1.Value * 100)+) / Math.Pow(va1, 2)) - ((double)(num_V0.Value) / va1);
            label5.Text = va1.ToString();
        }
    }
}
