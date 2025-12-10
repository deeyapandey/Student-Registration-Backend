using System.ComponentModel.DataAnnotations;

namespace StudentRegistrationAPI.Models
{
    public enum GenderType { Male, Female, Other }
    public enum ResidenceType { Hosteller, DayScholar}
    public enum TransportationType { Walk, Bicycle, Bus, PrivateVehicle }
    public enum FeeCategoryType { Regular, SelfFinanced, Scholarship, Quota }
    public enum AddressTypeEnum { Permanent, Temporary }
    public enum ParentTypeEnum { Father, Mother, LegalGuardian }
    public enum AcademicStatusType { Active, OnHold, Completed, DroppedOut }

    public class Student
    {
        public int StudentId { get; set; }

        [Required] public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        [Required] public string LastName { get; set; } = string.Empty;
        [Required] public DateTime DateOfBirth { get; set; }
        public string? PlaceOfBirth { get; set; }

        [Required] public int NationalityId { get; set; }
        public Nationality? Nationality { get; set; }
        [Required] public string CitizenshipNumber { get; set; } = string.Empty;
        [Required] public DateTime CitizenshipIssueDate { get; set; }
        [Required] public string CitizenshipIssueDistrict { get; set; } = string.Empty;
        [Required, EmailAddress] public string Email { get; set; } = string.Empty;
        [EmailAddress] public string? AlternateEmail { get; set; }
        [Required] public string PrimaryMobile { get; set; } = string.Empty;
        public string? SecondaryMobile { get; set; }
        [Required] public string EmergencyContactName { get; set; } = string.Empty;
        [Required] public string EmergencyContactRelation { get; set; } = string.Empty;
        [Required] public string EmergencyContactNumber { get; set; } = string.Empty;
        [Required] public GenderType Gender { get; set; }

        public int? BloodGroupId { get; set; }
        public BloodGroup? BloodGroup { get; set; }
        public int? MaritalStatusId { get; set; }
        public MaritalStatus? MaritalStatus { get; set; }


        public string? Religion { get; set; }
        [Required] public string EthnicityCaste { get; set; } = string.Empty;

        [Required] public int DisabilityStatusId { get; set; }
        public DisabilityStatus? DisabilityStatus { get; set; }

        public string? DisabilityTypeSpecify { get; set; }
        public int? DisabilityPercentage { get; set; }
        public string? AnnualFamilyIncome { get; set; }
        [Required] public  ResidenceType ResidenceType { get; set; }
        public TransportationType? TransportationMethod { get; set; }
        public string? ExtracurricularInterests { get; set; }
        [Required] public bool DeclarationAccepted { get; set; }
        [Required] public string Place { get; set; } = string.Empty;
        public DateTime DateOfApplication { get; set; } = DateTime.UtcNow;

        public List<Address> Addresses { get; set; } = new();
        public List<Parent> Parents { get; set; } = new();
        public Enrollment? Enrollment { get; set; }
        public List<AcademicHistory> PreviousAcademics { get; set; } = new();
        public List<FileUpload> Files { get; set; } = new();
        public FinancialDetail? Financial { get; set; }
        public List<ExtracurricularAward> Awards { get; set; } = new();
    }

    public class Address
    {
        public int AddressId { get; set; }
        public int StudentId { get; set; }
        [Required] public AddressTypeEnum AddressType { get; set; }

        [Required] public int ProvinceId { get; set; }
        public Province? Province { get; set; }

        [Required]public int DistrictId { get; set; }
        public District? District { get; set; }

        [Required]public int MunicipalityId { get; set; }
        public Municipality? Municipality { get; set; }

        [Required] public string WardNumber { get; set; } = string.Empty;
        public string? Street { get; set; }
        public string? HouseNumber { get; set; }
        public Student? Student { get; set; }
    }

    public class Parent
    {
        public int ParentId { get; set; }
        public int StudentId { get; set; }
        [Required] public ParentTypeEnum ParentType { get; set; }
        [Required] public string FullName { get; set; } = string.Empty;
        public string? Occupation { get; set; }
        public string? Designation { get; set; }
        public string? Organization { get; set; }
        [Required] public string MobileNumber { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Relation { get; set; }
        public Student? Student { get; set; }
    }

    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public int StudentId { get; set; }
        [Required] public string Faculty { get; set; } = string.Empty;
        [Required] public string Program { get; set; } = string.Empty;
        [Required] public string CourseLevel { get; set; } = string.Empty;
        [Required] public string AcademicYear { get; set; } = string.Empty;
        [Required] public string SemesterClass { get; set; } = string.Empty;
        public string? Section { get; set; }
        [Required] public string RollNumber { get; set; } = string.Empty;
        [Required] public string RegistrationNumber { get; set; } = string.Empty;
        [Required] public DateTime EnrollDate { get; set; }
        public AcademicStatusType AcademicStatus { get; set; }
        public Student? Student { get; set; }
    }

    public class AcademicHistory
    {
        public int AcademicHistoryId { get; set; }
        public int StudentId { get; set; }
        [Required] public string Qualification { get; set; } = string.Empty;
        [Required] public string BoardUniversity { get; set; } = string.Empty;
        [Required] public string Institution { get; set; } = string.Empty;
        [Required] public int PassedYear { get; set; }
        [Required] public string DivisionGPA { get; set; } = string.Empty;
        public string? MarksheetDocumentPath { get; set; }
        public Student? Student { get; set; }
    }

    public class FinancialDetail
    {
        public int FinancialDetailId { get; set; }
        public int StudentId { get; set; }
        [Required] public FeeCategoryType FeeCategory { get; set; }
        public string? ScholarshipType { get; set; }
        public string? ScholarshipProvider { get; set; }
        public decimal? ScholarshipAmount { get; set; }
        [Required] public string AccountHolderName { get; set; } = string.Empty;
        [Required] public string BankName { get; set; } = string.Empty;
        [Required] public string AccountNumber { get; set; } = string.Empty;
        [Required] public string Branch { get; set; } = string.Empty;
        public Student? Student { get; set; }
    }

    public class ExtracurricularAward
    {
        public int ExtracurricularAwardId { get; set; }
        public int StudentId { get; set; }
        [Required] public string TitleOfAward { get; set; } = string.Empty;
        public string? IssuingOrganization { get; set; }
        public int? YearReceived { get; set; }
        public string? CertificatePath { get; set; }
        public Student? Student { get; set; }
    }

    public class FileUpload
    {
        public int FileUploadId { get; set; }
        public int StudentId { get; set; }
        [Required]
        public string FileType { get; set; } = string.Empty;
        [Required]
        
        public string FilePath { get; set; } = string.Empty;
        public Student? Student { get; set; }
    }
}
