using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimsProject.Models
{
    public class ServiceBusMessageData
    {
        

        public string EmployeeName { get; set; }
        public DateTime EmployeeJoinDate { get; set; }
        public int EmployeeDepartmentId { get; set; }
        public int TicketId { get; set; }
        public DateTime TicketDate { get; set; }
        public string ApprovalStatus { get; set; }
        public string action { get; set; }
      
        public string actionMessage { get; set; }
    }
}
