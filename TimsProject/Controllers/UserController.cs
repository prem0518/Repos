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
    
    public class UserController : Controller
    {
         ICRUDRepository<Users, int> _repository; 
        public UserController( ICRUDRepository<Users, int> repository )  => _repository = repository;
        public ActionResult<IEnumerable<Users>> Get()
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
        public ActionResult<Users> GetDetails(int id)
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
       
        [HttpPost]
        public ActionResult<Users> Create(Users us)
        {
            if(us==null)
                return BadRequest();
            try
            {
            _repository.Create(us);
            return us;
            }
            catch(Exception e)
                    {
                        throw;
                    }
        }
        [HttpPost]
        public ActionResult<Users> update(int id, Users us)
        {
            Console.WriteLine(id);
            if(us==null)
                return BadRequest();
                if(id==0)
                return BadRequest();
            try
            {
                _repository.Update(us);
                return us;
            }
            catch(Exception e)
                    {
                        throw;
                    }
        }
        [HttpPost]
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