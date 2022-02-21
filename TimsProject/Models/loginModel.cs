using System.ComponentModel.DataAnnotations;

namespace TimsProject.Models
{
    public class User
    {
       [Required] 
        [StringLength(maximumLength:10, ErrorMessage = "Maximum length exceeded.")]
       public string UserName{get; set;}
       [Required] 
        [StringLength(maximumLength:10, ErrorMessage = "Maximum length exceeded.")]
       public string Password{get; set;}
        public  void Deconstruct(out string userName,out string password)
        {
            userName=this.UserName;
            password=this.Password;
        }
       
    }
}