using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimsProject.Models;
using TimsProject.Infrastructure;
using System.Security.Claims;
namespace team2.Controllers
{
    
    
    public class EmployeeController : Controller
    {
         string UserName;
        int Id;
        ICRUDRepository<Employee, int> _repository;
        public SendServiceBusMessage _SendServiceBusMessage;
        public EmployeeController(ICRUDRepository<Employee, int> repository, SendServiceBusMessage sendServiceBusMessage)
        {
            _repository = repository;
            _SendServiceBusMessage = sendServiceBusMessage;
        }

       
        public ActionResult<IEnumerable<Employee>> GetAll()
        {

            //UserName = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            //Id = Convert.ToInt32("0" + HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            //var role = Convert.ToString(HttpContext.User.FindFirst(ClaimTypes.Role).Value);
            //if (role != "admin") return Unauthorized();
            try
            {
            var items = _repository.GetAll(); 
            return View(items);
            } 
            catch(Exception e)
            {
                throw;
            }
        }
        
          [HttpGet("{id}")]
        public ActionResult<Employee> GetDetails(int id)
        {
            // UserName = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            // Id = Convert.ToInt32("0" + HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            // var role = Convert.ToString(HttpContext.User.FindFirst(ClaimTypes.Role).Value);
            // if(role!="admin") return Unauthorized();
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
        public IActionResult Create()
        {
            return View();
        }
        [Microsoft.AspNetCore.Authorization.Authorize()]
        [HttpPost]
        public async  Task<ActionResult<Employee>> Create(Employee emp)
        {
            if(!ModelState.IsValid)
            UserName = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            Id = Convert.ToInt32("0" + HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var role = Convert.ToString(HttpContext.User.FindFirst(ClaimTypes.Role).Value);
            if (role!= "Administrator") return Unauthorized();
            try
            {
                    if(emp==null)
                        return BadRequest();
                    _repository.Create(emp);
                await _SendServiceBusMessage.sendServiceBusMessage(new ServiceBusMessageData
                {
                    EmployeeName = emp.EmployeeName,
                    EmployeeDepartmentId= emp.EmployeeDepartmentId,
                    action="Add",
                    actionMessage = "Employee Added Sucessfully"
                }) ;
                    ViewBag.Message = string.Format("Employee Updates Succesfully");
                    return View();
            }
               catch(Exception e)
                    {
                        throw;
                    } 
        }
        public IActionResult update(int id)
        {
            Employee emp = _repository.GetDetails(id);
            if (emp == null)
            {
                return BadRequest();
            }
            else {
                return View(emp);
            }
        }
        [Microsoft.AspNetCore.Authorization.Authorize()]
        [HttpPost]
        public async Task<ActionResult<Employee>> update( Employee emp)
        {
            UserName = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            Id = Convert.ToInt32("0" + HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var role = Convert.ToString(HttpContext.User.FindFirst(ClaimTypes.Role).Value);
            if (role!= "Administrator") return Unauthorized();

            try
            {
                if(emp==null)
                    return BadRequest();
                   //if(id==0)
                    //return BadRequest();
                _repository.Update(emp);
                await _SendServiceBusMessage.sendServiceBusMessage(new ServiceBusMessageData
                {
                    EmployeeName = emp.EmployeeName,
                    EmployeeDepartmentId = emp.EmployeeDepartmentId,
                    action = "Update",
                    actionMessage = "Employee Updated Sucessfully"
                });
                ViewBag.Message = string.Format("Employee details Updated");
                return View();
            }
            catch(Exception e)
                    {
                        throw;
                    }
        }
        public IActionResult delete(int id)
        {
            Employee emp = _repository.GetDetails(id);
            if (emp == null)
            {
                return BadRequest();
            }
            else
            {
                return View(emp);
            }
        }
        [Microsoft.AspNetCore.Authorization.Authorize()]
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            UserName = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            Id = Convert.ToInt32("0" + HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var role = Convert.ToString(HttpContext.User.FindFirst(ClaimTypes.Role).Value);
            if (role!= "Administrator") return Unauthorized();
            try
            {
                _repository.Delete(id);
                await _SendServiceBusMessage.sendServiceBusMessage(new ServiceBusMessageData
                {
                    
                    action = "Delete",
                    actionMessage = "Employee Deleted Sucessfully"
                });
                ViewBag.Message = string.Format("Employee details Deleted");
                return View();
            }
            catch(Exception e)
                    {
                        throw;
                    }
        } 
    }
}