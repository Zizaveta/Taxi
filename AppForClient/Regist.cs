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

namespace AppForClient
{
    public partial class Regist : Form
    {
        public Regist()
        {
            InitializeComponent();
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
                string str = Form1.PersonWork.CreateNewPerson(textBox2.Text, textBox1.Text, textBox3.Text, buff);
                if (str == "")
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
    }
}
