namespace StudentRegistrationAPI.Models
{
    public class MaritalStatus
    {
        public int MaritalStatusId { get; set; }
        public string Status { get; set; } = string.Empty;

        public List<Student> Students { get; set; } = new();
    }
}
