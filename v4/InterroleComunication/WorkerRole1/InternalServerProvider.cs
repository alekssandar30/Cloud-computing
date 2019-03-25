using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerRole1
{
    public class InternalServerProvider : IForwardMessage
    {
      
        public void SendMessage(string message, string instanceId)
        {
            Trace.TraceInformation($"message: {message} received from brother instance");

        }
    }
}
