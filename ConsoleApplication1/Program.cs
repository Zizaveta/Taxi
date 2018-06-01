using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.ServiceReference1;
namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            PersonClient client = new PersonClient();
            Console.WriteLine( client.CreateNewPerson("Liza", "Renzhyna", "miss.elizaveta.rengan@gmail.com", "12345678", null));
        }
    }
}
