using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace Assignment.Models
{
    public class Student
    {
        // [Key]
        public int Id { get; set; }

        [Required] 
        [StringLength(50)] 
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Range(18, 100)] // Age between 18 and 100
        public int Age { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }

    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [StringLength(200)]
        public string Description { get; set; } = string.Empty;

        [Range(1, 10)]
        public int Credits { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }

    public class Enrollment
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public Student Student { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course Course { get; set; }

        public DateTime EnrollmentDate { get; set; }
    }
}

