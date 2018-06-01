using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Net.Mail;
using System.ServiceModel;
using System.Windows.Forms;

namespace BLL
{
    public class WorkPerson 
    {
        private Person person { get; set; }
		public List<Taxi> GetTaxies()
		{
			try
			{
				using (TaxiModel db = new TaxiModel())
				{
					return db.Taxies.ToList();
				}
			}
			catch(Exception ex)
			{
				Log.Logger(ex.Message);
				return null;
			}
		}
		public List<Order> MyOrders()
		{
			try
			{
                using (TaxiModel db = new TaxiModel())
                {
                    return db.Orders.Where(elem => elem.Person.Id == person.Id).ToList();
                }
			}
			catch(Exception ex)
			{
				Log.Logger(ex.Message);
				return null;
			}
		}
		public List<Car> AllCars()
		{
			try
			{
				using (TaxiModel db = new TaxiModel())
				{
					return db.Cars.ToList();
				}
			}
			catch(Exception ex)
			{
				Log.Logger(ex.Message);
				return null;
			}
		}
		public string MakeOrder(int idTaxi,double fromLongt, double fromlat, double toLongt, double tolat, string comment = null)
		{
			try
			{
				using (TaxiModel db = new TaxiModel())
				{
					db.Orders.Add(new Order() { Taxi = db.Taxies.First(elem =>elem.Id==idTaxi), Person = db.Persons.First(elem => elem.Id == person.Id), LatFrom = fromlat, LatTo = fromLongt, LongtFrom= tolat, LongtTo=toLongt, Coment = comment, StateOfOrder = "Wait" });
					db.SaveChanges();
                    Authoriz(person.Email, person.Password);
					return "";
				}
			}
			catch(Exception ex)
			{
				Log.Logger(ex.Message);
				return "Something wrong. Order is not add";
			}
		}
		public string ChangeStateOfOrder(int orderid, string state)
		{
			try
			{
				using (TaxiModel db = new TaxiModel())
				{
					db.Orders.First(elem => elem.Id == orderid).StateOfOrder = state;
					db.SaveChanges();
                    return "State of order is change";
				}
			}
			catch(Exception ex)
			{
				Log.Logger(ex.Message);
				return "Something wrong. You can't change state of order";
			}
		}
		public string CreateNewPerson(string Name, string Email, string Password, byte[] img = null)
		{
			try
			{
				using (TaxiModel db = new TaxiModel())
				{
                    if (Password.Length < 8)
                        return "Small password";
                    if (!Email.Contains("@") || !Email.Contains(".") || Email.Length < 8)
                        return "Wrong email";
                    if (Name.Length == 0)
                        return "Input your name";
						if (db.Persons.FirstOrDefault(elem => elem.Email == Email) != null)
							return "This email is in db";
						db.Persons.Add(new Person() {  Email=Email, PersonName = Name, Image = img, Password=Password });
						db.SaveChanges();
						return "";
				}
			}
			catch(Exception ex)
			{
				Log.Logger(ex.Message);
				return "Error. Peson is not add";
			}
		}
		public bool Authoriz(string email, string passw)
		{
			try
			{
				using (TaxiModel db = new TaxiModel())
				{
					person = db.Persons.FirstOrDefault(elem => elem.Email == email && elem.Password == passw);
                    if (person != null)
                    {
                        return true;
                    }
                    return false;
				}
			}
			catch(Exception ex)
			{
				Log.Logger(ex.Message);
                return false;
			}
		}
		public bool WriteEmail(string email, string Thema, string Message)
		{
			try
			{
                MailMessage m = new MailMessage(new MailAddress(person.Email, person.PersonName), new MailAddress(email));
				m.Subject = Thema;
				m.Body = Message;
				SmtpClient smtp = new SmtpClient("aspmx.l.google.com", 25);
				//smtp.Credentials = new NetworkCredential();
				smtp.EnableSsl = true;
				smtp.Send(m);
				return true;
			}
			catch (Exception ex)
			{
				Log.Logger(ex.Message);
				return false;
			}
		}
		public string ForgetPassword(string email)
		{
			try
			{	
				string tmp = "";
				using (TaxiModel db = new TaxiModel())
				{
					tmp = db.Persons.First(elem => elem.Email == email).Password;
				}
                if (WriteEmail(email, "Taxi: Forget password", "Your password on service taxi: " + tmp) == true)
                    return "Password is send on your email";
                else return "Message is not send on your email";
			}
			catch(Exception ex)
			{
				Log.Logger(ex.Message);
				return "Something wrong. Message is not receive";
			}
		}
        public void SingOut()
        {
            try
            {
                person = null;
            }
            catch(Exception ex)
            {
                Log.Logger(ex.Message);
            }
        }
        public Person AboutMe()
        {
            try
            {
                return person;
            }
            catch(Exception ex)
            {
                Log.Logger(ex.Message);
                return null;
            }
        }

    }


