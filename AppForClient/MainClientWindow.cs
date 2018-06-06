using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DAL;
using System.IO;
using GMap.NET.MapProviders;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace AppForClient
{
    public partial class MainClientWindow : Form
    {
        public MainClientWindow()
        {
            InitializeComponent();
            try
            {
                textBox1.Text = Form1.PersonWork.AboutMe().PersonName;
                try
                {
                    MemoryStream ms = new MemoryStream(Form1.PersonWork.AboutMe().Image, 0, Form1.PersonWork.AboutMe().Image.Length);
                    ms.Write(Form1.PersonWork.AboutMe().Image, 0, Form1.PersonWork.AboutMe().Image.Length);
                    pictureBox1.Image = Image.FromStream(ms, true);//Exception occurs here
                }
                catch { pictureBox1.Image = null; }
                ShowMap();
            }
            catch { }
        }
        public void ShowMap()
        {
            try
            {
                map.ShowCenter = false;
                map.DragButton = MouseButtons.Left;
                map.MapProvider = GMapProviders.BingMap;
                map.MinZoom = 5;
                map.MaxZoom = 500;
                map.Zoom = 10;
                map.SetPositionByKeywords("Rivne");
                map.Overlays.Clear();

                GMapOverlay markers = new GMapOverlay("markers");

                foreach (Taxi elem in Form1.PersonWork.GetTaxies().Where(elem => elem.Lat != 0 && elem.Longt != 0))
                {
                    map.Position = new GMap.NET.PointLatLng(elem.Lat, elem.Longt);
                    PointLatLng point = new PointLatLng(elem.Lat, elem.Longt);
                    GMapMarker marker = new GMarkerGoogle(point, GMarkerGoogleType.red_pushpin);
                    markers.Markers.Add(marker);
                }
                map.Overlays.Add(markers);
            }
            catch { }
        }
        private void newOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AddNewOrder w = new AddNewOrder();
                w.ShowDialog();
            }
            catch { }
        }

        private void showOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ShowOrders w = new ShowOrders();
                w.ShowDialog();
            }
            catch { }
        }

        private void showCarsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                About_car w = new About_car();
                w.ShowDialog();
            }
            catch { }
        }

        private void showTaxiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AboutTaxi w = new AboutTaxi();
                w.ShowDialog();
            }
            catch { }
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Form1.PersonWork.SingOut();
                this.Close();
            }
            catch { }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog o = new OpenFileDialog();
                if (o.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(o.FileName);
                    byte[] buff = new byte[255];
                    if (pictureBox1.Image != null)
                    {
                        MemoryStream ms = new MemoryStream();
                        pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        buff = ms.GetBuffer();
                    }
                    if (Form1.PersonWork.ChangeImage(buff) == false)
                        MessageBox.Show("Image not save");
                }
            }
            catch { }
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ChangeMyPassword c = new ChangeMyPassword();
                c.ShowDialog();
            }
            catch { }
        }
    }
}
