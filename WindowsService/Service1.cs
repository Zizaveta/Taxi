using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BLL;
namespace WindowsService
{
	public partial class Service1 : ServiceBase
	{
		public Service1()
		{
			InitializeComponent();
		}
		ServiceHost sh;
		ServiceHost sh2;
		protected override void OnStart(string[] args)
		{
			try
			{
				sh = new ServiceHost(typeof(WorkPerson));
			    sh2 = new ServiceHost(typeof(TaxiWork));
			
				sh.Open();
				sh2.Open();
				Log.Logger("Hosts start working");
			}
			catch(Exception ex)
			{
				Log.Logger(ex.Message);
			}
		}

		protected override void OnStop()
		{
			if (sh != null)
				sh.Close();

			if (sh2 != null)
				sh2.Close();
			Log.Logger("Hosts finish work");

		}
	}
}
