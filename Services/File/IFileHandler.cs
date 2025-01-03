namespace TaskProcessingSystem.Services.File
{
    public interface IFileHandler
    {
        List<T> ReadList<T>(string path) where T : class, new();
        void Write<T>(T obj, string filePath);
    }
}
