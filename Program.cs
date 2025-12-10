using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using StudentRegistrationAPI.Data;
using StudentRegistrationAPI.Models;
using StudentRegistrationAPI.Repositories.Implementations;
using StudentRegistrationAPI.Repositories.Interfaces;
using StudentRegistrationAPI.Services.Implementations;
using StudentRegistrationAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers()
.AddJsonOptions(options =>
 {
     options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
     options.JsonSerializerOptions.ReferenceHandler =
         System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
     options.JsonSerializerOptions.WriteIndented = true;
 });



// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173", "http://localhost:5174")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//DI
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IStudentService, StudentService>();

var app = builder.Build();

// Create Uploads directory if missing
var uploadPath = Path.Combine(app.Environment.ContentRootPath, "Uploads", "StudentFiles");
if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!db.Nationalities.Any())
    {
        db.Nationalities.AddRange(
            new Nationality { Name = "Nepali" },
            new Nationality { Name = "Indian" },
            new Nationality { Name = "American" }
        );
        db.SaveChanges();
    }
    if (!db.BloodGroups.Any())
    {
        db.BloodGroups.AddRange(
            new BloodGroup { Name = "A+" },
            new BloodGroup { Name = "A-" },
            new BloodGroup { Name = "B+" },
            new BloodGroup { Name = "B-" },
            new BloodGroup { Name = "O+" },
            new BloodGroup { Name = "O-" },
            new BloodGroup { Name = "AB+" },
            new BloodGroup { Name = "AB-" }

            );
        db.SaveChanges();
    }

        if (!db.MaritalStatuses.Any())
        {
            db.MaritalStatuses.AddRange(
                new MaritalStatus { Status = "Single" },
                new MaritalStatus { Status = "Married" },
                new MaritalStatus { Status = "Divorced" },
                new MaritalStatus { Status = "Widowed" },
                new MaritalStatus { Status = "Separated" }
            );
            db.SaveChanges();
        }

    if (!db.DisabilityStatuses.Any())
    {
        db.DisabilityStatuses.AddRange(
            new DisabilityStatus { Status = "None" },
            new DisabilityStatus { Status = "Physical" },
            new DisabilityStatus { Status = "Visual" },
            new DisabilityStatus { Status = "Hearing" },
            new DisabilityStatus { Status = "Other" }
            );
        db.SaveChanges();
    }


}

// Middleware
if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Student Registration API v1");
            options.RoutePrefix = "swagger";
        });
    }

    app.UseHttpsRedirection();

    // Serve uploaded files
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(builder.Environment.ContentRootPath, "Uploads", "StudentFiles")
        ),
        RequestPath = "/Uploads/StudentFiles"
    });

    // **CORS must come before Authorization**
    app.UseCors("AllowReact");

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
