using System;
using System.ComponentModel.DataAnnotations;

namespace TimsProject.Models
{
    public class UserRoles
    {
          [Key]

         [Required] 
        
         public int UserId{get; set;}
         [Required] 
        
         public int RoleId{get; set;}
    }
}