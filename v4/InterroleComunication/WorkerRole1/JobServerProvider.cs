using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerRole1
{
    public class JobServerProvider : IJob
    {
        public void SendMessage(string message)
        {
            Trace.WriteLine(message, "Information");
        }
    }
}
