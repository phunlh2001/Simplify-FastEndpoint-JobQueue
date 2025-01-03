namespace TaskProcessingSystem.Services.File
{
    public interface IFileHandler
    {
        void Write<T>(T obj, string filePath);
    }
}
