using Demo.DAL.Models;
using Demo.PL.View_Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManger;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManger = roleManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(string serchinput)
        {
            var Users = Enumerable.Empty<RoleViewModel>();
            if (string.IsNullOrEmpty(serchinput))
            {
                Users = await _roleManger.Roles.Select(u => new RoleViewModel()
                {
                    Id = u.Id,
                    RoleName = u.Name

                }).ToListAsync();
            }
            else
            {
                Users = await _roleManger.Roles.Where(u => u.Name.ToLower()
                  .Contains(serchinput.ToLower())).Select(u => new RoleViewModel()
                  {
                      Id = u.Id,
                      RoleName = u.Name
                  }).ToListAsync();

            }
            // ViewData["Message"] = "Hello World";

            return View(Users);

        }
        [HttpGet]
        public IActionResult Creat()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Creat(RoleViewModel model1)
        {

            if (ModelState.IsValid)
            {
                var role = new IdentityRole()
                {
                    Name = model1.RoleName,

                };

                await _roleManger.CreateAsync(role);
                return RedirectToAction(nameof(Index));
            }
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Details(string? id, string viwename = "Details")
        {
            if (id is null)
            {
                return BadRequest();
            }

            var user = await _roleManger.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var usermodel = new RoleViewModel()
            {
                Id = user.Id,
                RoleName = user.Name

            };

            return View(viwename, usermodel);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            return await Details(id, "Edit");


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string? id, RoleViewModel model)
        {

            if (id != model.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {

                var user = await _roleManger.FindByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                user.Name = model.RoleName;


                _roleManger.UpdateAsync(user);
                return RedirectToAction("Index");
            }

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            return await Details(id, viwename: "Delete");


        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] string? id, RoleViewModel model)
        {

            if (id != model.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {

                var user = await _roleManger.FindByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }


                user.Name = model.RoleName;


                _roleManger.DeleteAsync(user);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> AddOrRemove(string roleId)
        {
            var role = await _roleManger.FindByIdAsync(roleId);
            if (role == null)
                return NotFound();
            ViewData[index: "RoleId" ]= roleId;

            var userInRole = new List<UserInRoleViewModel>();
            var users = await _userManager.Users.ToArrayAsync();
            foreach (var user in users)
            {
                var userinRole = new UserInRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userinRole.Isselected = true;
                }
                else
                {
                    userinRole.Isselected = false;
                }
                userInRole.Add(userinRole);
            }
            return View(userInRole);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrRemove(string roleId,List<UserInRoleViewModel>users)
        {

            var role = await _roleManger.FindByIdAsync(roleId);
            if (role == null)
                return NotFound();
            if(ModelState.IsValid)
            {
                foreach (var user in users)
                {
                    var adduser =await _userManager.FindByIdAsync(user.UserId);
                    if (adduser is not null)
                    {
                       
                        if (user.Isselected && !await _userManager.IsInRoleAsync(adduser, role.Name))
                        {
                           await _userManager.AddToRoleAsync(adduser, role.Name);

                        }
                        else if (!user.Isselected && await _userManager.IsInRoleAsync(adduser, role.Name))
                        {
                          await  _userManager.RemoveFromRoleAsync(adduser, role.Name);
                        }
                    }
                }
                return RedirectToAction(nameof(Edit), new {id=roleId });

            }
            return View(users);


        }
    }
}
