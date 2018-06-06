using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using System.IO;

namespace AppForClient
{
    public partial class AboutTaxi : Form
    {
        public List<Taxi> Taxies;
        int i = 0;
        public AboutTaxi()
        {
            InitializeComponent();
            try
            {
                Taxies = Form1.PersonWork.GetTaxies();
                Show(Taxies[i]);
            }
            catch { }
        }
        public AboutTaxi(Taxi t):this()
        {
            try
            {
                button2.Visible = false;
                button3.Visible = false;
                Show(t);
            }
            catch { }
        }
        public void Show(Taxi taxi)
        {
            try
            {
                textBox1.Text = taxi.DriverName;
                pictureBox1.Image = null;
                try
                {
                    MemoryStream ms = new MemoryStream(taxi.Image, 0, taxi.Image.Length);
                    ms.Write(taxi.Image, 0, taxi.Image.Length);
                    pictureBox1.Image = Image.FromStream(ms, true);//Exception occurs here
                }
                catch { pictureBox1.Image = null; }
            }
            catch { }
        }
        private void About_Click(object sender, EventArgs e)
        {
            try
            {
                About_car w = new About_car(i);
                w.ShowDialog();
            }
            catch { }
        }

        private void Prev_Click(object sender, EventArgs e)
        {
            try
            {
                if (i - 1 >= 0)
                    i--;
                Show(Taxies[i]);
            }
            catch { }
        }

        private void Next_Click(object sender, EventArgs e)
        {
            try
            {
                if (i + 1 < Taxies.Count)
                    i++;
                Show(Taxies[i]);
            }
            catch { }
        }

     
    }
}
