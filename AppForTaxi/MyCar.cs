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
using System.IO;

namespace AppForTaxi
{
    public partial class MyCar : Form
    {
        public MyCar()
        {
            InitializeComponent();
            comboBox1.Items.Add("Econom");
            comboBox1.Items.Add("Premium");
            comboBox1.Items.Add("Middle");
            comboBox1.Items.Add("Bisness");
        }
        public MyCar(int carid)
        {
            Car car = Form1.WorkTaxi.AllCars().First();
            button1.Visible = false;
            textBox1.Text = car.Marka;
            comboBox1.Text = car.ClassOfCar;
            if (car.ForPassagers == true)
            {
                checkBox1.CheckState = CheckState.Checked;
                label3.Text = "Max number of passagers:";
                numericUpDown1.Value = car.MaxNumberOfPassagerOrVantazhKG;
            }
            else
            {
                checkBox1.CheckState = CheckState.Unchecked;
                label3.Text = "Max number of KG:";
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


        private void button1_Click(object sender, EventArgs e)
        {
            byte[] buff = new byte[255];
            if (pictureBox1.Image != null)
            {
                MemoryStream ms = new MemoryStream();
                pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                buff = ms.GetBuffer();
            }
            string str = Form1.WorkTaxi.CreateCar(new Car() { Marka = textBox1.Text, ClassOfCar = comboBox1.SelectedItem.ToString(), ForPassagers = checkBox1.CheckState == CheckState.Checked ? true : false, MaxNumberOfPassagerOrVantazhKG = (int)numericUpDown1.Value, Image = buff });
            if (str == "")
                this.Close();
            else MessageBox.Show(str);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            if(o.ShowDialog()==DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(o.FileName);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
                checkBox1.Text = "For passagers";
            else checkBox1.Text = "For vantazh";
        }
    }
}
