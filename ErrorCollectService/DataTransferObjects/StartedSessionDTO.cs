using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ErrorCollectService.DataTransferObjects {
    [MessageContract]
    public class StartedSessionDTO {
        [MessageBodyMember]
        public int SessionId { get; set; }
    }
}
