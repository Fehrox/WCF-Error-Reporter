//using ErrorCollectClient.ServiceReference;
using ErrorCollectClient.ServiceReference;
using System;
using System.ServiceModel;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Net.Sockets;

namespace ErrorCollectClient {
    public class RemoteErrorCollect
    {

        private string _server;
        private int _timeout = 1000, _port;

        private static ErrorCollectServiceClient _serviceClient;
        int? _sessionId;
        string _deviceId, _platform, _user;

        public int? SessionId { get { return _sessionId; } }

        public string User { set { _user = value; } }

        public RemoteErrorCollect(string server, int port, string deviceId, string platform, string user) {
            _server = server;
            _port = port;
            _deviceId = deviceId;
            _platform = platform;
            _user = user;

            var uri = "net.tcp://" + _server + ":" + _port + "/ErrorCollectService";
            var address = new EndpointAddress(uri);
            var binding = new NetTcpBinding();
            binding.Security.Mode = SecurityMode.None;
            _serviceClient = new ErrorCollectServiceClient(binding, address);
        }

        bool IsServiceAvailable() {
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sock.ReceiveTimeout = sock.SendTimeout = _timeout;
            IAsyncResult res = sock.BeginConnect( _server, 2358, null, null);
            return res.AsyncWaitHandle.WaitOne(sock.SendTimeout, true);
        }

        public void StartSession(Action onSessionStarted) {
            if (IsServiceAvailable()) {
                _serviceClient.BeginStartSession(
                    _deviceId, 
                    _platform, 
                    _user, 
                    new AsyncCallback(EndStartSession), 
                    onSessionStarted
                );
            }
        }

        void EndStartSession(IAsyncResult result) {
            _sessionId = _serviceClient.EndStartSession(result);
            (result.AsyncState as Action).Invoke();
        }

        public void LogError(Exception ex)
        {

            if (!_sessionId.HasValue) {
                StartSession(() => LogError(ex));
                return;
            }

            var dataEntries = new List<KeyValuePair<string, string>>();
            foreach (DictionaryEntry entry in ex.Data) {
                var kpv = new KeyValuePair<string, string>(
                    entry.Key.ToString(),
                    entry.Value.ToString()
                );
                dataEntries.Add(kpv);
            }

            _serviceClient.BeginLogError(
                dataEntries.ToArray(),
                ex.GetType().Name,
                ex.HelpLink,
                ex.Message,
                _sessionId.Value,
                ex.Source,
                ex.StackTrace,
                DateTime.Now,
                null,
                null
            );

        }
    }

}
