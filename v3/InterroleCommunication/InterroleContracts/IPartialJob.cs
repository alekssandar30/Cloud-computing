﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace InterroleContracts
{
    [ServiceContract]
    public interface IPartialJob
    {
        [OperationContract]
        int DoSum(int from, int to);
    }
}