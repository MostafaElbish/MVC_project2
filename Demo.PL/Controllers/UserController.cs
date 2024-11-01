using Demo.DAL.Models;
using Demo.PL.View_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    //[Authorize (Roles = "Admin")]
    public class UserController : Controller
    {
       
        private readonly UserManager<ApplicationUser> _userManager;
        public UserController(UserManager<ApplicationUser>userManager) 
        { 
            _userManager = userManager;
        
        }
        public async Task<IActionResult> Index(string serchinput)
        {
            var Users = Enumerable.Empty<UserViewModel>();
            if (string.IsNullOrEmpty(serchinput))
            {
             Users = await _userManager.Users.Select(u=>new UserViewModel()
                {
                    Id = u.Id,
                    FirstName=u.firstname,
                    LastName= u.Lastname,
                    Email=u.Email,
                    Roles=_userManager.GetRolesAsync(u).Result

                }).ToListAsync();
            }
            else
            {
              Users=await  _userManager.Users.Where(u=>u.Email.ToLower()
                .Contains(serchinput.ToLower())).Select(u => new UserViewModel()
                {
                    Id = u.Id,
                    FirstName = u.firstname,
                    LastName = u.Lastname,
                    Email = u.Email,
                    Roles = _userManager.GetRolesAsync(u).Result

                }).ToListAsync();

            }
            // ViewData["Message"] = "Hello World";
           
            return View(Users);

        }
        public async Task<IActionResult> Details(string id, string viwename = "Details")
        {
            if (id is null)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByIdAsync(id);
        if(user == null)
            {
                return NotFound();
            }
        var usermodel= new UserViewModel()
        {
            Id=user.Id,
            FirstName=user.firstname,
            LastName= user.Lastname,
            Email=user.Email,
            Roles= _userManager.GetRolesAsync(user).Result
        };

            return View(viwename, usermodel);
        }
        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, UserViewModel model)
        {

            if (id != model.Id)
            {
                return BadRequest();
            }
        
            if (ModelState.IsValid)
                {
                 
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

                user.firstname = model.FirstName;
                user.Lastname=model.LastName;
                user.Email = model.Email;

               await _userManager.UpdateAsync(user);
                 return RedirectToAction("Index");
                }
            
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");


        }
        [HttpPost, ActionName("Delete")]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id,UserViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {

                var user = await _userManager.FindByIdAsync(id);
                if (user is null)

                    return NotFound();



                await _userManager.DeleteAsync(user);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

    }
       
    }
