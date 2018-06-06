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
    public partial class ChangeMyCar : Form
    {
        public ChangeMyCar()
        {
            InitializeComponent();
            RefreshCars();
        }
        public void RefreshCars()
        {
            try
            {
                comboBox1.Items.Clear();
                foreach (Car elem in Form1.WorkTaxi.AllCars())
                {
                    comboBox1.Items.Add(elem.Marka);
                }
            }
            catch { }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
             //  MessageBox.Show(Form1.WorkTaxi.AllCars()[comboBox1.SelectedIndex].Marka + "+" + Form1.WorkTaxi.AllCars()[comboBox1.SelectedIndex].ClassOfCar + "+" + Form1.WorkTaxi.AllCars()[comboBox1.SelectedIndex].ForPassagers + "+" + Form1.WorkTaxi.AllCars()[comboBox1.SelectedIndex].MaxNumberOfPassagerOrVantazhKG + "+" +  Form1.WorkTaxi.AllCars()[comboBox1.SelectedIndex].Image);

                 MyCar c = new MyCar(Form1.WorkTaxi.AllCars()[comboBox1.SelectedIndex].Marka, Form1.WorkTaxi.AllCars()[comboBox1.SelectedIndex].ClassOfCar, Form1.WorkTaxi.AllCars()[comboBox1.SelectedIndex].ForPassagers, Form1.WorkTaxi.AllCars()[comboBox1.SelectedIndex].MaxNumberOfPassagerOrVantazhKG, Form1.WorkTaxi.AllCars()[comboBox1.SelectedIndex].Image);
                 c.ShowDialog();
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                MyCar w = new MyCar();
                w.ShowDialog();
                RefreshCars();
            }
            catch { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if(comboBox1.SelectedIndex != -1)
                {
                    if (Form1.WorkTaxi.ChangeCar(Form1.WorkTaxi.AllCars()[comboBox1.SelectedIndex].Id) == true)
                        this.Close();
                    else MessageBox.Show("Error");
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
