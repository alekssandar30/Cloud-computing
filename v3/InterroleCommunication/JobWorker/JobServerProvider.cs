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
        public int DoCalculus(int to)
        {
            //potrebno je izračunati sumu i ispisati poruku u Compute emulatoru: “DoCalculus
            //method called - interval[1, n]”

            Trace.WriteLine($"DoCalculus method called - interval [1, {to}");
            int rez = 0;
            for(int i=0; i<=to; i++)
            {
                rez += i;
            }

            return rez;
        }
    }
}
