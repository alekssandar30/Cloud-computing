using Common;
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
        private static IJob proxy;

        static void Main(string[] args)
        {
            string input = "";

            do
            {
                Console.WriteLine("Unesite poruku: ");
                Console.Write("> ");
                input = Console.ReadLine();

                Connect();

                proxy.SendMessage(input);

            } while (input != "");

            Console.ReadLine();
        }

        public static void Connect()
        {
            NetTcpBinding binding = new NetTcpBinding();
            ChannelFactory<IJob> factory = new ChannelFactory<IJob>(
                binding, new EndpointAddress("net.tcp://localhost:10100/InputRequest"));

            proxy = factory.CreateChannel();
        }
    }
}
