using System;
using System.Collections.Generic;
using System.Linq;
using TimsProject.Models;

namespace TimsProject.Infrastructure
{
    public class TicketRepository : ICRUDRepository<Tickets, int>
    {
       TimsDbContext _Db;
        public TicketRepository(TimsDbContext db)
        {
            _Db=db;
        }
        public IEnumerable<Tickets> GetOpenTicket()
        {
            //return _Db.Tickets.FirstOrDefault(c=>c.TicketId==id);
            var query=from c in _Db.Tickets where c.EmployeeId==0 || c.ApprovalStatus=="" select c;
            return  query.ToList();

        }

        public void Create(Tickets item)
        {
           // throw new NotImplementedException();
             _Db.Tickets.Add(item);
            _Db.SaveChanges();
        }

         

        public IEnumerable<Tickets> GetAll()
        {
           // throw new NotImplementedException();
           return _Db.Tickets.ToList();
        }

        public Tickets GetDetails(int id)
        {
            return _Db.Tickets.FirstOrDefault(c=>c.TicketId==id);
        }
        // public Tickets GetAssignedTickets(int id)
        //{
        //    return _Db.Tickets.FirstOrDefault(c=>c.TicketId==id);
        //}

         public void Update(Tickets item)
         {
            var obj = _Db.Tickets.FirstOrDefault(c => c.TicketId == item.TicketId);

            if (obj == null)

                return;

            obj.TicketId = item.TicketId;
            obj.TicketDate = item.TicketDate;
            obj.Description = item.Description;
            obj.EmployeeId = item.EmployeeId;
            obj.ApprovalStatus = item.ApprovalStatus;
            obj.TicketResponse = item.TicketResponse;
            _Db.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
           _Db.SaveChanges();

        }    
         public void Delete(int id)
        {
            var obj = _Db.Tickets.FirstOrDefault(c => c.TicketId == id);
            if (obj == null)
                return;
            _Db.Tickets.Remove(obj);
            _Db.SaveChanges();
        }

       
    }
}
