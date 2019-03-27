using Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerRole1
{
    public class InternalServiceProvider : IForward
    {
        public string Send(string message)
        {
            Trace.TraceInformation($"Message received:  {message}\n");
            return "Instanca u workerRoli1 uspesno primila poruku.\n";
        }
    }
}
