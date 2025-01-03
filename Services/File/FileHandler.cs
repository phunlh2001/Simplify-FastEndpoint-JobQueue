namespace TaskProcessingSystem.Services.File
{
    public class FileHandler : IFileHandler
    {
        public void Write<T>(T obj, string filePath)
        {
            try
            {
                using StreamWriter sw = new(filePath, append: true);
                sw.WriteLine(obj?.ToString());
                sw.WriteLine("----------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
