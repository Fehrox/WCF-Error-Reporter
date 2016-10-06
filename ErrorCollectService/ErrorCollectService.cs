using ErrorCollectEntities;
using ErrorCollectService.DataTransferObjects;
using System;
using System.Linq;
using System.ServiceModel;

namespace ErrorCollectService {

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class ErrorCollectService : IErrorCollectService {

        ErrorCollectModelContainer db = new ErrorCollectModelContainer();

        public StartedSessionDTO StartSession(SessionDTO sessionDTO) {

            var user = db.Users.FirstOrDefault(u => u.Name == sessionDTO.User);
            if (user == null)
                user = new User { Name = sessionDTO.User };

            var device = db.Devices.FirstOrDefault(d => d.DeviceGUID == sessionDTO.DeviceId);
            if (device == null)
                device = new Device { DeviceGUID = sessionDTO.DeviceId };

            var platform = db.Platforms.FirstOrDefault(p => p.Name == sessionDTO.Platform);
            if (platform == null)
                platform = new Platform { Name = sessionDTO.Platform };

            var session = new Session {
                User = user,
                Device = device,
                Platform = platform
            };
            db.Sessions.Add(session);

            try {
                db.SaveChanges();
                return new StartedSessionDTO { SessionId = session.Id };
            } catch (Exception e) {
                throw e;
            }
        }

        public void LogError(LoggedErrorDTO error) {
            
            var loggedException = BuildExceptionLog(error);
            var occurance = BuildOccerence(error);

            occurance.ExceptionLog = loggedException;
            occurance.SessionId = error.SessionId;

            db.Occurances.Add(occurance);

            try {
                db.SaveChanges();
            } catch (Exception e) {
                throw e;
            }
        }

        private Occurance BuildOccerence(LoggedErrorDTO error) {
            return new Occurance {
                StackTrace = error.StackTrace,
                Time = error.Time
            };
        }

        private ExceptionLog BuildExceptionLog(LoggedErrorDTO error) {
            var message = db.Messages.FirstOrDefault(m => m.Text == error.Message);
            if (message == null)
                message = new Message { Text = error.Message };

            var helpLink = db.HelpLinks.FirstOrDefault(hl => hl.Link == error.HelpLink);
            if (helpLink == null && error.HelpLink != null)
                helpLink = new HelpLink { Link = error.HelpLink };

            var source = db.Sources.FirstOrDefault(s => s.Application == error.Source);
            if (source == null && error.Source != null)
                source = new Source { Application = error.Source };

            //var data = db.Data.Where(d => d.DataEntries.)

            var exceptionType = db.ExceptionTypes.FirstOrDefault(e => e.Name == error.ExceptionType);
            if (exceptionType == null)
                exceptionType = new ExceptionType { Name = error.ExceptionType };

            var loggedException = new ExceptionLog();
            loggedException.Message = message;
            loggedException.HelpLink = helpLink;
            loggedException.Source = source;
            loggedException.ExceptionType = exceptionType;
            return loggedException;
        }

    }
}
