using InterroleContracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobWorker
{
    public class PartialJobServerProvider : IPartialJob
    {
        public int DoSum(int from, int to)
        {
            Trace.WriteLine($"DoSum method called - interval [{from}, {to}]");
            //TODO:
            //Implementirati algoritam sukcesivnog sabiranja i vraćati
            //vrednost kao odgovor metode DoSum.

            int result = 0;
            for (int i = from; i <= to; i++)
            {
                result += i;
            }
            return result;

        }
    }
}
