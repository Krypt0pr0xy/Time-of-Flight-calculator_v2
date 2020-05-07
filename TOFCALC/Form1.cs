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
            public const double m = 1.6605306660e-27;

        }

        private void b_CALC_Click(object sender, EventArgs e)
        {
            //double E1 = (double)((num_Pot2.Value - num_Pot1.Value) / (num_d1.Value*100));

            double q = Constants.e;

            if (rb1.Checked)
            {
                q *= 1;
            }
            else if (rb2.Checked)
            {
                q *= 2;
            }

            double m = Constants.m * (double)num_mass.Value;

            double d_Quelle = (double)num_d_Quelle.Value / 100;
            double d_Beschleunigung = (double)num_d_Beschleunigung.Value / 100;
            double d_Drifstrecke = (double)num_d_Drifstrecke.Value / 100;
            double x = (double)num_x.Value / 100;
            double Vzi = (double)num_Vzi.Value;
            double zi = (double)num_Zi.Value / 100;
            double Pot1 = (double)num_Pot1.Value;
            double Pot2 = (double)num_Pot2.Value;



            double Va = Math.Sqrt(2 * x * q * ((Pot1 - Pot2) / m));

            double ta = ((2 * d_Quelle) / Va) * (Math.Sqrt(1 + (Math.Pow(Vzi / Va, 2) - (zi / d_Quelle))) - (Vzi / Va));

            double Vb = Math.Sqrt(Math.Pow(Va, 2) * 2 * x * q * (Pot2 / m));

            double tb = ((2 * d_Beschleunigung) / (Math.Pow(Vb,2)-Math.Pow(Va,2))) * (Math.Sqrt(Math.Pow(Vb, 2) + Math.Pow(Vzi, 2) - (zi / d_Quelle) * Math.Pow(Va, 2)) - Math.Sqrt(Math.Pow(Va, 2) + Math.Pow(Vzi, 2) - (zi / d_Quelle) * Math.Pow(Va, 2)));

            double tlf = d_Drifstrecke * (1 / (Math.Sqrt(Math.Pow(Vb, 2) + Math.Pow(Vzi, 2) - (zi / d_Quelle) * Math.Pow(Va, 2))));

            double TOF = ta + tb + tlf;

            l_ta.Text = ta.ToString();

            l_tb.Text = tb.ToString();

            l_tlf.Text = tlf.ToString();

            l_TOF.Text = TOF.ToString();

        }

        private void num_d_Beschleunigung_ValueChanged(object sender, EventArgs e)
        {
            if(num_d_Beschleunigung.Value == 0)
            {
                num_Pot2.Value = 0;
            }
        }

        private void num_Pot1_ValueChanged(object sender, EventArgs e)
        {
            while(num_Pot1.Value <= num_Pot2.Value)
            {
                num_Pot2.Value -= 1;
            }
        }

        private void num_Pot2_ValueChanged(object sender, EventArgs e)
        {
            while (num_Pot1.Value <= num_Pot2.Value)
            {
                num_Pot1.Value += 1;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBoxElemente.Text == "Wasserstoff")
            {
                num_mass.Value = (decimal)1.008;
            }
            else if (comboBoxElemente.Text == "Helium")
            {
                num_mass.Value = (decimal)4.002602;
            }


        }
    }
}
