using System.Collections.Generic;
using System.Linq;
using TimsProject.Models;

namespace TimsProject.Infrastructure
{
    public class UserRoleRepository : ICRUDRepository<UserRoles, int>
    {
        TimsDbContext _Db;
        public UserRoleRepository(TimsDbContext db)
        {
            _Db=db;
        }
        

        public void Create(UserRoles item)
        {
           // throw new NotImplementedException();
             _Db.UserRoles.Add(item);
            _Db.SaveChanges();
        }

        public void Delete(int id)
        {
           // throw new NotImplementedException();
            var obj = _Db.UserRoles.FirstOrDefault(c=>c.UserId==id);
            if(obj==null)
                return;  
            _Db.UserRoles.Remove(obj);
            _Db.SaveChanges();
        }

        public IEnumerable<UserRoles> GetAll()
        {
           // throw new NotImplementedException();
           return _Db.UserRoles.ToList();
        }

        public UserRoles GetDetails(int id)
        {
            return _Db.UserRoles.FirstOrDefault(c=>c.UserId==id);
        }

        public void Update(UserRoles item)
        {
            //throw new NotImplementedException();
            
             var obj = _Db.UserRoles.FirstOrDefault(c=>c.UserId==item.UserId);

            if(obj==null)

                return;

            obj.UserId= item.UserId;

            obj.RoleId = item.RoleId;

           

           
            _Db.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            _Db.SaveChanges();
        }
         

    }
}
