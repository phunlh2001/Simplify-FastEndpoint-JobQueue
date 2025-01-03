namespace TaskProcessingSystem.Models
{
    public class Task
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? CreatedAt { get; set; }

        public override string ToString()
        {
            return $"- Id: {Id}\n" +
                $"- Name: {Name}\n" +
                $"- Description: {Description}\n" +
                $"- CreatedAt: {CreatedAt}";
        }
    }
}
