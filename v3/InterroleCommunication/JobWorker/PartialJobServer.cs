﻿using InterroleContracts;
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
    
    public class PartialJobServer
    {
        private ServiceHost serviceHost;
        private string internalEndpointName = "InternalRequest";


        public PartialJobServer()
        {
            RoleInstanceEndpoint inputEndPoint = RoleEnvironment.
                CurrentRoleInstance.InstanceEndpoints[internalEndpointName];
            string endpoint = $"net.tcp://{inputEndPoint.IPEndpoint}/{internalEndpointName}";
            serviceHost = new ServiceHost(typeof(PartialJobServerProvider));
            var binding = new NetTcpBinding();

            serviceHost.AddServiceEndpoint(typeof(IPartialJob), binding, endpoint);
        }

        public void Open()
        {
            try
            {
                serviceHost.Open();
                Trace.TraceInformation(String.Format("Host for {0} endpoint type opened successfully at {1}", internalEndpointName, DateTime.Now));
            }
            catch (Exception e)
            {
                Trace.TraceInformation("Host open error for {0} endpoint type. Error message is: {1}. ", internalEndpointName, e.Message);
            }
        }

        public void Close()
        {
            try
            {
                serviceHost.Close();
                Trace.TraceInformation(String.Format("Host for {0} endpoint type closed successfully at {1}", internalEndpointName, DateTime.Now));
            }
            catch (Exception e)
            {
                Trace.TraceInformation("Host close error for {0} endpoint type. Error message is: {1}. ", internalEndpointName, e.Message);
            }
        }
    }
}
