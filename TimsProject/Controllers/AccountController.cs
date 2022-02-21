using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimsProject.Infrastructure;
using TimsProject.Models;
namespace team2.Controllers
{
   
    public class AccountController : Controller
    {
 
        IUserService _userService;
        
        public AccountController(IUserService service) => _userService = service;
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(Users model)
        {
            
            if(!ModelState.IsValid)
                return BadRequest();
            
            var signInStatus = _userService.Authenticate(model); 
            if(signInStatus==false) 
            {
                return NotFound();
            }

            var role = _userService.GetUserRole(model.Id);
           
             var claims  = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.UserName), 
                new Claim(ClaimTypes.NameIdentifier, model.Id.ToString()),
                new Claim(ClaimTypes.Role, role.RoleName)
            };

            var claimsIdentity = new ClaimsIdentity(
                claims:  claims,
                authenticationType: CookieAuthenticationDefaults.AuthenticationScheme );
            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                IsPersistent = true
            };
            await HttpContext.SignInAsync(
                scheme: CookieAuthenticationDefaults.AuthenticationScheme,
                principal: new ClaimsPrincipal(claimsIdentity), 
                properties: authProperties

            );

           
            return Redirect("/Ticket/GetAll");
        }
        
            [HttpGet]
            new public async Task<IActionResult> SignOut(){
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();            
                 }
            
       
    }
}