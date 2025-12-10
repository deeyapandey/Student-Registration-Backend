using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudentRegistrationAPI.Models;
using System.Linq;

namespace StudentRegistrationAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // DbSets
        public DbSet<Student> Students { get; set; } = default!;
        public DbSet<Address> Addresses { get; set; } = default!;
        public DbSet<Parent> Parents { get; set; } = default!;
        public DbSet<Enrollment> Enrollments { get; set; } = default!;
        public DbSet<AcademicHistory> AcademicHistories { get; set; } = default!;
        public DbSet<FinancialDetail> FinancialDetails { get; set; } = default!;
        public DbSet<ExtracurricularAward> ExtracurricularAwards { get; set; } = default!;
        public DbSet<FileUpload> FileUploads { get; set; } = default!;

        public DbSet<Nationality> Nationalities { get; set; }

        public DbSet<BloodGroup> BloodGroups { get; set; }

        public DbSet<Province> Provinces { get; set; }

        public DbSet<District> Districts { get; set; }

        public DbSet<Municipality> Municipalities { get; set; }

        public DbSet<MaritalStatus> MaritalStatuses { get; set; } = default!;

        public DbSet<DisabilityStatus> DisabilityStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =====================
            // One-to-One relations
            // =====================

            // Student -> Enrollment
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Enrollment)
                .WithOne(e => e.Student)
                .HasForeignKey<Enrollment>(e => e.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Student -> Financial
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Financial)
                .WithOne(f => f.Student)
                .HasForeignKey<FinancialDetail>(f => f.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // =====================
            //  One-to-Many relations
            // =====================

            modelBuilder.Entity<Student>()
                .HasMany(s => s.Addresses)
                .WithOne(a => a.Student)
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Student>()
                .HasMany(s => s.Parents)
                .WithOne(p => p.Student)
                .HasForeignKey(p => p.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Student>()
                .HasMany(s => s.PreviousAcademics)
                .WithOne(a => a.Student)
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Student>()
                .HasMany(s => s.Awards)
                .WithOne(a => a.Student)
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Student>()
                .HasMany(s => s.Files)
                .WithOne(f => f.Student)
                .HasForeignKey(f => f.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // =====================
            //  Location relations
            // =====================

            // Province -> Districts
            modelBuilder.Entity<Province>()
                .HasMany(p => p.Districts)
                .WithOne(d => d.Province)
                .HasForeignKey(d => d.ProvinceId)
                .OnDelete(DeleteBehavior.Cascade);

            // District -> Municipalities
            modelBuilder.Entity<District>()
                .HasMany(d => d.Municipalities)
                .WithOne(m => m.District)
                .HasForeignKey(m => m.DistrictId)
                .OnDelete(DeleteBehavior.Cascade);

            //Address -> Province/District/Municipality
            modelBuilder.Entity<Address>()
                .HasOne(a => a.Province)
                .WithMany()
                .HasForeignKey(a => a.ProvinceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Address>()
                .HasOne(a=>a.District)
                .WithMany()
                .HasForeignKey(a=>a.DistrictId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Address>()
                .HasOne(a=>a.Municipality)
                .WithMany()
                .HasForeignKey(a=>a.MunicipalityId)
                .OnDelete(DeleteBehavior.Restrict);


            // =====================
            //  Enum to string conversion
            // =====================

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var enumProps = entityType.GetProperties().Where(p => p.ClrType.IsEnum);
                foreach (var prop in enumProps)
                {
                    var converterType = typeof(EnumToStringConverter<>).MakeGenericType(prop.ClrType);
                    var converter = (ValueConverter)Activator.CreateInstance(converterType, new ConverterMappingHints())!;
                    prop.SetValueConverter(converter);
                }
            }

            // =====================
            // Additional configurations
            // =====================

            // Addresses: ensure all foreign keys exist
            modelBuilder.Entity<Address>()
                .Property(a => a.ProvinceId)
                .IsRequired();

            modelBuilder.Entity<Address>()
                .Property(a => a.DistrictId)
                .IsRequired();

            modelBuilder.Entity<Address>()
                .Property(a => a.MunicipalityId)
                .IsRequired();

            // Parents: ensure required fields
            modelBuilder.Entity<Parent>()
                .Property(p => p.FullName)
                .IsRequired();

            modelBuilder.Entity<Parent>()
                .Property(p => p.MobileNumber)
                .IsRequired();
        }

    }
}
