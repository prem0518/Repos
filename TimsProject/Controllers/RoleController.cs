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
 
    public class RoleController : Controller
    {
         ICRUDRepository<Roles, int> _repository; 
        public RoleController( ICRUDRepository<Roles, int> repository )  => _repository = repository;
        public ActionResult<IEnumerable<Roles>> Get()
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
        public ActionResult<Roles> GetDetails(int id)
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
        public ActionResult<Roles> Create(Roles rs)
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
        public ActionResult<Roles> update(int id, Roles rs)
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