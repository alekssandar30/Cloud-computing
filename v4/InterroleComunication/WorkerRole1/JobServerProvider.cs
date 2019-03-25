using Common;
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
    public class JobServerProvider : IJob
    {
        NetTcpBinding binding = new NetTcpBinding();
        private string internalEndpointName = "InternalRequest";

        public void SendMessage(string message)
        {
            Trace.TraceInformation(String.Format("String {0} received from client.", message));
            /*
             instanca koja primi prvi zahtev prosleđuje drugoj instanci dobijene vrednost
             druga instanca = sledeca instanca tj. indeks povecan za 1
             i ta druga instanca kada primi vrednost ispisuje je u emulatoru
            */
            // all internal endpoints of all worker role processes not including this worker role process
           

            int bratskeInstance = RoleEnvironment.Roles["WorkerRole1"].Instances.Count;
            int index = GetIndex(RoleEnvironment.CurrentRoleInstance.Id) + 1;
            index = index == bratskeInstance ? 0 : index;

            string adresa = "";

            foreach(var el in RoleEnvironment.Roles["WorkerRole1"].Instances)
            {
                Trace.WriteLine($"Calling node at {el.InstanceEndpoints.ToString()}", "Information");
                if(index == GetIndex(el.Id))
                {
                    adresa = String.Format("net.tcp://{0}/{1}", el.InstanceEndpoints["InternalRequest"].IPEndpoint,
                        internalEndpointName);
                    break;
                }

                
            }
            //slanje drugoj instanci:
            var factory = new ChannelFactory<IForwardMessage>(binding, new EndpointAddress(adresa));
            var proxy = factory.CreateChannel();
            proxy.SendMessage(message, RoleEnvironment.CurrentRoleInstance.Id);
        }

        private int GetIndex(string instanceId)
        {
            int instanceIndex = 0;
            if(!int.TryParse(instanceId.Substring(instanceId.LastIndexOf('.')+1), out instanceIndex))
            {
                int.TryParse(instanceId.Substring(instanceId.LastIndexOf('_') + 1), out instanceIndex);
            }

            return instanceIndex;
        }
    }
}
