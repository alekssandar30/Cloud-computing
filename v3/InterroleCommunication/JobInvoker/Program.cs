using InterroleContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

//ovaj proces treba da
//omogući unos gornje granice intervala nakon čega i poziv WCF servisa sa vrednostima intervala
//[0, n]

namespace JobInvoker
{
    class Program
    {
        public static IJob proxy;

        static void Main(string[] args)
        {

            int rez = -1;
            int broj;
            do
            {
                Console.WriteLine("Unesite gornju granicu intervala: ");
                broj = Int32.Parse(Console.ReadLine());

                Connect();

                rez = proxy.DoCalculus(broj);
                Console.WriteLine($"Suma prvih {broj} prirodnih brojeva je: {rez}");

            } while (broj != 0);
            Console.ReadLine();

            
        }

        public static void Connect()
        {
            NetTcpBinding binding = new NetTcpBinding();
            ChannelFactory<IJob> factory = new ChannelFactory<IJob>(binding,
                new EndpointAddress("net.tcp://localhost:10100/InputRequest"));
            proxy = factory.CreateChannel();
        }
    }
}
