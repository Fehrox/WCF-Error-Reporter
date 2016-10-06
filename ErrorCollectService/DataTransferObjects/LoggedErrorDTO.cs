using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace ErrorCollectService.DataTransferObjects {
    [MessageContract]
    public class LoggedErrorDTO {

        public LoggedErrorDTO() { }

        public LoggedErrorDTO(string exceptionType,
                              string message, 
                              string helpLink, 
                              string source, 
                              List<KeyValuePair<string, string>> data,
                              DateTime time, 
                              string stackTrace) {

            ExceptionType = exceptionType;
            Message = message;
            HelpLink = helpLink;
            Source = source;
            Data = data;

            Time = time;
            StackTrace = stackTrace;
        }

        [MessageBodyMember]
        public string ExceptionType { get; set; }

        [MessageBodyMember]
        public string Message { get; set; }

        [MessageBodyMember]
        public string HelpLink { get; set; }

        [MessageBodyMember]
        public string Source { get; set; }

        [MessageBodyMember]
        public List<KeyValuePair<string, string>> Data { get; set; }
        
        [MessageBodyMember]
        public DateTime Time { get; set; }

        [MessageBodyMember]
        public string StackTrace { get; set; }

        [MessageBodyMember]
        public int SessionId { get; set; }
    }
}
