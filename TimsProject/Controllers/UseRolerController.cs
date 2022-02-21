using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimsProject.Models;
using TimsProject.Infrastructure;
namespace team2.Controllers
{
    
    public class UseRolerController : Controller
    {
         ICRUDRepository<UserRoles, int> _repository; 
        public UseRolerController( ICRUDRepository<UserRoles, int> repository )  => _repository = repository;
        public ActionResult<IEnumerable<UserRoles>> Get()
        {
            try
            {
                var items = _repository.GetAll(); 
                return items.ToList();
            }
            catch(Exception e)
                    {
                        throw;
                    }
        }
        
          [HttpGet("{id}")]
        public ActionResult<UserRoles> GetDetails(int id)
        {
            try
            {
                var item = _repository.GetDetails(id);
                if(item==null)
                    return NotFound();
                return item;
            }
            catch(Exception e)
                    {
                        throw;
                    }
        }
       
        [HttpPost("rsaddnew")]
        public ActionResult<UserRoles> Create(UserRoles rs)
        {
            if(rs==null)
                return BadRequest();
            try
            {
                _repository.Create(rs);
                return rs;
            }
            catch(Exception e)
                    {
                        throw;
                    }
        }
        [HttpPut("rsupdate/{id}")]
        public ActionResult<UserRoles> update(int id, UserRoles rs)
        {
            Console.WriteLine(id);
            if(rs==null)
                return BadRequest();
                if(id==0)
                return BadRequest();
            try
            {
                _repository.Update(rs);
                return rs;
            }
            catch(Exception e)
                    {
                        throw;
                    }
        }
        [HttpDelete("rsremove/{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _repository.Delete(id);
                return Ok();
            }
            catch(Exception e)
                    {
                        throw;
                    }
        } 
    }
}