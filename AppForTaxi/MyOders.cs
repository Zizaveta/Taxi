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
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.MapProviders;

namespace AppForTaxi
{
    public partial class MyOders : Form
    {
        List<Order> ListOfOrder;
        int i;
        public MyOders()
        {
            InitializeComponent();

            map.ShowCenter = false;
            map.DragButton = MouseButtons.Left;
            map.MapProvider = GMapProviders.BingMap;
            map.MinZoom = 5;
            map.MaxZoom = 500;
            map.Zoom = 10;
            map.SetPositionByKeywords("Rivne");

            comboBox1.Items.Add("Wait");
            comboBox1.Items.Add("In work");
            comboBox1.Items.Add("Closed");
            comboBox1.Items.Add("Cansel");
            ListOfOrder = new List<Order>();
            ListOfOrder = Form1.WorkTaxi.MyOrders();
            i = 0;
            ShowOrder();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (i - 1 >= 0)
                    i--;
                ShowOrder();
            }
            catch { }
        }
        public void ShowOrder()
        {

            map.Overlays.Clear();

            comboBox1.Items.Clear();
            textBox5.Text = ListOfOrder[i].Coment;
            if(ListOfOrder[i].StateOfOrder == "Closed" || ListOfOrder[i].StateOfOrder == "Cansel")
            {
                comboBox1.Enabled = false;          
            }
            else
            {
                comboBox1.Enabled = true;
                if (ListOfOrder[i].StateOfOrder == "Wait")
                {
                    comboBox1.Items.Add("Wait");
                    comboBox1.Items.Add("In work");
                }
                if (ListOfOrder[i].StateOfOrder == "In work")
                    comboBox1.Items.Add("In work");

            }
            comboBox1.Items.Add("Closed");
            comboBox1.Items.Add("Cansel");
            comboBox1.SelectedItem = ListOfOrder[i].StateOfOrder;

            map.Position = new PointLatLng(ListOfOrder[i].LatFrom, ListOfOrder[i].LongtFrom);
            PointLatLng point = new PointLatLng(ListOfOrder[i].LatFrom, ListOfOrder[i].LongtFrom);
            GMapMarker marker = new GMarkerGoogle(point, GMarkerGoogleType.red_pushpin);

            //map.Position = new PointLatLng(ListOfOrder[i].LatTo, ListOfOrder[i].LongtTo);
            PointLatLng point2 = new PointLatLng(ListOfOrder[i].LatTo, ListOfOrder[i].LongtTo);
            GMapMarker marker2 = new GMarkerGoogle(point2, GMarkerGoogleType.red_pushpin);

            GMapOverlay markers = new GMapOverlay("markers");
            markers.Markers.Add(marker);
            markers.Markers.Add(marker2);

            map.Overlays.Add(markers);

            var route = GoogleMapProvider.Instance.GetRoute(point, point2, false, false, 50);
            var r = new GMapRoute(route.Points, "My route");
            var routes = new GMapOverlay("routes");
            routes.Routes.Add(r);
            map.Overlays.Add(routes);



        }
        private void Check_Change(object sender, EventArgs e)
        {
            try
            {
                ListOfOrder = new List<Order>();
                if (checkBox1.CheckState == CheckState.Checked)
                {
                    ListOfOrder.AddRange(Form1.WorkTaxi.MyOrders().Where(elem => elem.StateOfOrder == "Wait"));
                }
                if (checkBox2.CheckState == CheckState.Checked)
                {
                    ListOfOrder.AddRange(Form1.WorkTaxi.MyOrders().Where(elem => elem.StateOfOrder == "In work"));
                }
                if (checkBox3.CheckState == CheckState.Checked)
                {
                    ListOfOrder.AddRange(Form1.WorkTaxi.MyOrders().Where(elem => elem.StateOfOrder == "Closed"));
                }
                if (checkBox2.CheckState == CheckState.Checked)
                {
                    ListOfOrder.AddRange(Form1.WorkTaxi.MyOrders().Where(elem => elem.StateOfOrder == "Cansel"));
                }
                if (ListOfOrder.Count == 0)
                    MessageBox.Show("Empty list");
                i = 0;
                ShowOrder();
            }
            catch { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show(Form1.WorkTaxi.ChangeStateOfOrder(ListOfOrder[i].Id, comboBox1.SelectedItem.ToString(), textBox5.Text));
            }
            catch { }
            }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (i + 1 < ListOfOrder.Count)
                    i++;
                ShowOrder();
            }
            catch { }
        }
    }
}
