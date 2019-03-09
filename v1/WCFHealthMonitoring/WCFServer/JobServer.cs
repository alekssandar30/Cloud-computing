using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCFServer
{
    public class JobServer
    {
        //ovde ce biti metoda za otvaranje i zatvaranje konekcije servisa

        private ServiceHost _serviceHost;

        public JobServer()
        {
            Start();
            
        }


        public void Start()
        {
            _serviceHost = new ServiceHost(typeof(HealthMonitoring));
            NetTcpBinding binding = new NetTcpBinding();
            _serviceHost.AddServiceEndpoint(typeof(IHealthMonitoring), binding, new
            Uri("net.tcp://localhost:6000/HealthMonitoring"));
            _serviceHost.Open();
            Console.WriteLine("Server ready and waiting for requests.");
            Console.ReadLine();
        }

        public void Stop()
        {
            _serviceHost.Close();
            Console.WriteLine("Server stopped.");
        }



    }
}
