using StudentRegistrationAPI.DTOs;
using StudentRegistrationAPI.Models;
using System.Linq;
using System.Collections.Generic;

namespace StudentRegistrationAPI.Helpers
{
    public static class StudentMapper
    {
        public static Student ToModel(StudentRegistrationDto dto)
        {
            var student = new Student
            {
                FirstName = dto.FirstName,
                MiddleName = dto.MiddleName,
                LastName = dto.LastName,
                DateOfBirth = dto.DateOfBirth,
                PlaceOfBirth = dto.PlaceOfBirth,
                NationalityId = dto.NationalityId,
                CitizenshipNumber = dto.CitizenshipNumber,
                CitizenshipIssueDate = dto.CitizenshipIssueDate,
                CitizenshipIssueDistrict = dto.CitizenshipIssueDistrict,
                Email = dto.Email,
                AlternateEmail = dto.AlternateEmail,
                PrimaryMobile = dto.PrimaryMobile,
                SecondaryMobile = dto.SecondaryMobile,
                EmergencyContactName = dto.EmergencyContactName,
                EmergencyContactRelation = dto.EmergencyContactRelation,
                EmergencyContactNumber = dto.EmergencyContactNumber,
                Gender = dto.Gender,
                BloodGroupId = dto.BloodGroupId,
                MaritalStatusId = dto.MaritalStatusId,
                Religion = dto.Religion,
                EthnicityCaste = dto.EthnicityCaste,
                DisabilityStatusId = dto.DisabilityStatusId,
                DisabilityTypeSpecify = dto.DisabilityTypeSpecify,
                DisabilityPercentage = dto.DisabilityPercentage,
                AnnualFamilyIncome = dto.AnnualFamilyIncome,
                ResidenceType = dto.ResidenceType,
                TransportationMethod = dto.TransportationMethod,
                ExtracurricularInterests = dto.ExtracurricularInterests,
                DeclarationAccepted = dto.DeclarationAccepted,
                Place = dto.Place,
                DateOfApplication = System.DateTime.UtcNow,

                // Initialize empty lists to avoid null reference errors
                Addresses = new List<Address>(),
                Parents = new List<Parent>(),
                PreviousAcademics = new List<AcademicHistory>(),
                Files = new List<FileUpload>(),
                Awards = new List<ExtracurricularAward>()
            };

            // -------------------------
            //   Addresses
            // -------------------------
            if (dto.Addresses != null)
            {
                foreach (var a in dto.Addresses)
                {
                    student.Addresses.Add(new Address
                    {
                       
                        AddressType = a.AddressType,
                        ProvinceId = a.ProvinceId,
                        DistrictId = a.DistrictId,
                        MunicipalityId = a.MunicipalityId,
                        WardNumber = a.WardNumber,
                        Street = a.Street,
                        HouseNumber = a.HouseNumber
                    });
                }
            }

            // -------------------------
            //   Parents / Guardians
            // -------------------------
            if (dto.Parents != null)
            {
                foreach (var p in dto.Parents)
                {
                    student.Parents.Add(new Parent
                    {
                        ParentType = p.ParentType,
                        FullName = p.FullName,
                        Occupation = p.Occupation,
                        Designation = p.Designation,
                        Organization = p.Organization,
                        MobileNumber = p.MobileNumber,
                        Email = p.Email,
                        Relation = p.Relation
                    });
                }
            }

            // -------------------------
            //   Academic History
            // -------------------------
            if (dto.PreviousAcademics != null)
            {
                foreach (var ac in dto.PreviousAcademics)
                {
                    student.PreviousAcademics.Add(new AcademicHistory
                    {
                        Qualification = ac.Qualification,
                        BoardUniversity = ac.BoardUniversity,
                        Institution = ac.Institution,
                        PassedYear = ac.PassedYear,
                        DivisionGPA = ac.DivisionGPA
                    });
                }
            }

            // -------------------------
            //   Enrollment
            // -------------------------
            if (dto.Enrollment != null)
            {
                student.Enrollment = new Enrollment
                {
                    Faculty = dto.Enrollment.Faculty,
                    Program = dto.Enrollment.Program,
                    CourseLevel = dto.Enrollment.CourseLevel,
                    AcademicYear = dto.Enrollment.AcademicYear,
                    SemesterClass = dto.Enrollment.SemesterClass,
                    Section = dto.Enrollment.Section,
                    RollNumber = dto.Enrollment.RollNumber,
                    RegistrationNumber = dto.Enrollment.RegistrationNumber,
                    EnrollDate = dto.Enrollment.EnrollDate,
                    AcademicStatus = dto.Enrollment.AcademicStatus
                };
            }

            // -------------------------
            //   Financial Info
            // -------------------------
            if (dto.Financial != null)
            {
                student.Financial = new FinancialDetail
                {
                    FeeCategory = dto.Financial.FeeCategory,
                    ScholarshipType = dto.Financial.ScholarshipType,
                    ScholarshipProvider = dto.Financial.ScholarshipProvider,
                    ScholarshipAmount = dto.Financial.ScholarshipAmount,
                    AccountHolderName = dto.Financial.AccountHolderName,
                    BankName = dto.Financial.BankName,
                    AccountNumber = dto.Financial.AccountNumber,
                    Branch = dto.Financial.Branch
                };
            }

            return student;
        }
    }
}
