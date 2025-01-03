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

        public List<T> ReadList<T>(string path) where T : class, new()
        {
            var items = new List<T>();
            var section = new List<string>();

            using StreamReader sr = new(path);
            string? line;
            while ((line = sr.ReadLine()) != null)
            {
                if (line.Trim() == "----------------")
                {
                    if (section.Count > 0)
                    {
                        var item = ParseSection<T>(section);
                        items.Add(item);
                        section.Clear();
                    }
                }
                else
                {
                    section.Add(line);
                }
            }

            if (section.Count > 0)
            {
                var item = ParseSection<T>(section);
                items.Add(item);
            }
            return items;
        }

        private static T ParseSection<T>(List<string> section) where T : class, new()
        {
            var item = new T();

            foreach (var line in section)
            {
                foreach (var property in typeof(T).GetProperties())
                {
                    string value = line.Split(": ")[1];
                    string propertyName = property.Name;
                    if (line.StartsWith($"- {propertyName}:"))
                    {
                        property.SetValue(item, Convert.ChangeType(value, property.PropertyType));
                    }
                }
            }

            return item;
        }
    }
}
