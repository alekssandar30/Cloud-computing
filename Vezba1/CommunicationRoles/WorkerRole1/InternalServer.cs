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
    public class InternalServer
    {
        NetTcpBinding binding = new NetTcpBinding();
        ServiceHost serviceHost;
        string endpointName = "Internal";

        public InternalServer()
        {
            RoleInstanceEndpoint roleInstanceEndpoint = RoleEnvironment
                .CurrentRoleInstance.InstanceEndpoints[endpointName];
            string adresa = $"net.tcp://{roleInstanceEndpoint.IPEndpoint}/{endpointName}";
            serviceHost = new ServiceHost(typeof(InternalServiceProvider));
            serviceHost.AddServiceEndpoint(typeof(IForward), binding, adresa);
        }

        public void Open()
        {
            try
            {
                serviceHost.Open();
            }
            catch (Exception e)
            {
                Trace.WriteLine($"Server openning error: {e.Message}\n");
            }
        }

        public void Close()
        {
            try
            {
                serviceHost.Close();
            }
            catch (Exception e)
            {
                Trace.WriteLine($"Server closing error: {e.Message}\n");
            }
        }
    }
}
