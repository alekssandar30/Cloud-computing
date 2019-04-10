using Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {

            ChannelFactory<IStudent> factory = new ChannelFactory<IStudent>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:10100/Input"));

            IStudent proxy = factory.CreateChannel();

            string unos = "";
            string[] unosi;
            while(true)
            {
                Console.WriteLine("Unesi studenta u formatu [broj_indexa, ime, prezime] ili unesi 'end' ");
                unos = Console.ReadLine();
                if (unos.ToLower() == "end")
                    break;

                unosi = unos.Split(',');
                
                proxy.AddStudent(unosi[0], unosi[1], unosi[2]);
                Trace.TraceInformation($"{unosi[0]}, {unosi[1]}, {unosi[2]} je uspesno unet.\n");

            }

            List<string> indexi = new List<string>();
            foreach(string el in proxy.RetrieveAllIndexes())
            {
                indexi.Add(el);
            }

            Console.WriteLine("Svi trenutni indexi:");
            foreach (string index in indexi)
            {
                Console.WriteLine(index);
            }


            Console.ReadKey();

        }
    }
}
