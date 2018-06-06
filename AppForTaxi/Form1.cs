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
using BLL;
namespace AppForTaxi
{
    public partial class Form1 : Form
    {
        public static TaxiWork WorkTaxi;
        public Form1()
        {
            InitializeComponent();
            WorkTaxi = new TaxiWork();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (WorkTaxi.Authoriz(textBox1.Text, textBox2.Text) == true)
                {
                    MainTaxiWindow w = new MainTaxiWindow();
                    w.ShowDialog();
                }
                else MessageBox.Show("Wrong email or password");
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Registr w = new Registr();
                w.ShowDialog();
            }
            catch { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Length == 0)
                    MessageBox.Show("Input email");
                else
                {
                    MessageBox.Show(WorkTaxi.ForgetPassword(textBox1.Text));
                }
            }
            catch { }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                WorkTaxi.SingOut();
            }
            catch { }
        }
    }
}
