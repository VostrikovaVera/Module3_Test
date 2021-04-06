using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Module3_Test.Services.Abstractions;

namespace Module3_Test
{
    public class Starter
    {
        private readonly IFileService _fileService;
        private SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);
        private ILoggerService _loggerService;

        public Starter()
        {
            _loggerService = LocatorService.LoggerService;
            _fileService = LocatorService.FileService;
        }

        public void Run()
        {
            _loggerService.LogBackupHandler += CreateBackUp;

            Task.Run(async () =>
            {
                for (int i = 0; i < 51; i++)
                {
                    await WriteAsync($"Log number {i}");
                }
            });

            Task.Run(async () =>
            {
                for (int i = 51; i < 101; i++)
                {
                    await WriteAsync($"Log number {i}");
                }
            });

            Console.ReadKey();
        }

        private async Task WriteAsync(string text)
        {
            await _semaphoreSlim.WaitAsync();

            await Task.Run(() =>
            {
                _loggerService.CreateLog(text);
            });

            _semaphoreSlim.Release();
        }

        private void CreateBackUp(string filePath, string logs)
        {
            _fileService.WriteToFile(filePath, logs);
        }
    }
}