namespace TaskProcessingSystem.DTOs
{
    public class ResInfo<T> where T : class
    {
        public string? Message { get; set; }
        public T? Info { get; set; }
    }
}
