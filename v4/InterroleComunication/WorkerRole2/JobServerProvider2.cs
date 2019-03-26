using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            list.Add(new Poruka(firstInstanceId, secondInstanceId, message)); //cuvanje
            foreach(var item in list)
            {
                Trace.TraceInformation($"Poruka sacuvana: {item.ToString()}");
            }
            
        }
    }
}
