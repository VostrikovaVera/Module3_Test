using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Module3_Test.Services.Abstractions;

namespace Module3_Test
{
    public class Starter
    {
        private SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);
        private ILoggerService _loggerService;

        public Starter()
        {
            _loggerService = LocatorService.LoggerService;
        }

        public void Run()
        {
            File.WriteAllText("test.txt", "aaaaaaaaaa");

            // На события Logger’a подписан класс Starter
            Task.Run(async () =>
            {
                for (int i = 0; i < 50; i++)
                {
                    await WriteAsync($"Log number {i}");
                }
            });

            Task.Run(async () =>
            {
                for (int i = 51; i < 100; i++)
                {
                    await WriteAsync($"Log number {i}");
                }
            });

            Console.ReadKey();
        }

        private async Task WriteAsync(string text)
        {
            await _semaphoreSlim.WaitAsync();

            // await _streamWriter.WriteLineAsync(text);
            _loggerService.CreateLog(text);

            Console.WriteLine(text);

            _semaphoreSlim.Release();
        }
    }
}