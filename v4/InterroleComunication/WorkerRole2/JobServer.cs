using Common;
using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WorkerRole2
{
    public class JobServer
    {
        private static ServiceHost serviceHost;
        private string endPointName = "InternalRequest";

        public JobServer()
        {
            NetTcpBinding binding = new NetTcpBinding();
            RoleInstanceEndpoint instanceEndpoint = RoleEnvironment.CurrentRoleInstance.InstanceEndpoints[endPointName];
            string endpoint = $"net.tcp://{instanceEndpoint.IPEndpoint}/{endPointName}";
            serviceHost = new ServiceHost(typeof(JobServerProvider2));
            serviceHost.AddServiceEndpoint(typeof(IForwardMessageToSecondRole), binding, endpoint);
        }

        public void Open()
        {
            try
            {
                serviceHost.Open();
                Trace.TraceInformation($"Host for {endPointName} type opened sucesfully at {DateTime.Now}");
            }
            catch (Exception e)
            {
                Trace.TraceInformation($"Host open error for {endPointName} endpoint type. Error message is" +
                    $" {e.Message}");

            }
        }

        public void Close()
        {
            try
            {
                serviceHost.Close();
                Trace.TraceInformation($"Host for {endPointName} type closed sucesfully at {DateTime.Now}");
            }
            catch (Exception e)
            {
                Trace.TraceInformation($"Host close error for {endPointName} endpoint type. Error message is" +
                    $" {e.Message}");

            }
        }
    }
}
