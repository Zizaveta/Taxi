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
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace AppForClient
{
    public partial class ShowOrders : Form
    {

        List<Order> ListOfOrder;
        int i;
        public ShowOrders()
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
                //  map.SetPositionByKeywords("Rivne");



                comboBox1.Items.Add("Wait");
                comboBox1.Items.Add("In work");
                comboBox1.Items.Add("Closed");
                comboBox1.Items.Add("Cansel");
                ListOfOrder = new List<Order>();
                ListOfOrder = Form1.PersonWork.MyOrders();
               // MessageBox.Show(ListOfOrder.Count.ToString());

                i = 0;
                ShowMyOrder();
            }
            catch { }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (i - 1 >= 0)
                    i--;
                ShowMyOrder();
            }
            catch { }
        }

        public void SetMap()
        {
            try
            {
                //map = new GMapControl();
                // map.Overlays.Clear();
                //map.SetPositionByKeywords("Rivne");

                textBox1.Text = ListOfOrder[i].LatFrom.ToString();
                textBox2.Text = ListOfOrder[i].LongtFrom.ToString();

                textBox3.Text = ListOfOrder[i].LatTo.ToString();
                textBox4.Text = ListOfOrder[i].LongtTo.ToString();

                PointLatLng point = new PointLatLng(ListOfOrder[i].LatFrom, ListOfOrder[i].LongtFrom);
                GMapMarker marker = new GMarkerGoogle(point, GMarkerGoogleType.red_pushpin);

                
                map.Position = new PointLatLng(ListOfOrder[i].LatFrom, ListOfOrder[i].LongtFrom);
                PointLatLng point2 = new PointLatLng(ListOfOrder[i].LatTo, ListOfOrder[i].LongtTo);
                GMapMarker marker2 = new GMarkerGoogle(point2, GMarkerGoogleType.red_pushpin);

                //MessageBox.Show(ListOfOrder[i].LatFrom + "; " + ListOfOrder[i].LongtFrom + " : " + ListOfOrder[i].LatTo + "; " + ListOfOrder[i].LongtTo);

                GMapOverlay markers = new GMapOverlay("markers");
                markers.Markers.Add(marker);
             //   markers.Markers.Add(marker2);

                map.Overlays.Add(markers);

                //var route = GoogleMapProvider.Instance.GetRoute(point, point2, true, true, 50);
                //var r = new GMapRoute(route.Points, "My route");
                //var routes = new GMapOverlay("routes");
                //routes.Routes.Add(r);
                //map.Overlays.Add(routes);
            }
            catch {  }
        }
        public void ShowMyOrder()
        {
            try
            {
                comboBox1.SelectedItem = ListOfOrder[i].StateOfOrder;
                textBox5.Text = ListOfOrder[i].Coment;
                comboBox1.Enabled = false;

                //   PointLatLng point1 = new PointLatLng(ListOfOrder[i].LongtFrom, ListOfOrder[i].LatFrom);
                //  PointLatLng point2 = new PointLatLng( ListOfOrder[i].LongtTo, ListOfOrder[i].LatTo);

                //    map.Position = new GMap.NET.PointLatLng(ListOfOrder[i].LatFrom, ListOfOrder[i].LongtFrom);
                //  map.Position = new GMap.NET.PointLatLng(ListOfOrder[i].LatTo, ListOfOrder[i].LongtTo);



                //GMapOverlay markers = new GMapOverlay("markers");
                // markers.Markers.Add(new GMarkerGoogle(point1, GMarkerGoogleType.red_pushpin));
                // markers.Markers.Add(new GMarkerGoogle(point2, GMarkerGoogleType.pink_pushpin));
                //map.Overlays.Add(markers);


                //var route = GoogleMapProvider.Instance.GetRoute(point1, point2, false, false, 14);
                //var r = new GMapRoute(route.Points, "My route");
                //var routes = new GMapOverlay("routes");
                //routes.Routes.Add(r);
                //map.Overlays.Add(routes);


                SetMap();
            }
            catch { }
        }
        private void Check_Change(object sender, EventArgs e)
        {
            try
            {
                ListOfOrder = new List<Order>();
                if (checkBox1.CheckState == CheckState.Checked)
                {
                    ListOfOrder.AddRange(Form1.PersonWork.MyOrders().Where(elem => elem.StateOfOrder == "Wait"));
                }
                if (checkBox2.CheckState == CheckState.Checked)
                {
                    ListOfOrder.AddRange(Form1.PersonWork.MyOrders().Where(elem => elem.StateOfOrder == "In work"));
                }
                if (checkBox3.CheckState == CheckState.Checked)
                {
                    ListOfOrder.AddRange(Form1.PersonWork.MyOrders().Where(elem => elem.StateOfOrder == "Closed"));
                }
                if (checkBox2.CheckState == CheckState.Checked)
                {
                    ListOfOrder.AddRange(Form1.PersonWork.MyOrders().Where(elem => elem.StateOfOrder == "Cansel"));
                }
                if (ListOfOrder.Count == 0)
                    MessageBox.Show("Empty list");
                i = 0;
                ShowMyOrder();
            }
            catch  { }
        }

      

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (i + 1 < ListOfOrder.Count)
                    i++;
                ShowMyOrder();
            }
            catch { }
        }

        private void ShowOrders_Load(object sender, EventArgs e)
        {

        }
    }
}

