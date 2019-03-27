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
    public class JobServiceProvider : IForward
    {
        string endpointName = "Internal";
        IForward proxy;
        NetTcpBinding binding = new NetTcpBinding();

        public string Send(string message)
        {
            //Trace.TraceInformation($"Message received:  {message}\n");
            //return "Message succesfully sent.\n";

            //salji instanci ciji je id za 1 manji od trenutne 
            var brotherInstances = RoleEnvironment.Roles["WorkerRole1"].Instances;

            int index = GetIndex(RoleEnvironment.CurrentRoleInstance.Id)-1;  //umanjen za 1

            if(index < 0)
            {
                index = brotherInstances.Count - 1;
            }
            string adresa = "";

            foreach (var item in brotherInstances)
            {
                Trace.WriteLine($"***Calling node on {item.InstanceEndpoints.ToString()}\n\n");
                if(index == GetIndex(item.Id))
                {
                    adresa = $"net.tcp://{item.InstanceEndpoints[endpointName].IPEndpoint}/{endpointName}";
                    break;
                }
            }

            //slanje odredjenoj instanci:
            ChannelFactory<IForward> factory = new ChannelFactory<IForward>(binding,
                new EndpointAddress(adresa));
            proxy = factory.CreateChannel();
            proxy.Send(message);
            return "Uspesno prosledjeno instanci za id-em umanjenim za 1\n";
        }

        private int GetIndex(string instanceId)
        {
            int instanceIndex = 0;
            if (!int.TryParse(instanceId.Substring(instanceId.LastIndexOf('.')+1), out instanceIndex))
            {
                int.TryParse(instanceId.Substring(instanceId.LastIndexOf('_') + 1), out instanceIndex);
            }

            return instanceIndex;
        }
    }
}
