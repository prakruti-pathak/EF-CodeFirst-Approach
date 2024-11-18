using System.ComponentModel.DataAnnotations;

namespace EFCodeFirstDemo.Dtos
{
    public class AddEmployeeDto
    {
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
    }
}
