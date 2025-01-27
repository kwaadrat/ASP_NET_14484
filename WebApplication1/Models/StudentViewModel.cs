using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Models
{
    public class StudentViewModel
    {
        [HiddenInput]
        public int? Id { get; set; }

        [Required(ErrorMessage = "Name is missing!")]
        public string Name { get; set; }

        [RegularExpression(pattern: ".+\\@.+\\.[a-z]{2,3}", ErrorMessage = "Incorrect email format!")]
        [Required(ErrorMessage = "Email is missing or incorrect!")]
        public string Email { get; set; }

        [MinLength(length: 5, ErrorMessage = "Index number too short!")]
        [MaxLength(length: 6, ErrorMessage = "Index number too long!")]
        [Required(ErrorMessage = "Index number is missing!")]
        public string IndexNumber { get; set; }

        public DateTime? Birth { get; set; }
    }
}
