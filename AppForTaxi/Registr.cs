using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
namespace AppForTaxi
{
    public partial class Registr : Form
    {
        public Registr()
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
        private void B_Click(object sender, EventArgs e)
        {
            try
            {
                MyCar w = new MyCar();
                w.ShowDialog();
                RefreshCars();
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] buff = new byte[255];
                if (pictureBox1.Image != null)
                {
                    MemoryStream ms = new MemoryStream();
                    pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    buff = ms.GetBuffer();
                }
                string str = Form1.WorkTaxi.CreateNewTaxi(new Taxi() { Email = textBox1.Text, DriverName = textBox2.Text, Password = textBox3.Text, Image = buff }, Form1.WorkTaxi.AllCars()[comboBox1.SelectedIndex].Id);
                if (str == "Taxi is add")
                    this.Close();
                else MessageBox.Show(str);
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog o = new OpenFileDialog();
                if (o.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(o.FileName);
                }
            }
            catch { }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                 MyCar c = new MyCar(Form1.WorkTaxi.AllCars()[comboBox1.SelectedIndex].Marka, Form1.WorkTaxi.AllCars()[comboBox1.SelectedIndex].ClassOfCar, Form1.WorkTaxi.AllCars()[comboBox1.SelectedIndex].ForPassagers, Form1.WorkTaxi.AllCars()[comboBox1.SelectedIndex].MaxNumberOfPassagerOrVantazhKG, Form1.WorkTaxi.AllCars()[comboBox1.SelectedIndex].Image);
                  c.ShowDialog();
            }
            catch { }
        }
    }
}
