using System;
using System.ComponentModel.DataAnnotations;

namespace TimsProject.Models
{
    public class Roles
    {
          [Key]
       [Required]
       
        public int RoleId{get; set;}
        
       [Required] 
        [StringLength(maximumLength:20, ErrorMessage = "Maximum length exceeded.")]
        public string RoleName{get; set;}
        // public string Description{get; set;}
    }
}