using System.ComponentModel.DataAnnotations;

namespace EFCodeFirstDemo.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        [Required]
        [StringLength(50)]
        public string DepartmentName { get; set; }
        public string DepartmentDescription { get; set; }
        public virtual ICollection<Employee> Employees {get;set;}
    }
}
