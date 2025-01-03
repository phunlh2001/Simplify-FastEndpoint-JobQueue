namespace TaskProcessingSystem.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? CreatedAt { get; set; }

        public override string ToString()
        {
            return $"Task {Id}:\n" +
                $"- Name: {Name}\n" +
                $"- Description: {Description}\n" +
                $"- Created at: {CreatedAt}";
        }
    }
}
