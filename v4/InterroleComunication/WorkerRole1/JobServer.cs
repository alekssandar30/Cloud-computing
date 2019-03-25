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
    public class JobServer
    {
        private string endPointName = "InputRequest";
        private ServiceHost serviceHost;

        public JobServer()
        {
            NetTcpBinding binding = new NetTcpBinding();
            RoleInstanceEndpoint inputEndPoint = RoleEnvironment.CurrentRoleInstance.InstanceEndpoints[endPointName];
            string endpoint = $"net.tcp://{inputEndPoint.IPEndpoint}/{endPointName}";
            serviceHost = new ServiceHost(typeof(JobServerProvider));
            serviceHost.AddServiceEndpoint(typeof(IJob), binding, endpoint);
            
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
