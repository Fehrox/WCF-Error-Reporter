using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DuplexService
{

    [ServiceContract(CallbackContract=typeof(IMessageClient))]
    public interface IService {
        [OperationContract(IsOneWay = true)]
        void Connect();
    }

    public interface IMessageClient {
        [OperationContract(IsOneWay = true)]
        void RecieveMessage(string message);
    }

}
