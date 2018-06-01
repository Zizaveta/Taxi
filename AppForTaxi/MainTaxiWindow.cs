using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
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

namespace AppForTaxi
{
    public partial class MainTaxiWindow : Form
    {
        GMapOverlay markers;
        public MainTaxiWindow()
        {
            InitializeComponent();
            map.ShowCenter = false;
            map.DragButton = MouseButtons.Left;
            map.MapProvider = GMapProviders.BingMap;
            map.MinZoom = 5;
            map.MaxZoom = 500;
            map.Zoom = 10;
            map.SetPositionByKeywords("Rivne");

            textBox1.Text = Form1.WorkTaxi.AboutMe().DriverName;
            try
            {
                MemoryStream ms = new MemoryStream(Form1.WorkTaxi.AboutMe().Image, 0, Form1.WorkTaxi.AboutMe().Image.Length);
                ms.Write(Form1.WorkTaxi.AboutMe().Image, 0, Form1.WorkTaxi.AboutMe().Image.Length);
                pictureBox1.Image = Image.FromStream(ms, true);//Exception occurs here
            }
            catch { pictureBox1.Image = null; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            Form1.WorkTaxi.Place(double.Parse(textBox3.Text),double.Parse(textBox2.Text));

            map.Overlays.Clear();

            map.Position = new GMap.NET.PointLatLng(double.Parse(textBox3.Text), double.Parse(textBox2.Text));
            PointLatLng point = new PointLatLng(double.Parse(textBox3.Text), double.Parse(textBox2.Text));
            GMapMarker marker = new GMarkerGoogle(point, GMarkerGoogleType.red_pushpin);

            markers = new GMapOverlay("markers");
            
            markers.Markers.Add(marker);
            map.Overlays.Add(markers);
        }

        private void ordersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyOders w = new MyOders();
            w.ShowDialog();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1.WorkTaxi.SingOut();
            this.Close();

        }

        private void aboutMyCarToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // MessageBox.Show(Form1.WorkTaxi.taxi.Car.Id.ToString());

            MyCar w = new MyCar(1);
            w.ShowDialog();
        }

        private void map_MouseClick(object sender, MouseEventArgs e)
        {
            textBox3.Text = map.FromLocalToLatLng(e.X, e.Y).Lat.ToString();
            textBox2.Text = map.FromLocalToLatLng(e.X, e.Y).Lng.ToString();

            map.Overlays.Clear();
            map.Position = new GMap.NET.PointLatLng(double.Parse(textBox3.Text), double.Parse(textBox2.Text));
            PointLatLng point = new PointLatLng(double.Parse(textBox3.Text), double.Parse(textBox2.Text));
            GMapMarker marker = new GMarkerGoogle(point, GMarkerGoogleType.red_pushpin);

            markers = new GMapOverlay("markers");

            markers.Markers.Add(marker);
            map.Overlays.Add(markers);
        }
    }
}
