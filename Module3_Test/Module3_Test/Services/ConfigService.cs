using Module3_Test.Configs;
using Module3_Test.Services.Abstractions;
using Newtonsoft.Json;

namespace Module3_Test.Services
{
    public class ConfigService : IConfigService
    {
        private readonly string _filePath = "config.json";
        private readonly LoggerConfig _loggerConfig;
        private readonly IFileService _fileService;

        public ConfigService()
        {
            _fileService = LocatorService.FileService;

            var config = GetConfig();
            _loggerConfig = config.Logger;
        }

        public LoggerConfig LoggerConfig => _loggerConfig;

        private Config GetConfig()
        {
            var json = _fileService.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<Config>(json);
        }
    }
}
