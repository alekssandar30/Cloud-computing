using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static IForward proxy;
        static void Main(string[] args)
        {
            //client salje string (poruku)
            string message = "";
            string odgovor = "";
            do
            {
                Console.WriteLine("Unesi poruku: ");
                message = Console.ReadLine();

                Connect();

                odgovor = proxy.Send(message);
                Console.WriteLine(odgovor);
            } while (message != "");

            Console.ReadLine();
        }

        public static void Connect()
        {
            NetTcpBinding binding = new NetTcpBinding();
            ChannelFactory<IForward> factory = new ChannelFactory<IForward>(binding,
                new EndpointAddress($"net.tcp://localhost:10100/Input"));
            proxy = factory.CreateChannel();
            
        }
    }
}
