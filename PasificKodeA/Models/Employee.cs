namespace PasificKodeA.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Employee
    {
        public int EmployeeId { get; set; }       // PK

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        // Age is calculated from DateOfBirth on the fly so it cannot get out of sync with DOB
        public int Age
        {
            get
            {
                var now = DateTime.Now;
                var age = now.Year - DateOfBirth.Year;
                if (DateOfBirth.Date > now.AddYears(-age))
                    age--;
                return age;
            }
        }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Salary { get; set; }

        [Required]
        public int DepartmentId { get; set; }     // FK
    }
}