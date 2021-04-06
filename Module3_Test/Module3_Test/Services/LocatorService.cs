using Module3_Test.Services;
using Module3_Test.Services.Abstractions;

namespace Module3_Test
{
    public static class LocatorService
    {
        private static readonly LoggerService _loggerService = new LoggerService();

        public static ILoggerService LoggerService => _loggerService;

        public static IFileService FileService => new FileService();

        public static IConfigService ConfigService => new ConfigService();
    }
}
