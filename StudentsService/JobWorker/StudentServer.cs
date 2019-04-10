using Contracts;
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
    public class StudentServer
    {
        ServiceHost serviceHost;
        NetTcpBinding binding = new NetTcpBinding();
        string endpointName = "Input";

        public StudentServer()
        {
            RoleInstanceEndpoint roleInstance = RoleEnvironment.CurrentRoleInstance
                .InstanceEndpoints[endpointName];
            string adresa = $"net.tcp://{roleInstance.IPEndpoint}/{endpointName}";
            serviceHost = new ServiceHost(typeof(StudentServerProvider));
            serviceHost.AddServiceEndpoint(typeof(IStudent), binding, adresa);
        }

        public void Open()
        {
            try
            {
                serviceHost.Open();
            }catch(Exception e)
            {
                Trace.WriteLine($"Greska pri otvaranju servera: {e.Message}");
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
                Trace.WriteLine($"Greska pri zatvaranju servera: {e.Message}");
            }
        }

    }
}
