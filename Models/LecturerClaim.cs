using System.ComponentModel.DataAnnotations;

namespace W98.Models
{
    public class LecturerClaim
    {
        public  bool? IsApproved;

        public int ID { get; set; }  // Primary Key

        [Required]
        public string Name { get; set; }  // Lecturer's first name

        [Required]
        public string Surname { get; set; }  // Lecturer's surname

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Hourly rate must be a positive number")]
        public decimal HourlyRate { get; set; }  // Hourly rate of the lecturer

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Hours worked must be a positive number")]
        public decimal HoursWorked { get; set; }  // Total hours worked by the lecturer
        [Required]
        public string ModuleCode { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }  // Short description of the claim
        public string Status { get; set; } = "Pending"; // Default status
        public string? Feedback { get; set; } // Feedback from Programme Manager
    }
}
