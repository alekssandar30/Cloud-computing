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
        //otvaranje i zatvaranje konekcije servisa
        //koristi se NetTcpBinding
        //za dobijanje ip endpointa trenutnog procesa koristi se klasa RoleEnviroment

        private ServiceHost _serviceHost;
        private String _externalEndPointName = "InputRequest";

        public JobServer()
        {
            RoleInstanceEndpoint inputEndPoint = RoleEnvironment.
                CurrentRoleInstance.InstanceEndpoints[_externalEndPointName];
            string endpoint = ($"net.tcp://{inputEndPoint.IPEndpoint}/{_externalEndPointName}");
            _serviceHost = new ServiceHost(typeof(JobServerProvider));
            NetTcpBinding binding = new NetTcpBinding();

            _serviceHost.AddServiceEndpoint(typeof(IJob), binding, endpoint);
        }

        public void Open()
        {
            try
            {
                _serviceHost.Open();
                Trace.TraceInformation(String.Format("Host for {0} endpoint type opened successfully at {1}", _externalEndPointName, DateTime.Now));
            }
            catch (Exception e)
            {
                Trace.TraceInformation("Host open error for {0} endpoint type. Error message is: {1}. ", _externalEndPointName, e.Message);
           }
        }

        public void Close()
        {
            try
            {
                _serviceHost.Close();
                Trace.TraceInformation(String.Format("Host for {0} endpoint type closed successfully at {1}", _externalEndPointName, DateTime.Now));
            }
            catch (Exception e)
            {
                Trace.TraceInformation("Host close error for {0} endpoint type. Error message is: {1}. ", _externalEndPointName, e.Message);
            }
        }
    }
}
