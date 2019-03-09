using Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFServer
{
    public class HealthMonitoring : IHealthMonitoring
    {
        //ova klasa implementira interfejs iz contract-a
        public void IAmAlive()
        {
            Trace.WriteLine("I am alive.");
        }
    }
}
