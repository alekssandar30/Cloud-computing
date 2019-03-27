using Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerRole2
{
    public class JobServiceProvider2 : IForward
    {
        public string Send(string message)
        {
            Trace.TraceInformation($"Message received: {message}\n");
            return $"Instance u WorkerRole2 uspesno primile poruku\n";
        }
    }
}
