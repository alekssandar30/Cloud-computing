using InterroleContracts;
using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace JobWorker
{
    public class JobServer
    {
        // this will contain logic for opening and closing connection
        // for IP addres of a current proccess we are using class RoleEnviroment

        private ServiceHost serviceHost;
        private string endPointName = "InputRequest";

        public JobServer()
        {
            RoleInstanceEndpoint inputEndPoint = RoleEnvironment.CurrentRoleInstance
                .InstanceEndpoints[endPointName];
            string endPoint = String.Format("net.tcp://{0}/{1}", inputEndPoint.IPEndpoint, endPointName);
            serviceHost = new ServiceHost(typeof(JobServerProvider));
            NetTcpBinding binding = new NetTcpBinding();

            serviceHost.AddServiceEndpoint(typeof(IJob), binding, endPoint);
        }

        public void Open()
        {
            try
            {
                serviceHost.Open();
                Trace.TraceInformation($"Host for {0} endpoint type opened succesfully at {1}",
                    endPointName, DateTime.Now);

            }
            catch (Exception e)
            {

                Trace.TraceInformation($"Host open error for {0} endpoint type. Error message is {1}",
                    endPointName, e.Message);
            }
        }

        public void Close()
        {
            try
            {
                serviceHost.Close();
                Trace.TraceInformation(String.Format("Host for {0} endpoint type closed successfully at {1}"
                ,endPointName, DateTime.Now));
            }
            catch (Exception e)
            {

                Trace.TraceInformation($"Host close error for {0} endpoint type. Error message is: {1}. "                    , endPointName, e.Message);
            }
        }
    }
}
