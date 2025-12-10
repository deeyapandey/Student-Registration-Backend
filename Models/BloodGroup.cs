using System.Reflection.Metadata.Ecma335;

namespace StudentRegistrationAPI.Models
{
    public class BloodGroup
    {
        public int BloodGroupId { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<Student> Students { get; set; } = new();
    }
}
