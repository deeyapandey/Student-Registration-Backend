namespace StudentRegistrationAPI.Models
{
    public class Nationality
    {
        public int NationalityId { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<Student> Students { get; set; } = new();
    }
}
