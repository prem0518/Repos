using System;
using System.Collections.Generic;
using System.Linq;
using TimsProject.Models;

namespace TimsProject.Infrastructure
{
    public class RoleRepository : ICRUDRepository<Roles, int>
    {
        TimsDbContext _Db;
        public RoleRepository(TimsDbContext db)
        {
            _Db=db;
        }
        

        public void Create(Roles item)
        {
           // throw new NotImplementedException();
             _Db.Roles.Add(item);
            _Db.SaveChanges();
        }

        public void Delete(int id)
        {
           // throw new NotImplementedException();
            var obj = _Db.Roles.FirstOrDefault(c=>c.RoleId==id);
            if(obj==null)
                return;  
            _Db.Roles.Remove(obj);
            _Db.SaveChanges();
        }

        public IEnumerable<Roles> GetAll()
        {
           // throw new NotImplementedException();
           return _Db.Roles.ToList();
        }

        public Roles GetDetails(int id)
        {
            return _Db.Roles.FirstOrDefault(c=>c.RoleId==id);
        }

        public void Update(Roles item)
        {
            //throw new NotImplementedException();
            
             var obj = _Db.Roles.FirstOrDefault(c=>c.RoleId==item.RoleId);

            if(obj==null)

                return;

            obj.RoleId = item.RoleId;

            obj.RoleName = item.RoleName;
            

           

            _Db.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            _Db.SaveChanges();
        }
        public void CreateTicket(Roles id)
        {
            throw new NotImplementedException();
    }
    }
}
