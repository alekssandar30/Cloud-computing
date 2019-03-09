using InterroleContracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobWorker
{
    public class JobServerProvider : IJob
    {
        // this will contain logic for doing calculus for a client
        // this class implements contract
        public int DoCalculus(int to)
        {
            int result = 0;
            Trace.WriteLine($"DoCalculus method called - interval [1, {to}");

            for (int i=0; i<to; i++)
            {
                result += i;
            }
            
            return result;
        }
    }
}
