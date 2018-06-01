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
        public void ShowMap()
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

            foreach (Taxi elem in Form1.PersonWork.GetTaxies().Where(elem => elem.Lat!=0 && elem.Longt!=0))
            {
                map.Position = new GMap.NET.PointLatLng(elem.Lat, elem.Longt);
                PointLatLng point = new PointLatLng(elem.Lat, elem.Longt);
                GMapMarker marker = new GMarkerGoogle(point, GMarkerGoogleType.red_pushpin);
                markers.Markers.Add(marker);
            }
            map.Overlays.Add(markers);
        }
        private void newOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewOrder w = new AddNewOrder();
            w.ShowDialog();
        }

        private void showOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowOrders w = new ShowOrders();
            w.ShowDialog();
        }

        private void showCarsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About_car w = new About_car();
            w.ShowDialog();
        }

        private void showTaxiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutTaxi w = new AboutTaxi();
            w.ShowDialog();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1.PersonWork.SingOut();
            this.Close();
        }


    }
}
