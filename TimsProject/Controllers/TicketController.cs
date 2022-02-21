using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimsProject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using TimsProject.Infrastructure;
namespace team2.Controllers
{
    
    public class TicketController : Controller
    {
        // int sentId;
        string UserName;
        int Id;
        ICRUDRepository<Tickets, int> _repository;
        public SendServiceBusMessage _SendServiceBusMessage;
        public TicketController(ICRUDRepository<Tickets, int> repository, SendServiceBusMessage sendServiceBusMessage)
        {
            _repository = repository;
            _SendServiceBusMessage = sendServiceBusMessage;
        }
        public ActionResult<IEnumerable<Tickets>> GetAll()
        {
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
        
          [HttpGet]
        public ActionResult<Tickets> GetDetails(int id)
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
        public IActionResult Create()
        {
            return View();
        }
       [Microsoft.AspNetCore.Authorization.Authorize()]
        [HttpPost]
        public ActionResult<Tickets> Create(Tickets ts)
        {
            if (!ModelState.IsValid)
              UserName = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
              Id = Convert.ToInt32("0" + HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
              var role = Convert.ToString(HttpContext.User.FindFirst(ClaimTypes.Role).Value);
            if (role != "Administrator" && role != "Employee")
            {
                return Unauthorized();
            }
            try
            {
                if (ts==null)
                    return BadRequest();
                _repository.Create(ts);
                ViewBag.Message = string.Format("Ticket Created Succesfully");
                return View();
            }
            catch(Exception e)
                    {
                        throw;
                    }
        }
        public IActionResult update(int id)
        {
            Tickets ts = _repository.GetDetails(id);
            if (ts == null)
            {
                return BadRequest();
            }
            else
            {
                return View(ts);
            }
        }
        [Microsoft.AspNetCore.Authorization.Authorize()]
        [HttpPost]
        public async Task<ActionResult<Employee>> update(Tickets ts)
        {
            UserName = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            Id = Convert.ToInt32("0" + HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var role = Convert.ToString(HttpContext.User.FindFirst(ClaimTypes.Role).Value);
            if (role != "Operator") return Unauthorized();

            try
            {
                if (ts == null)
                    return BadRequest();
                //if(id==0)
                //return BadRequest();
                _repository.Update(ts);
                await _SendServiceBusMessage.sendServiceBusMessage(new ServiceBusMessageData
                {
                    TicketId = ts.TicketId,
                    TicketDate = ts.TicketDate,
                    ApprovalStatus=ts.ApprovalStatus,
                    action = "Update",
                    actionMessage = "Ticket Updated Sucessfully"
                });
                ViewBag.Message = string.Format("Ticket details Updated");
                return View();
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public IActionResult delete(int id)
        {
            Tickets ts = _repository.GetDetails(id);
            if (ts == null)
            {
                return BadRequest();
            }
            else
            {
                return View(ts);
            }
        }
        [Microsoft.AspNetCore.Authorization.Authorize()]
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            UserName = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            Id = Convert.ToInt32("0" + HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var role = Convert.ToString(HttpContext.User.FindFirst(ClaimTypes.Role).Value);
            if (role != "Administrator" && role != "Operator") return Unauthorized();
            try
            {
                _repository.Delete(id);
                await _SendServiceBusMessage.sendServiceBusMessage(new ServiceBusMessageData
                {

                    action = "Delete",
                    actionMessage = "Ticket Deleted Sucessfully"
                });
                ViewBag.Message = string.Format("Ticket details Removed");
                return View();
            }
            catch (Exception e)
            {
                throw;
            }
        }
        
    }
}
