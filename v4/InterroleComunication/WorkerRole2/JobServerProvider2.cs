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
    public class JobServerProvider2 : IForwardMessageToSecondRole
    {
        List<Poruka> list = new List<Poruka>();
       

        public void SendMessage(string message, string firstInstanceId, string secondInstanceId)
        {
            //treba da cuva poruku u nekoj strukturi i da ispise
            Trace.TraceInformation($"Message {message} received from WorkerRole1");

            

            list.Add(new Poruka(GetIndex(firstInstanceId), GetIndex(secondInstanceId), message)); //cuvanje
            foreach(var item in list)
            {
                Trace.TraceInformation($"Poruka sacuvana: {item.ToString()}");
            }
            
        }

        private int GetIndex(string instanceId)
        {
            int instanceIndex = 0;
            if (!int.TryParse(instanceId.Substring(instanceId.LastIndexOf('.') + 1), out instanceIndex))
            {
                int.TryParse(instanceId.Substring(instanceId.LastIndexOf('_') + 1), out instanceIndex);
            }

            return instanceIndex;
        }
    }
}
