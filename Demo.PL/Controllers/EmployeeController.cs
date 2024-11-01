using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using Demo.DLL.models;
using Demo.PL.View_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PL.Controllers

{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        // private IemployeeRepostry _employeerspostres;
        private readonly IMapper _mapper;
        private readonly IdepartmentResposty _idepartmentResposty;

        public EmployeeController(/*IemployeeRepostry employeerespostres,*/IUnitOfWork unitOfWork,IMapper mapper,IdepartmentResposty idepartmentResposty)
        {
            //_employeerspostres = employeerespostres;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        
            this._idepartmentResposty = idepartmentResposty;
        }

        public async Task< IActionResult> Index(string serchinput)
        {
            var employees = Enumerable.Empty<Employees>();
            if (string.IsNullOrEmpty(serchinput))
            {
                employees = await _unitOfWork.EmoloyeeRepostry.GetAll();
            }
            else
            {
                employees =await _unitOfWork.EmoloyeeRepostry.Getbyname(serchinput.ToLower());
            }
            // ViewData["Message"] = "Hello World";
            var result = _mapper.Map<IEnumerable<EployeeViewModel>>(employees);
            return View(result);

        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Department"] =  _idepartmentResposty.GetAll();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult>  Create(EployeeViewModel moddel)
        {
            if (ModelState.IsValid)
            {
                //    Employees employees = new Employees()
                //    {
                //        Id = moddel.Id,
                //        Name = moddel.Name,

                //        HirangDate = moddel.HirangDate,
                //        Salary = moddel.Salary,
                //        Address = moddel.Address,
                //        Phone = moddel.Phone,
                //        DateofCreation = moddel.DateofCreation,
                //        DepartmentId = moddel.DepartmentId,
                //        Department = moddel.Department,
                //        IsDeleted = moddel.IsDeleted
                //    };
                var  employee= _mapper.Map<Employees>(moddel);

                 _unitOfWork.EmoloyeeRepostry.Add(employee);
                var count =await _unitOfWork.Complete();
                if (count > 0)
                {
                    TempData["Message"] = "Employee Added";
                 
                }
                else
                {
                    TempData["Message"] = "Employee Not Added";
                }
                return RedirectToAction(nameof(Index));
            }
            return View(moddel);
        }
      
        public async Task< IActionResult> Details(int? id, string viwename = "Details")
        {
            if (id is null)
            {
                return BadRequest();
            }

            var employee =await _unitOfWork.EmoloyeeRepostry.Get(id.Value);
            var employeeviewmodel =  _mapper.Map<EployeeViewModel>(employee);
           
            if (employee is null)
            {
                return NotFound();
            }
            return View(viwename, employeeviewmodel);
        }
        [HttpGet]
        public async Task< IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Edit([FromRoute] int? id, Employees model)
        {

            if (id != model.Id)
            {
                return BadRequest();
            }
            var employee = _mapper.Map<Employees>(model);
            if (ModelState.IsValid)
            {
                 _unitOfWork.EmoloyeeRepostry.Update(model);
                var count = await _unitOfWork.Complete();
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
        public async Task< IActionResult> Delete(int? id, EployeeViewModel model)
        {

            if (id != model.Id)
            {
                return BadRequest();
            }
            var employee = _mapper.Map<Employees>(model);

            if (ModelState.IsValid)
            {
               _unitOfWork.EmoloyeeRepostry.Delete(employee);
                var count =await _unitOfWork.Complete();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));

                }
            }
            return View(model);
        }
    }
}
