using System.Collections.Generic;
using System.Linq;
using TimsProject.Models;

namespace TimsProject.Infrastructure
{
    public class UserRepository : ICRUDRepository<Users, int>
    {
        TimsDbContext _Db;
        public UserRepository(TimsDbContext db)
        {
            _Db=db;
        }
        

        public void Create(Users item)
        {
           // throw new NotImplementedException();
             _Db.Users.Add(item);
            _Db.SaveChanges();
        }

        public void Delete(int id)
        {
           // throw new NotImplementedException();
            var obj = _Db.Users.FirstOrDefault(c=>c.Id==id);
            if(obj==null)
                return;  
            _Db.Users.Remove(obj);
            _Db.SaveChanges();
        }

        public IEnumerable<Users> GetAll()
        {
           // throw new NotImplementedException();
           return _Db.Users.ToList();
        }

        public Users GetDetails(int id)
        {
            return _Db.Users.FirstOrDefault(c=>c.Id==id);
        }

        public void Update(Users item)
        {
            //throw new NotImplementedException();
            
             var obj = _Db.Users.FirstOrDefault(c=>c.Id==item.Id);

            if(obj==null)

                return;

            obj.Id= item.Id;

            obj.UserName = item.UserName;

            obj.Password = item.Password;

           
            _Db.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            _Db.SaveChanges();
        }
        

    }
}
