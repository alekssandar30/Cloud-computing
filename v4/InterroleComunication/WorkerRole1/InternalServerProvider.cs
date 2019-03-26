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
    public class InternalServerProvider : IForwardMessage
    {
        NetTcpBinding binding = new NetTcpBinding();
        IForwardMessageToSecondRole proxy;
      
        public void SendMessage(string message, string instanceId)
        {
            Trace.TraceInformation($"message: {message} received from brother instance");
            //primanje poruke i slanje na sledecu rolu:
            //instanca salje instanci u drugoj roli svoj ID, ID instance od koje 
            //je dobila vrednost i samu vrednost
            //instanca druge role prima poruku i skladisti te podatke lokalno u neku strukturu

            string adresa = $"net.tcp://{RoleEnvironment.Roles["WorkerRole2"].Instances[0].InstanceEndpoints["InternalRequest"].IPEndpoint}/InternalRequest";
            var factory = new ChannelFactory<IForwardMessageToSecondRole>(binding, new EndpointAddress(adresa));
            proxy = factory.CreateChannel();
            proxy.SendMessage(message, instanceId, RoleEnvironment.CurrentRoleInstance.Id);



        }
    }
}
