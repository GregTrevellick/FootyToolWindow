using System.Runtime.CompilerServices;

namespace FootieData.Vsix
{
    public sealed partial class ToolWindow1Package
    {
        public interface ITextWriterService
        {
            System.Threading.Tasks.Task WriteLineAsync(string path, string line);
            TaskAwaiter GetAwaiter();
        }
    }
}
