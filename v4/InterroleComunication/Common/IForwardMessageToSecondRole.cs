using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
    public interface IForwardMessageToSecondRole
    {
        [OperationContract]
        void SendMessage(string message, string firstInstanceId, string secondInstanceId);
    }
}
