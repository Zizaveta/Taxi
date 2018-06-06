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
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET;
using GMap.NET.WindowsForms.Markers;

namespace AppForClient
{
    public partial class AddNewOrder : Form
    {
        PointLatLng point1;
        PointLatLng point2;
        GMapOverlay markers;
        public AddNewOrder()
        {
            InitializeComponent();
            try
            {
                map.ShowCenter = false;
                map.DragButton = MouseButtons.Left;
                map.MapProvider = GMapProviders.BingMap;
                map.MinZoom = 5;
                map.MaxZoom = 500;
                map.Zoom = 10;
                map.SetPositionByKeywords("Rivne");

                foreach (Taxi elem in Form1.PersonWork.GetTaxies())
                {
                    comboBox1.Items.Add(elem.DriverName);
                }
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string str = Form1.PersonWork.MakeOrder(Form1.PersonWork.GetTaxies()[comboBox1.SelectedIndex].Id, double.Parse(lfrom.Text), double.Parse(lafrom.Text), double.Parse(lto.Text), double.Parse(lato.Text), coment.Text);
                if (str == "")
                    this.Close();
                else MessageBox.Show(str);
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                AboutTaxi w = new AboutTaxi(Form1.PersonWork.GetTaxies()[comboBox1.SelectedIndex]);
                w.ShowDialog();
            }
            catch { }
        }

        private void cityfrom_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    lfrom_Leave(null, null);
                }
            }
            catch { }
        }

        private void to_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    lto_Leave(null, null);
                }


                if (point1 != null && point2 != null)
                {
                    //   map.Overlays.Clear();
                    var route = GoogleMapProvider.Instance.GetRoute(point1, point2, true, true, 14);
                    var r = new GMapRoute(route.Points, "My route");
                    var routes = new GMapOverlay("routes");
                    routes.Routes.Add(r);
                    map.Overlays.Add(routes);
                }
            }
            catch { }
        }

        private void lfrom_Leave(object sender, EventArgs e)
        {
            try
            {
                double f, s;
                if (double.TryParse(lfrom.Text, out f) == true && double.TryParse(lafrom.Text, out s) == true)
                {
                    map.Position = new GMap.NET.PointLatLng(f, s);
                    point1 = new PointLatLng(f, s);
                    GMapMarker marker1 = new GMarkerGoogle(point1, GMarkerGoogleType.red_pushpin);
                    map.Overlays.Clear();
                    markers = new GMapOverlay("markers");
                    markers.Markers.Add(marker1);
                    if (point2 != null)
                        markers.Markers.Add(new GMarkerGoogle(point2, GMarkerGoogleType.pink_pushpin));
                    map.Overlays.Add(markers);
                }

                if (point1 != null && point2 != null)
                {
                    //   map.Overlays.Clear();
                    var route = GoogleMapProvider.Instance.GetRoute(point1, point2, true, true, 14);
                    var r = new GMapRoute(route.Points, "My route");
                    var routes = new GMapOverlay("routes");
                    routes.Routes.Add(r);
                    map.Overlays.Add(routes);
                }
            }
            catch { }
        }

        private void lto_Leave(object sender, EventArgs e)
        {
            try
            {
                double f, s;
                if (double.TryParse(lto.Text, out f) == true && double.TryParse(lato.Text, out s) == true)
                {
                    map.Overlays.Clear();
                    map.Position = new GMap.NET.PointLatLng(f, s);
                    point2 = new PointLatLng(f, s);
                    GMapMarker marker2 = new GMarkerGoogle(point2, GMarkerGoogleType.red_pushpin);

                    markers = new GMapOverlay("markers");
                    markers.Markers.Add(marker2);
                    if (point1 != null)
                        markers.Markers.Add(new GMarkerGoogle(point1, GMarkerGoogleType.pink_pushpin));
                    map.Overlays.Add(markers);
                }
            }
            catch { }
        }

        private void map_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    lfrom.Text = map.FromLocalToLatLng(e.X, e.Y).Lat.ToString();
                    lafrom.Text = map.FromLocalToLatLng(e.X, e.Y).Lng.ToString();
                    lfrom_Leave(null, null);
                }
                if(e.Button == MouseButtons.Right)
                {
                    lto.Text = map.FromLocalToLatLng(e.X, e.Y).Lat.ToString();
                    lato.Text = map.FromLocalToLatLng(e.X, e.Y).Lng.ToString();
                    lto_Leave(null, null);
                }
            }
            catch { }
        }
    }
}
