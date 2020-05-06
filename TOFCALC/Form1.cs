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
            //double E1 = (double)((num_Pot2.Value - num_Pot1.Value) / (num_d1.Value*100));

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

            double d1 = (double)num_d1.Value * 100;
            double d2 = (double)num_d2.Value * 100;
            double d3 = (double)num_d3.Value * 100;
            double x = (double)num_x.Value * 100;
            double Vzi = (double)num_Vzi.Value;
            double zi = (double)num_Zi.Value * 100;
            double Pot1 = (double)num_Pot1.Value;
            double Pot2 = (double)num_Pot2.Value;



            double Va = Math.Sqrt(2 * x * q * ((Pot2 - Pot1) / m));

            double ta = ((2 * d1) / Va) * Math.Sqrt(1 + ((Math.Pow(Vzi, 2)) / Va) - (zi / d1) - (Vzi / Va));

            double Vb = Math.Sqrt(Math.Pow(Va, 2) * 2 * x * q * (Pot2 / m));

            double tb = ((2 * d2) / Va) * (Math.Sqrt(Math.Pow(Vb, 2) + Math.Pow(Vzi, 2) - (zi / d1) * Math.Pow(Va, 2)) - Math.Sqrt(Math.Pow(Va, 2) + Math.Pow(Vzi, 2) - (zi / d1) * Math.Pow(Va, 2)));

            double tlf = (d3 * (1 / Math.Sqrt(Math.Pow(Vb, 2) + Math.Pow(Vzi, 2) - (zi / d1) * Math.Pow(Va, 2))));

            double TOF = ta + tb + tlf;

            label10.Text = TOF.ToString();

        }
    }
}
