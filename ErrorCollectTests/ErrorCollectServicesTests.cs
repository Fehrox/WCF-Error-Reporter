using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ServiceModel;
using System.Linq;
using NSubstitute;
using ErrorCollectEntities;
using ErrorCollectClient;
using ErrorCollectClient.ServiceReference;

namespace ErrorCollectTests {
    [TestClass]
    public class ErrorCollectServicesTests {


        [TestMethod]
        public void OpenHost() {
            // Arrange
            using (var host = new ServiceHost(typeof(ErrorCollectService.ErrorCollectService))) {
                // Act
                try {
                    host.Open();
                } catch (Exception ex) {
                    throw ex;
                }

                // Assert
                Assert.IsTrue(host.State == CommunicationState.Opened);
            } 
        }

        [TestMethod]
        public void StartSession() {
            // Arrange
            var db = new ErrorCollectModelContainer();
            var guid = "submittedfromunitestlogerror-startsession";
            var username = "UnitTest-StartSession";
            var platform = "Test StartSession";
            var mockClient = new RemoteErrorCollect(
                "localhost",
                2358,
                guid,
                platform,
                username
            );
            bool sessionStarted = false;
            int timeout = 0;

            using (var host = new ServiceHost(typeof(ErrorCollectService.ErrorCollectService))) {
                host.Open();

                // Act
                mockClient.StartSession(() => { sessionStarted = true; });
                while (!sessionStarted && timeout++ < 10000)
                    System.Threading.Thread.Sleep(10);

                // Assert
                Assert.IsTrue(sessionStarted);
                Assert.IsNotNull(mockClient.SessionId);
                var session = db.Sessions.Find(mockClient.SessionId);
                Assert.IsNotNull(session);
                Assert.AreEqual(guid, session.Device.DeviceGUID);
                Assert.AreEqual(username, session.User.Name);
                Assert.AreEqual(platform, session.Platform.Name);
                
                db.Entry(session).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
        }

        [TestMethod]
        public void LogError() {

            // Arrange
            var db = new ErrorCollectModelContainer();
            var mockClient = GetMockClient();
            var mockException = GetMockException();
            int timeout = 0;
            Func<Occurance> checkOccurance = () => {
                var occurance = db.Occurances
                .ToList()
                .FirstOrDefault(o => {
                    var stack = (o.StackTrace ?? "") == mockException.StackTrace;
                    var type = o.ExceptionLog != null &&
                               o.ExceptionLog.ExceptionType != null &&
                               o.ExceptionLog.ExceptionType.Name == "NullReferenceExceptionProxy";
                    var help = o.ExceptionLog != null &&
                               o.ExceptionLog.HelpLink != null &&
                               o.ExceptionLog.HelpLink.Link == mockException.HelpLink;
                    var source = o.ExceptionLog != null &&
                                 o.ExceptionLog.Source != null &&
                                 o.ExceptionLog.Source.Application == mockException.Source;
                    return stack && type && help && source;
                });
                return occurance;
            };

            using (var host = new ServiceHost(typeof(ErrorCollectService.ErrorCollectService))) {
                host.Open();

                // Act
                mockClient.LogError(mockException);

                // Assert
                Occurance occurance = null;
                while (timeout++ < 1000 && (occurance = checkOccurance.Invoke()) == null)
                    System.Threading.Thread.Sleep(10);

                Assert.IsNotNull(occurance);
                var session = db.Sessions.Find(mockClient.SessionId);
                db.Entry(session).State = System.Data.Entity.EntityState.Deleted;
                db.Entry(occurance).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
        }

        private static NullReferenceException GetMockException() {
            var mockException = Substitute.For<NullReferenceException>();
            mockException.HelpLink.Returns("http://mockuri.com");
            mockException.Message.Returns("This is a test exception");
            mockException.StackTrace.Returns(Environment.StackTrace);
            mockException.Source.Returns("UnitTestAdapter: Running test");
            return mockException;
        }

        private static RemoteErrorCollect GetMockClient() {
            var guid = "submittedfromunitestlogerror";
            var username = "Test User";
            var platform = "UnitTest";
            var address = "localhost";
            var port = 2358;
            return new RemoteErrorCollect(address, port, guid, platform, username);
        }

        private ErrorCollectServiceClient GetMockServiceClient() {
            var address = new EndpointAddress(
                "net.tcp://cree8ware.com:2358/ErrorCollectService");
            var binding = new NetTcpBinding();
            binding.OpenTimeout = TimeSpan.FromSeconds(1.5);
            binding.Security.Mode = SecurityMode.None;
            return new ErrorCollectServiceClient(
                binding,
                address
            );
        }

    }
}
