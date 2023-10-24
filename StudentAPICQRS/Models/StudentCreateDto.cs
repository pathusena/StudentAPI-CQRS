namespace StudentAPICQRS.Models
{
    public class StudentCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Age { get; set; }
    }
}
