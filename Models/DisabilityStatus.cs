namespace StudentRegistrationAPI.Models
{
    public class DisabilityStatus
    {
        public int DisabilityStatusId { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<Student> Students { get; set; } = new();
    }
}
