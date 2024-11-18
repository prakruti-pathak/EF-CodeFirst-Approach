using System.ComponentModel.DataAnnotations;

namespace EFCodeFirstDemo.Dtos
{
    public class EmployeeDto
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(12)]
        public string PhoneNumber { get; set; }
        public int DepartmentId { get; set; }

    }
}
