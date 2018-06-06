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
    public partial class About_car : Form
    {
        List<Car> cars;
        int i = 0;
        public About_car()
        {
            InitializeComponent();
            try
            {
                cars = Form1.PersonWork.AllCars();
                Show(cars[i]);
            }
            catch { }
        }
        public About_car(int car) : this()
        {
            try
            {
                Show(cars[i]);
                button2.Visible = false;
                button1.Visible = false;
            }
            catch { }
        }

        public void Show(Car car)
        {
            try
            {
                pictureBox1.Image = null;
                textBox1.Text = car.Marka;
                textBox2.Text = car.ClassOfCar;
                if (car.ForPassagers == true)
                {
                    checkBox1.CheckState = CheckState.Checked;
                    label3.Text = "Max number of passagers:";
                    numericUpDown1.Value = car.MaxNumberOfPassagerOrVantazhKG;
                }
                else
                {
                    checkBox1.CheckState = CheckState.Unchecked;
                    label3.Text = "Load capacity:";
                    numericUpDown1.Value = car.MaxNumberOfPassagerOrVantazhKG;
                }
                try
                {
                    MemoryStream ms = new MemoryStream(car.Image, 0, car.Image.Length);
                    ms.Write(car.Image, 0, car.Image.Length);
                    pictureBox1.Image = Image.FromStream(ms, true);//Exception occurs here
                }
                catch { pictureBox1.Image = null; }
            }
            catch { }
        }

        private void Right_Click(object sender, EventArgs e)
        {
            try
            {
                if (i + 1 < cars.Count)
                    i++;
                Show(cars[i]);
            }
            catch { }
        }

        private void Left_Click(object sender, EventArgs e)
        {
            try
            {
                if (i - 1 >= 0)
                    i--;
                Show(cars[i]);
            }
            catch { }
        }
    }
}
