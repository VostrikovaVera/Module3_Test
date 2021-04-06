using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Module3_Test.Configs;
using Module3_Test.Services.Abstractions;

namespace Module3_Test.Services
{
    public class LoggerService : ILoggerService
    {
        private readonly LoggerConfig _loggerConfig;
        private readonly StringBuilder _generalLog = new StringBuilder();
        private int _logCounter = 0;

        static LoggerService()
        {
        }

        public LoggerService()
        {
            var config = LocatorService.ConfigService;
            _loggerConfig = config.LoggerConfig;
        }

        public event Action<string, string> LogBackupHandler;

        public void CreateLog(string message)
        {
            var log = $"{DateTime.UtcNow}:{message}";

            _generalLog.AppendLine(log);

            _logCounter++;

            if (_logCounter == _loggerConfig.BackUpCount)
            {
                HandleBackup();

                _logCounter = 0;
            }
        }

        public void HandleBackup()
        {
            var dirPath = _loggerConfig.DirectoryPath;

            var fileName = $"{DateTime.UtcNow.ToString(_loggerConfig.NameFormat)}";
            var filePath = $"{dirPath}{fileName}{_loggerConfig.ExtensionFile}";

            LogBackupHandler.Invoke(filePath, _generalLog.ToString());
        }
    }
}
