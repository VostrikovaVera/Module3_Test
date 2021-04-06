using System;
using System.Threading.Tasks;

namespace Module3_Test.Services.Abstractions
{
    public interface ILoggerService
    {
        event Action<string, string> LogBackupHandler;

        void CreateLog(string message);
    }
}
