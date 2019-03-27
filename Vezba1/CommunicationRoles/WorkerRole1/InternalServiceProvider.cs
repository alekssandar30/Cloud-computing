using Contracts;
using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WorkerRole1
{
    public class InternalServiceProvider : IForward
    {
        NetTcpBinding binding = new NetTcpBinding();
        string endpointName = "Internal";
        IForward proxy;

        public string Send(string message)
        {
            Trace.TraceInformation($"Message received:  {message}\n");
            //return "Instanca u workerRoli1 uspesno primila poruku.\n";

            //salji svim instancama WorkerRole2 istovremeno
            var instancesOfSecondRole = RoleEnvironment.Roles["WorkerRole2"].Instances;
            string adresa = "";

            foreach(var el in instancesOfSecondRole)
            {
                adresa = $"net.tcp://{el.InstanceEndpoints[endpointName].IPEndpoint}/{endpointName}";
                ChannelFactory<IForward> factory = new ChannelFactory<IForward>(binding,
                    new EndpointAddress(adresa));
                proxy = factory.CreateChannel();
                Task t = Task.Factory.StartNew(() =>
                {
                    proxy.Send(message);
                });

                //Trace.TraceInformation($"Task completed at {DateTime.Now}");
            }
            Task.WaitAll();

            return $"Poruka uspesno poslata instancama u WorkerRole2\n";
        }
    }
}
