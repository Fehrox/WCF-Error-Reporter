using System.ServiceModel;

namespace ErrorCollectService.DataTransferObjects {
    [MessageContract]
    public class SessionDTO {

        public SessionDTO() { }

        public SessionDTO(string deviceId, 
                          string user,
                          string platform) {
            DeviceId = deviceId;
            User = user;
            Platform = platform;
        }

        [MessageBodyMember]
        public string DeviceId { get; set; }

        [MessageBodyMember]
        public string User { get; set; }

        [MessageBodyMember]
        public string Platform { get; set; }
    }
}
