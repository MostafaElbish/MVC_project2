using Microsoft.AspNetCore.Mvc;
using Demo.BLL.Repostry;
using Demo.BLL.Interfaces;
using Demo.DLL.models;
using Demo.BLL;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Demo.PL.Controllers
   
{
    [Authorize]
    public class DEpartmentController : Controller
    {
      

        public IUnitOfWork _UnitOfWork { get; }

        public  DEpartmentController (IUnitOfWork unitOfWork)
        {
           
            _UnitOfWork = unitOfWork;
        }
   
        public async Task<IActionResult> Index()
        {
         var department=  await _UnitOfWork.DepartmentReposrty.GetAll();
            return View(department);
        }
        [HttpGet]
        public IActionResult Create()
        {
        return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Create(Department moddel )
        {
            if (ModelState.IsValid)
            {


               await _UnitOfWork.DepartmentReposrty.Add(moddel);
                var count = await _UnitOfWork.Complete();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(moddel);
        }
        public async Task< IActionResult> Details(int?id,string viwename="Details")
        {
            if(id is null)
            {
                return BadRequest();
            }
           
            var depart = await _UnitOfWork.DepartmentReposrty.Get(id.Value);
            if (depart is null)
            {
                return NotFound();
            }
            return View(viwename,depart);
        }
        [HttpGet]
        public async Task< IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int?id,Department model) {
        
      if(id!=model.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                 _UnitOfWork.DepartmentReposrty.Update(model);
                var count = await _UnitOfWork.Complete();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));

                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task< IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");


        }
        [HttpPost]  
        public async Task< IActionResult> Delete(int?id,Department model)
        {

            if (id != model.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                 _UnitOfWork.DepartmentReposrty.Delete(model);
                var count =await _UnitOfWork.Complete();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));

                }
            }
            return View(model);
        }
        
    }

}