    public class TaxiWork 
	{
        private Taxi taxi { get; set; }
		public List<Order> MyOrders()
		{
			try
			{
                using (TaxiModel db = new TaxiModel())
                {
                    return db.Orders.Where(elem => elem.Taxi.Id == taxi.Id).ToList();
                }
			}
			catch (Exception ex)
			{
				Log.Logger(ex.Message);
				return null;
			}
		}
		public List<Car> AllCars()
		{
			try
			{
				using (TaxiModel db = new TaxiModel())
				{
					return db.Cars.ToList();
				}
			}
			catch (Exception ex)
			{
				Log.Logger(ex.Message);
				return null;
			}
		}
		public string CreateNewTaxi(Taxi taxi, int idCar)
		{
			try
			{
				using (TaxiModel db = new TaxiModel())
				{
					if (taxi.Password.Length >= 8 && taxi.Email.Contains("@") && taxi.Email.Contains(".") && taxi.Email.Length > 8)
					{
						if (db.Taxies.FirstOrDefault(elem => elem.Email == taxi.Email) != null)
							return "This email is in db";
                        taxi.Car = db.Cars.First(elem => elem.Id == idCar);
                        taxi.Lat = 0;
                        taxi.Longt = 0;
						db.Taxies.Add(taxi);
						db.SaveChanges();
						return "Taxi is add";
					}
					else
						return "Password must be longer then 8 symvols and you need email";
				}
			}
			catch (Exception ex)
			{
				Log.Logger(ex.Message);
				return "Error. Peson is not add";
			}
		}
		public bool Authoriz(string email, string passw)
		{
			try
			{
				using (TaxiModel db = new TaxiModel())
				{
					taxi = db.Taxies.FirstOrDefault(elem => elem.Email == email && elem.Password == passw);
                    if (taxi == null)
                        return false;
                    return true;
				}
			}
			catch (Exception ex)
			{
				Log.Logger(ex.Message);
                return false;
			}
		}
		public bool WriteEmail(string email, string Thema, string Message)
		{
			try
			{
                MailMessage m = new MailMessage(new MailAddress(taxi.Email, taxi.DriverName), new MailAddress(email));
				m.Subject = Thema;
				m.Body = Message;
				SmtpClient smtp = new SmtpClient("aspmx.l.google.com", 25);
				//smtp.Credentials = new NetworkCredential();
				smtp.EnableSsl = true;
				smtp.Send(m);
				return true;
			}
			catch (Exception ex)
			{
				Log.Logger(ex.Message);
				return false;
			}
		}
		public string ChangeStateOfOrder(int orderid, string state, string comment)
		{
			try
			{
				using (TaxiModel db = new TaxiModel())
				{
					db.Orders.First(elem => elem.Id == orderid).StateOfOrder = state;
                    db.Orders.First(elem => elem.Id == orderid).Coment = comment;
                    db.SaveChanges();
					return "State of order is change";
				}
			}
			catch (Exception ex)
			{
				Log.Logger(ex.Message);
				return "Something wrong. You can't change state of order";
			}
		}
		public string ForgetPassword(string email)
		{
			try
            { 
				string tmp = "";
				using (TaxiModel db = new TaxiModel())
				{
					tmp = db.Taxies.First(elem => elem.Email == email).Password;
				}
                if (WriteEmail(email, "Taxi: Forget Password", " Your password on service taxi: " + tmp) == true)
                    return "Password is send on your email";
                else return "Message is not send";
			}
			catch (Exception ex)
			{
				Log.Logger(ex.Message);
				return "Something wrong. Message is not receive";
			}
		}
        public void SingOut()
        {
            try
            {
                Place(0,0);
                taxi = null;
            }
            catch (Exception ex)
            {
                Log.Logger(ex.Message);
            }
        }
        public void Place(double C, double S)
        {
            try
            {
                using (TaxiModel db = new TaxiModel())
                {
                    db.Taxies.First(elem => elem.Email == taxi.Email).Lat = C;
                    db.Taxies.First(elem => elem.Email == taxi.Email).Longt = S;
                    db.SaveChanges();
                }
                Authoriz(taxi.Email, taxi.Password);
            }
            catch(Exception ex)
            {
                Log.Logger(ex.Message);
            }
        }
        public string CreateCar(Car car)
        {
            try
            {
                using (TaxiModel db = new TaxiModel())
                {
                    if (car.Marka.Length == 0 || car.MaxNumberOfPassagerOrVantazhKG <= 0)
                        return "Input true info";
                    db.Cars.Add(car);
                    db.SaveChanges();
                    return "";
                }
            }
            catch(Exception ex)
            {
                Log.Logger(ex.Message);
                return "Something wrong. Car is not create";
            }
        }
        public Taxi AboutMe()
        {
            try
            {
                using (TaxiModel db = new TaxiModel())
                {
                    return taxi;
                }
            }
            catch(Exception ex)
            {
                Log.Logger(ex.Message);
                return null;
            }
        }
    }
}
