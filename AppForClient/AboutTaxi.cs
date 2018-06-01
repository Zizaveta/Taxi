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
            Taxies = Form1.PersonWork.GetTaxies();
            Show(Taxies[i]);
        }
        public AboutTaxi(Taxi t):this()
        {
            button2.Visible = false;
            button3.Visible = false;
            Show(t);
        }
        public void Show(Taxi taxi)
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
        private void About_Click(object sender, EventArgs e)
        {
            About_car w = new About_car(i);
            w.ShowDialog();
        }

        private void Prev_Click(object sender, EventArgs e)
        {
            if (i - 1 >= 0)
                i--;
            Show(Taxies[i]);
        }

        private void Next_Click(object sender, EventArgs e)
        {
            if (i + 1 < Taxies.Count)
                i++;
            Show(Taxies[i]);
        }

     
    }
}
