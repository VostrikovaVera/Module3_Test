using System;
using System.IO;
using System.Text;
using Module3_Test.Configs;
using Module3_Test.Services.Abstractions;

namespace Module3_Test.Services
{
    public class LoggerService : ILoggerService
    {
        private readonly IFileService _fileService;
        private readonly LoggerConfig _loggerConfig;
        private readonly StringBuilder _generalLog = new StringBuilder();
        private int _logCounter = 0;

        static LoggerService()
        {
        }

        public LoggerService()
        {
            _fileService = LocatorService.FileService;

            var config = LocatorService.ConfigService;
            _loggerConfig = config.LoggerConfig;
        }

        public void CreateLog(string message)
        {
            var log = $"{DateTime.UtcNow}:{message}";

            _generalLog.AppendLine(log);

            _logCounter++;

            if (_logCounter == _loggerConfig.BackUpCount)
            {
                Console.WriteLine(_logCounter);
                CreateBackUp();

                _logCounter = 0;
            }
        }

        private void CreateBackUp()
        {
            Console.WriteLine("CreateBackUp");

            var dirPath = _loggerConfig.DirectoryPath;

            var fileName = $"{DateTime.UtcNow.ToString(_loggerConfig.NameFormat)}";
            var filePath = $"{dirPath}{fileName}{_loggerConfig.ExtensionFile}";

            _fileService.WriteToFile(filePath, _generalLog.ToString());
        }
    }
}
