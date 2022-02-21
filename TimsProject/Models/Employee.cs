using System;
using System.ComponentModel.DataAnnotations;

namespace TimsProject.Models
{
    public class Employee
    {
          [Key]
       [Required] 
      
       public int EmployeeId{get; set;}
        [Required]
        [StringLength(maximumLength:20, ErrorMessage = "Maximum length exceeded.")]
        public string EmployeeName{get; set;}
       
        [Required]
        public DateTime EmployeeJoinDate{get; set;}
        [Required]
        public DateTime EmployeeBirthDate{get; set;}
        [Required]
                public int EmployeeDepartmentId{get; set;}
        [Required]
        
        public int EmployeeProjectId{get; set;}
        [Required]
        
        public int ManagerId{get; set;}
    }
}