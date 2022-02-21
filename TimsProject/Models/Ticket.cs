using System;
using System.ComponentModel.DataAnnotations;

namespace TimsProject.Models
{
    public class Tickets
{
      [Key]
     
    [Required]
    
    public int TicketId{get; set;}
   
    public DateTime TicketDate{get; set;}
    [Required]
    
    public string Description { get; set;}
    [Required]
    
    public int EmployeeId{get; set;}
   
    public string ApprovalStatus{get; set;}
   
    public string TicketResponse{get; set;}
}
}