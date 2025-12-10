using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders.Physical;
using StudentRegistrationAPI.DTOs;
using StudentRegistrationAPI.Helpers;
using StudentRegistrationAPI.Models;
using StudentRegistrationAPI.Repositories.Interfaces;
using StudentRegistrationAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Threading.Tasks;

namespace StudentRegistrationAPI.Services.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // -------------------------
        // Register a new student
        // -------------------------
        public async Task<Student> RegisterStudentAsync(StudentRegistrationDto dto, string uploadRoot)
        {
            var student = StudentMapper.ToModel(dto);

            // Handle file uploads
            await HandleFilesAsync(student, dto, uploadRoot);

            // Save student to database
            await _unitOfWork.Students.AddAsync(student);
            await _unitOfWork.SaveChangesAsync();

            return student;
        }

        // -------------------------
        // Update existing student
        // -------------------------
        public async Task<Student?> UpdateStudentAsync(int id, StudentRegistrationDto dto, string uploadRoot)
        {
            // Load existing student with all related entities
            var student = await _unitOfWork.Students.GetStudentWithDetailsAsync(id);
            if (student == null) return null;

            // Map DTO → temporary Student object
            var updatedStudent = StudentMapper.ToModel(dto);

            // Update scalar fields
            UpdateScalarFields(student, updatedStudent);

            // Update nested collections
            if (dto.Addresses != null)
            {
                foreach (var a in dto.Addresses)
                {
                    var existingAddress = student.Addresses
                        .FirstOrDefault(x => x.AddressId == a.AddressId);

                    if (existingAddress != null)
                    {
                        // User wants to update this address
                        existingAddress.AddressType = a.AddressType;
                        existingAddress.ProvinceId = a.ProvinceId;
                        existingAddress.DistrictId = a.DistrictId;
                        existingAddress.MunicipalityId = a.MunicipalityId;
                        existingAddress.WardNumber = a.WardNumber;
                        existingAddress.Street = a.Street;
                        existingAddress.HouseNumber = a.HouseNumber;
                    }
                    else
                    {
                        // New address added by user
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
            }

            if (dto.Parents != null)
            {
              
                foreach (var p in dto.Parents)
                {
                    var existingParents = student.Parents
                        .FirstOrDefault(x => x.ParentId == p.ParentId);
                    if (existingParents != null)
                    {
                        existingParents.ParentType = p.ParentType;
                        existingParents.FullName = p.FullName;
                        existingParents.Occupation = p.Occupation;
                        existingParents.Designation = p.Designation;
                        existingParents.Organization = p.Organization;
                        existingParents.MobileNumber = p.MobileNumber;
                        existingParents.Email = p.Email;
                        existingParents.Relation = p.Relation;
                    }
                    else
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
            }
            if (dto.PreviousAcademics != null)
            {
                
                foreach (var a in dto.PreviousAcademics)
                {
                    var existingPreviousAcademics = student.PreviousAcademics
                        .FirstOrDefault(x => x.AcademicHistoryId == a.AcademicHistoryId);
                    if (existingPreviousAcademics != null)
                    {
                        existingPreviousAcademics.Qualification = a.Qualification;
                        existingPreviousAcademics.BoardUniversity = a.BoardUniversity;
                        existingPreviousAcademics.Institution = a.Institution;
                        existingPreviousAcademics.PassedYear = a.PassedYear;
                        existingPreviousAcademics.DivisionGPA = a.DivisionGPA;
                    }
                    else
                    {
                        student.PreviousAcademics.Add(new AcademicHistory
                        {
                            Qualification = a.Qualification,
                            BoardUniversity = a.BoardUniversity,
                            Institution = a.Institution,
                            PassedYear = a.PassedYear,
                            DivisionGPA = a.DivisionGPA
                        });
                    }

                        
                }
            }
            student.Enrollment = updatedStudent.Enrollment ?? student.Enrollment;
            student.Financial = updatedStudent.Financial ?? student.Financial;

            // Handle files & awards
            await HandleFilesAsync(student, dto, uploadRoot);

            // Save changes
            await _unitOfWork.SaveChangesAsync();
            return student;
        }

        // -------------------------
        // Delete student
        // -------------------------
        public async Task<bool> DeleteStudentAsync(int id)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(id);
            if (student == null) return false;

            _unitOfWork.Students.Delete(student);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        // -------------------------
        // Get all students
        // -------------------------
        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _unitOfWork.Students.GetAllAsync();
        }

        // -------------------------
        // Get student by ID with details
        // -------------------------
        public async Task<Student?> GetStudentAsync(int id)
        {
            return await _unitOfWork.Students.GetStudentWithDetailsAsync(id);
        }

        // -------------------------
        // Helper: Update scalar fields
        // -------------------------
        private void UpdateScalarFields(Student target, Student source)
        {
            target.FirstName = source.FirstName;
            target.MiddleName = source.MiddleName;
            target.LastName = source.LastName;
            target.DateOfBirth = source.DateOfBirth;
            target.PlaceOfBirth = source.PlaceOfBirth;
            target.NationalityId = source.NationalityId;
            target.CitizenshipNumber = source.CitizenshipNumber;
            target.CitizenshipIssueDate = source.CitizenshipIssueDate;
            target.CitizenshipIssueDistrict = source.CitizenshipIssueDistrict;
            target.Email = source.Email;
            target.AlternateEmail = source.AlternateEmail;
            target.PrimaryMobile = source.PrimaryMobile;
            target.SecondaryMobile = source.SecondaryMobile;
            target.EmergencyContactName = source.EmergencyContactName;
            target.EmergencyContactRelation = source.EmergencyContactRelation;
            target.EmergencyContactNumber = source.EmergencyContactNumber;
            target.Gender = source.Gender;
            target.BloodGroupId = source.BloodGroupId;
            target.MaritalStatusId = source.MaritalStatusId;
            target.Religion = source.Religion;
            target.EthnicityCaste = source.EthnicityCaste;
            target.DisabilityStatusId = source.DisabilityStatusId;
            target.DisabilityTypeSpecify = source.DisabilityTypeSpecify;
            target.DisabilityPercentage = source.DisabilityPercentage;
            target.AnnualFamilyIncome = source.AnnualFamilyIncome;
            target.ResidenceType = source.ResidenceType;
            target.TransportationMethod = source.TransportationMethod;
            target.ExtracurricularInterests = source.ExtracurricularInterests;
            target.DeclarationAccepted = source.DeclarationAccepted;
            target.Place = source.Place;
            

        }

        // -------------------------
        // Helper: Handle files and awards
        // -------------------------
        private async Task HandleFilesAsync(Student student, StudentRegistrationDto dto, string uploadRoot)
        {
            var uploadPath = Path.Combine(uploadRoot, "StudentFiles");
            if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);


            
            async Task<string> SaveFileAsync(IFormFile file, string docType)
            {
                var ext = Path.GetExtension(file.FileName);
                var uniqueName = $"{docType}_{student.CitizenshipNumber}_{Path.GetRandomFileName()}{ext}";
                var filePath = Path.Combine(uploadPath, uniqueName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(stream);

                return $"/Uploads/StudentFiles/{uniqueName}";
            }

            // Save general files
            if (dto.Files != null)
            {
                // Loop through all file types sent from frontend
                foreach (var fileDto in dto.Files)
                {
                    // Check if this file type already exists
                    var existingFile = student.Files
                        .FirstOrDefault(f => f.FileType == fileDto.FileType);

                    // CASE 1: User uploaded a new file -> save + replace
                    if (fileDto.File != null)
                    {
                        var savedPath = await SaveFileAsync(fileDto.File, fileDto.FileType);

                        if (existingFile != null)
                        {
                            existingFile.FilePath = savedPath; // replace file
                        }
                        else
                        {
                            student.Files.Add(new FileUpload
                            {
                                FileType = fileDto.FileType,
                                FilePath = savedPath
                            });
                        }
                    }

                    // CASE 2: No new file uploaded -> KEEP existing file
                    else
                    {
                        if (existingFile != null)
                        {
                            // keep old
                            existingFile.FilePath = existingFile.FilePath;
                        }
                        else if(fileDto.ExistingFilePath!=null)
                        {
                            // frontend sent previous path
                            student.Files.Add(new FileUpload
                            {
                                FileType=fileDto.FileType,
                                FilePath = fileDto.ExistingFilePath
                            });
                        }
                    }
                }
            }

            

            // Save awards
            if (dto.Awards != null)
            {
                foreach (var awardDto in dto.Awards)
                {
                    // Try to find a matching award
                    var existingAward = student.Awards
                        .FirstOrDefault(a =>
                            a.TitleOfAward == awardDto.TitleOfAward &&
                            a.IssuingOrganization == awardDto.IssuingOrganization &&
                            a.YearReceived == awardDto.YearReceived
                        );

                    string? certPath = existingAward?.CertificatePath;

                    if (awardDto.CertificateFile != null)
                        certPath = await SaveFileAsync(awardDto.CertificateFile, "AwardCert");

                    if (existingAward != null)
                    {
                        // Update certificate if uploaded
                        existingAward.CertificatePath = certPath;
                    }
                    else
                    {
                        student.Awards.Add(new ExtracurricularAward
                        {
                            TitleOfAward = awardDto.TitleOfAward,
                            IssuingOrganization = awardDto.IssuingOrganization,
                            YearReceived = awardDto.YearReceived,
                            CertificatePath = certPath
                        });
                    }
                }

                // Remove awards that are not in dto anymore
                student.Awards.RemoveAll(a =>
                    !dto.Awards.Any(d =>
                        d.TitleOfAward == a.TitleOfAward &&
                        d.IssuingOrganization == a.IssuingOrganization &&
                        d.YearReceived == a.YearReceived
                    )
                );
            }

        }
    }
}
