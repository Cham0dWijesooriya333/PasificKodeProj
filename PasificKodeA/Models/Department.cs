using System.ComponentModel.DataAnnotations;

namespace PasificKodeA.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }

        [Required]
        [MaxLength(20)]
        public string DepartmentCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string DepartmentName { get; set; }
    }
}
