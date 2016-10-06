using ErrorCollectService.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ErrorCollectService
{

    [ServiceContract/*(CallbackContract=typeof(IMessageClient))*/]
    public interface IErrorCollectService {
        [OperationContract(IsOneWay = true)]
        void LogError(LoggedErrorDTO error);

        [OperationContract(IsOneWay = false)]
        StartedSessionDTO StartSession(SessionDTO session);
    }

    //public interface IMessageClient {
    //    [OperationContract(IsOneWay = true)]
    //    void RecieveMessage(string message);
    //}

}
