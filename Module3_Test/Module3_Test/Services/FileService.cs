using System.IO;
using Module3_Test.Services.Abstractions;

namespace Module3_Test.Services
{
    public class FileService : IFileService
    {
        public string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }

        public void WriteToFile(string path, string text)
        {
            File.WriteAllText(path, text);
        }
    }
}
