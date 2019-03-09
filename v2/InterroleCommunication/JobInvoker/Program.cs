using InterroleContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace JobInvoker
{
    class Program
    {
        private static IJob proxy;

        static void Main(string[] args)
        {
            //THIS IS CLIENT.

            int broj = -1;

            do
            {
                Console.WriteLine("Unesi gornju granicu (1-n) > ");
                int gornjaGranica = int.Parse(Console.ReadLine());

                Connect();

                broj = proxy.DoCalculus(gornjaGranica); //method from server

                Console.WriteLine($"Suma prvih {gornjaGranica} prirodnih brojeva je: {broj}" );
            } while (broj != 0);

            
            Console.ReadKey();
            
        }

        public static void Connect()
        {
            var binding = new NetTcpBinding();
            ChannelFactory<IJob> factory = new ChannelFactory<IJob>(binding,
                new EndpointAddress("net.tcp://localhost:10100/InputRequest"));
            proxy = factory.CreateChannel();
        }
    }
}
