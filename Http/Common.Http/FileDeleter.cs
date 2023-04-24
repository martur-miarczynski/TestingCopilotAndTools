using System.IO;
using System.Threading.Tasks;

namespace Common.Http;

public class FileDeleter
{
    public Task DeleteDirectoryAndAllContentAsync(string directoryToDelete)
    {
        return Task.Run(() =>
        {
            if (Directory.Exists(directoryToDelete))
            {
                Directory.Delete(directoryToDelete, true);
            }
        });
    }    
}