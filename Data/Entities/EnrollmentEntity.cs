using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public enum Grade
    {
        A, B, C, D, F
    }

    [Table("enrollment")]
    public class EnrollmentEntity
    {
        [Key]
        public int Id { get; set; }

        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public Grade? Grade { get; set; }

        public StudentEntity Student { get; set; }
        public CourseEntity Course { get; set; }

    }
}
