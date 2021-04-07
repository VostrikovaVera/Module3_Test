using System.Threading.Tasks;

namespace Module3_Test.Services.Abstractions
{
    public interface IFileService
    {
        string ReadAllText(string path);

        void WriteToFile(string path, string text);
    }
}
