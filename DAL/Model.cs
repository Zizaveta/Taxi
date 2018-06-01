namespace DAL
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Data.Entity;
	using System.Linq;
	using System.IO;

	public class TaxiModel : DbContext
	{
		public TaxiModel()
			: base("name=Model")
		{
		}

		public virtual DbSet<Car> Cars { get; set; }
		public virtual DbSet<Taxi> Taxies { get; set; }
		public virtual DbSet<Person> Persons { get; set; }
		public virtual DbSet<Order> Orders { get; set; }
	}
	public class Car
	{
		public Car()
		{
			Taxies = new List<Taxi>();
		}
		public int Id { get; set; }
		[Required]
		public string Marka { get; set; }
		public byte[] Image { get; set; }
		public int MaxNumberOfPassagerOrVantazhKG { get; set; }
		public bool ForPassagers { get; set; } 
		public string ClassOfCar { get; set; }  // econom, bissness, premium

		public virtual ICollection<Taxi> Taxies { get; set; }
	}

	public class Taxi
	{

		public Taxi()
		{
			Orders = new List<Order>();
		}
		public int Id { get; set; }
		public string DriverName { get; set; }
		public byte[] Image { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
        public double Lat { get; set; }
        public double Longt { get; set; }
		public virtual ICollection<Order> Orders { get; set; }
		public virtual Car Car { get; set; }

	}

	public class Person
	{
		public Person()
		{
			Orders = new List<Order>();
		}
		public int Id { get; set; }
        [Required]
		public string PersonName { get; set; }
		public byte[] Image { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public virtual ICollection<Order> Orders { get; set; }
	}

	public class Order
	{
		public int Id { get; set; }
		[Required]
		public virtual Taxi Taxi { get; set; }
		[Required]
		public virtual Person Person { get; set; }
        public double LatFrom { get; set; }
        public double LongtFrom { get; set; }
        public double LatTo { get; set; }
        public double LongtTo { get; set; }
        public double Price { get; set; }
		public string StateOfOrder { get; set; } // Ok, cansel, in work
		public string Coment { get; set; }
	}
	public class Log
	{
		public static void Logger(string m)
		{
			using (StreamWriter s = new StreamWriter(@"Exeptions.txt", true))
			{
				s.WriteLine(m);
			}
		}
	}
}