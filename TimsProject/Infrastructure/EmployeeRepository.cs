using System.Collections.Generic;
using System.Linq;
using TimsProject.Models;

namespace TimsProject.Infrastructure
{
    public class EmployeeRepository : ICRUDRepository<Employee, int>
    {
        TimsDbContext _Db;
        public EmployeeRepository(TimsDbContext db)
        {
            _Db=db;
        }
        

        public void Create(Employee item)
        {
           // throw new NotImplementedException();
             _Db.Employee.Add(item);
            _Db.SaveChanges();
        }

        public void Delete(int id)
        {
           // throw new NotImplementedException();
            var obj = _Db.Employee.FirstOrDefault(c=>c.EmployeeId==id);
            if(obj==null)
                return;  
            _Db.Employee.Remove(obj);
            _Db.SaveChanges();
        }

        public IEnumerable<Employee> GetAll()
        {
           // throw new NotImplementedException();
           return _Db.Employee.ToList();
        }

        public Employee GetDetails(int id)
        {
            return _Db.Employee.FirstOrDefault(c=>c.EmployeeId==id);
        }

        public void Update(Employee item)
        {
            //throw new NotImplementedException();
            
             var obj = _Db.Employee.FirstOrDefault(c=>c.EmployeeId==item.EmployeeId);

            if(obj==null)

                return;

            obj.EmployeeId = item.EmployeeId;

            obj.EmployeeName = item.EmployeeName;

            obj.EmployeeJoinDate = item.EmployeeJoinDate;

            obj.EmployeeBirthDate = item.EmployeeBirthDate;

            obj.EmployeeDepartmentId = item.EmployeeDepartmentId;

            obj.EmployeeProjectId = item.EmployeeProjectId;

            obj.ManagerId = item.ManagerId;

            _Db.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            _Db.SaveChanges();
        }
        
    }
}