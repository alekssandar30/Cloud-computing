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
    public class JobServerProvider : IJob
    {
        private ServiceHost serviceHost;
        private string internalEndpointName = "InternalRequest";
        NetTcpBinding binding = new NetTcpBinding();

        public int DoCalculus(int to)
        {
            //2.
            //potrebno je izračunati sumu i ispisati poruku u Compute emulatoru: “DoCalculus
            //method called - interval[1, n]”

            //3.
            //Metodu IJob interfejsa kog implementira JobServerProvider izmeniti tako da osim
            //ispisivanja intervala u compute emulatoru poziva metode IPartialJob interfejsa ostalih instanci
            //prosleđujući iste vrednosti intervala.

            Trace.WriteLine($"DoCalculus method called - interval [1, {to}");

            // all internal endpoints of all worker role processes not including this
            //worker role process:
            List<EndpointAddress> internalEndPoints =
                RoleEnvironment.Roles[RoleEnvironment.CurrentRoleInstance.Role.Name].
                Instances.Where(i => i.Id != RoleEnvironment.CurrentRoleInstance.Id).
                Select(process => new EndpointAddress(String.Format("net.tcp://{0}/{1}",
                process.InstanceEndpoints[internalEndpointName].IPEndpoint.ToString(),
                internalEndpointName))).ToList();

            int brotherInstances = internalEndPoints.Count;
            int totalSum = 0;

            Task<int>[] tasks = new Task<int>[brotherInstances];

            for(int i=0; i< brotherInstances; i++)
            {
                Trace.WriteLine($"Calling node at: {internalEndPoints[i].ToString()}", "Information");
                int index = i;

                Task<int> calculatePartialSum = new Task<int>(() =>
                    {
                        IPartialJob proxy = new ChannelFactory<IPartialJob>(binding, internalEndPoints[index]).CreateChannel();
                        return proxy.DoSum(0, to);
                    });
                calculatePartialSum.Start();

                tasks[index] = calculatePartialSum;
            }

            Task.WaitAll(tasks);

            foreach(Task<int> task in tasks)
            {
                totalSum += task.Result;
            }

            return totalSum;
        }
    }
}
