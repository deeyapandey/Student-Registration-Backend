using Microsoft.AspNetCore.Http;
using StudentRegistrationAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentRegistrationAPI.DTOs
{
    public class StudentRegistrationDto
    {
        // Personal & Biometric Details
        [Required] public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        [Required] public string LastName { get; set; } = string.Empty;
        [Required] public DateTime DateOfBirth { get; set; }
        public string? PlaceOfBirth { get; set; }
        [Required] public int NationalityId { get; set; }
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
        public int? BloodGroupId  { get; set; }
        public int? MaritalStatusId { get; set; }
        public string? Religion { get; set; }
        [Required] public string EthnicityCaste { get; set; } = string.Empty;
        [Required] public int DisabilityStatusId { get; set; }
        public string? DisabilityTypeSpecify { get; set; }
        public int? DisabilityPercentage { get; set; }
        public string? AnnualFamilyIncome { get; set; }
        [Required] public ResidenceType ResidenceType { get; set; }
        public TransportationType? TransportationMethod { get; set; }
        public string? ExtracurricularInterests { get; set; }
        [Required] public bool DeclarationAccepted { get; set; }
        [Required] public string Place { get; set; } = string.Empty;

        // Nested collections
        public List<AddressDto>? Addresses { get; set; }
        public List<ParentDto>? Parents { get; set; }
        public EnrollmentDto? Enrollment { get; set; }
        public FinancialDetailDto? Financial { get; set; }
        public List<AcademicHistoryDto>? PreviousAcademics { get; set; }
        public List<FileUploadDto>? Files { get; set; }
        public List<ExtracurricularAwardDto>? Awards { get; set; }
    }

    public class AddressDto
    {
        public int? AddressId { get; set; }
        [Required] public AddressTypeEnum AddressType { get; set; }
        [Required] public int ProvinceId { get; set; }
        [Required] public int DistrictId { get; set; } 
        [Required] public int MunicipalityId { get; set; }
        [Required] public string WardNumber { get; set; } = string.Empty;
        public string? Street { get; set; }
        public string? HouseNumber { get; set; }
    }

    public class ParentDto
    {
        public int? ParentId { get; set; }
        [Required] public ParentTypeEnum ParentType { get; set; }
        [Required] public string FullName { get; set; } = string.Empty;
        public string? Occupation { get; set; }
        public string? Designation { get; set; }
        public string? Organization { get; set; }
        [Required] public string MobileNumber { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Relation { get; set; }
    }

    public class EnrollmentDto
    {

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
    }

    public class FinancialDetailDto
    {
        [Required] public FeeCategoryType FeeCategory { get; set; }
        public string? ScholarshipType { get; set; }
        public string? ScholarshipProvider { get; set; }
        public decimal? ScholarshipAmount { get; set; }
        [Required] public string AccountHolderName { get; set; } = string.Empty;
        [Required] public string BankName { get; set; } = string.Empty;
        [Required] public string AccountNumber { get; set; } = string.Empty;
        [Required] public string Branch { get; set; } = string.Empty;
    }

    public class AcademicHistoryDto
    {
        public int? AcademicHistoryId { get; set; }
        [Required] public string Qualification { get; set; } = string.Empty;
        [Required] public string BoardUniversity { get; set; } = string.Empty;
        [Required] public string Institution { get; set; } = string.Empty;
        [Required] public int PassedYear { get; set; }
        [Required] public string DivisionGPA { get; set; } = string.Empty;
    }

    public class FileUploadDto
    {
        [Required] public string FileType { get; set; } = string.Empty;
        public IFormFile? File { get; set; }

        // NEW → keeps old file path when no new file is uploaded
        public string? ExistingFilePath { get; set; }
    }

    public class ExtracurricularAwardDto
    {
        [Required] public string TitleOfAward { get; set; } = string.Empty;
        public string? IssuingOrganization { get; set; }
        public int? YearReceived { get; set; }
        public IFormFile? CertificateFile { get; set; }
    }
}
