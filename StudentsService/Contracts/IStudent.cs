using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    [ServiceContract]
    public interface IStudent
    {
        [OperationContract]
        List<string> RetrieveAllIndexes();

        [OperationContract]
        void AddStudent(string indexNo, string name, string lastName);
    }
}
