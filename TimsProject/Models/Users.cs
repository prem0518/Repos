using System;
using System.ComponentModel.DataAnnotations;

namespace TimsProject.Models
{
    public class Users
    {
          [Key]
       [Required]
   
       public int Id{get; set;}
        //public string Name{get; set;}
       [Required]
        [StringLength(maximumLength:10, ErrorMessage = "Maximum length exceeded.")]
        public string UserName{get; set;}
       [Required]
        [StringLength(maximumLength:10, ErrorMessage = "Maximum length exceeded.")]
        public string Password{get; set;}
    }
}