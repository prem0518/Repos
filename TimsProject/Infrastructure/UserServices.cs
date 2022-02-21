using TimsProject.Models; 
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Linq;
using System.Collections.Generic;
using System;

namespace TimsProject.Infrastructure
{
    public interface IUserService 
    {
        bool Authenticate(Users item); 
    //ADDED: a function declaration to get the user role from the DB. 
        Roles GetUserRole(int id);
        List<Users> GetAll(); 
        Users GetDetails(int id);
    }
    public class UserService : IUserService
    {
        TimsDbContext _context; 
        public UserService(TimsDbContext context) => _context = context;
        public bool Authenticate(Users item)
        {
            var obj = _context.Users.FirstOrDefault(
                c=>c.UserName.Equals(item.UserName) && c.Password.Equals(item.Password) );
            if(obj != null){
                item.Id = obj.Id;
                return true;
            } 
            else 
                return false;
        }
//ADDED: the implementation to get the Role for the currently logged in user. 
        public Roles GetUserRole(int id)
        {
            var roles = _context.Roles.FromSqlRaw(
                $"SELECT RoleId, RoleName FROM Roles WHERE RoleId IN " + 
                $" (SELECT RoleID FROM UserRoles WHERE UserId={id})"
            );
            if(roles.Count()==0)
            return null; 
            else 
            return roles.First();
        }
//END: of editing the function.
        public List<Users> GetAll()
        {
            throw new NotImplementedException();
        }
        public Users GetDetails(int id)
        {
            throw new NotImplementedException();
        }
        
    }
}