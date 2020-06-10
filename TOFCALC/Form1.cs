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
            this.Text = "TOF Calculator";
            for (int i = 0; i < Constants.max_elements; i++)
            {
                comboBoxElemente.Items.Add(array2D_Elements[0, i]);
            }
        }

        public class Constants
        {
            public const double e = 1.602176462e-19;//Globale Variable Ladung e
            public const double m = 1.6605306660e-27;//Globale Variable für masse einheit u
            public const int max_elements = 25;
        }
        //refernz von https://de.webqc.org/mmcalc.php
        string[,] array2D_Elements = new string[2, Constants.max_elements] {
            {"H",    "H2",   "He",      "He2",     "HD",      "CH3F",   "OCS",    "C",       "S",      "O",       "O2",      "H2O",     "N2",       "CH",       "CH2",     "CH3",    "CO",     "Ar",     "Ar2",    "Ne",      "Ne2",    "Xe",      "Xe2",     "Mg",      "MgAr"},
            {"1,008","2.016","4,002602","8,005204","3,022042","34,0329","60,0751","12,01070","32,0650","15,99940","31,99880","18,01528","28,01340", "13,01864", "14,02658","15,0345","28,0101","39,9480","79,8960","20,17970","40,3594","131,2930","262,5860","24,30500","64,2530"} };

        private void b_CALC_Click(object sender, EventArgs e)
        {

            double q = Constants.e;

            //abfrage Doppelte ladung oder einzel ladung
            if (rb1.Checked)
            {
                q *= 1;
            }
            else if (rb2.Checked)
            {
                q *= 2;
            }

            //Constante aus Globale Variable 
            double m = Constants.m * (double)num_mass_u.Value;

            double d_Quelle = (double)num_d_source_cm.Value / 100;
            double d_Beschleunigung = (double)num_d_acceleration_cm.Value / 100;
            double d_Drifstrecke = (double)num_d_drift_distance_cm.Value / 100;
            double x = (double)num_x_cm.Value / 100;
            double Vzi = (double)num_Vzi.Value;
            double zi = (double)num_Zi_cm.Value / 100;
            double Pot1_V = (double)num_Pot1_V.Value;
            double Pot2_V = (double)num_Pot2_V.Value;



            double Va = Math.Sqrt(2 * x * q * ((Pot1_V - Pot2_V) / m));

            double ta = ((2 * d_Quelle) / Va) * (Math.Sqrt(1 + (Math.Pow(Vzi / Va, 2) - (zi / d_Quelle))) - (Vzi / Va));

            double Vb = Math.Sqrt(Math.Pow(Va, 2) * 2 * x * q * (Pot2_V / m));

            double tb = ((2 * d_Beschleunigung) / (Math.Pow(Vb,2)-Math.Pow(Va,2))) * (Math.Sqrt(Math.Pow(Vb, 2) + Math.Pow(Vzi, 2) - (zi / d_Quelle) * Math.Pow(Va, 2)) - Math.Sqrt(Math.Pow(Va, 2) + Math.Pow(Vzi, 2) - (zi / d_Quelle) * Math.Pow(Va, 2)));

            double tlf = d_Drifstrecke * (1 / (Math.Sqrt(Math.Pow(Vb, 2) + Math.Pow(Vzi, 2) - (zi / d_Quelle) * Math.Pow(Va, 2))));

            double TOF = ta + tb + tlf;

            if (ta.ToString() == "NaN" || tb.ToString() == "NaN" || tlf.ToString() == "NaN" || TOF.ToString() == "NaN")
            {
                l_ta.Text = "Invalid Input";

                l_tb.Text = "";

                l_tlf.Text = "";

                l_TOF.Text = "";
            }
            else
            {
                l_ta.Text = ta.ToString() + " seconds";

                l_tb.Text = tb.ToString() + " seconds";

                l_tlf.Text = tlf.ToString() + " seconds";

                l_TOF.Text = TOF.ToString() + " seconds";
            }
        }

        private void num_d_Beschleunigung_ValueChanged(object sender, EventArgs e)
        {
            if(num_d_acceleration_cm.Value == 0)
            {
                num_Pot2_V.Value = 0;
            }
        }

        private void num_Pot1_V_ValueChanged(object sender, EventArgs e)
        {
            while(num_Pot1_V.Value <= num_Pot2_V.Value)
            {
                num_Pot2_V.Value -= 1;
            }
        }

        private void num_Pot2_V_ValueChanged(object sender, EventArgs e)
        {
            while (num_Pot1_V.Value <= num_Pot2_V.Value)
            {
                num_Pot1_V.Value += 1;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            for(int i = 0; i<Constants.max_elements; i++)
            {
                if(comboBoxElemente.Text == array2D_Elements[0,i])
                {
                    num_mass_u.Value = (decimal)float.Parse(array2D_Elements[1, i]);
                }
            }
        }

        private void b_help_Click(object sender, EventArgs e)
        {
            new Form_help().ShowDialog();
        }
    }
}
