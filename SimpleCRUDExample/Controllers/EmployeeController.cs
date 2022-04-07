using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Business.Service.Interfaces;
using Core.Common.DTOs.Employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleCRUDExample.Helper;
using SimpleCRUDExample.Models.Employee;

namespace SimpleCRUDExample.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IMapper _mapper;

        private readonly IEmployeeService _employeeservice;

        public EmployeeController(IEmployeeService EmployeeEngine, IMapper mapper)
        {
            _employeeservice = EmployeeEngine ?? throw new ArgumentNullException(nameof(EmployeeEngine));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IActionResult Index()
        {


            return View(_mapper.Map<List<EmployeeDto>, List<EmployeeViewModel>>(_employeeservice.GetAllEmployee()));
        }
        public IActionResult Showlist()
        {


            return View("_ViewAll", _mapper.Map<List<EmployeeDto>, List<EmployeeViewModel>>(_employeeservice.GetAllEmployee()));
        }
        [HttpGet]
       
        public IActionResult Create()
        {
            return View(new EmployeeViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> Create(EmployeeViewModel model)
        {
            
            if (!ModelState.IsValid)
            {
                Response.StatusCode = 400;
                var modelErrors = ModelState.AllErrors(); 
                return Json(modelErrors);
            }

            if (await _employeeservice.EmployeeIsExists(model.EmployeeFirstName, model.EmployeeLastName))
            {
                ModelState.AddModelError("EmployeeFirstName", "The Employee name must be unique!");
                ModelState.AddModelError("EmployeeLastName", "The Employee name must be unique!");
                Response.StatusCode = 400;
                var modelErrors = ModelState.AllErrors();
                return Json(modelErrors);
            }

            if (await _employeeservice.AddEmployee(_mapper.Map<EmployeeViewModel, EmployeeDto>(model)))
            {
                return Json(new { Ok=1});
            }
            else
            {
              
                ModelState.AddModelError("general", "Failed to save team! Please try again!");
                Response.StatusCode = 400;
                var modelErrors = ModelState.AllErrors();
                return Json(modelErrors);
            }



        }

        [HttpGet]
       
        public IActionResult Edit(int id)
        {
            return View(_mapper.Map<EmployeeDto, EmployeeViewModel>(_employeeservice.GetEmployeeById(id)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> Edit(EmployeeViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            if (await _employeeservice.EmployeeIsExists(model.EmployeeID, model.EmployeeFirstName, model.EmployeeLastName))
            {
                ModelState.AddModelError("Name", "A Employee with that name already exists!");

                return View(model);
            }

            if (await _employeeservice.UpdateEmployee(_mapper.Map<EmployeeViewModel, EmployeeDto>(model)))
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.Message = "Failed to save Employee! Please try again!";

                return View(model);
            }

        }

        public IActionResult Delete(int id)
        {
            if (_employeeservice.DeleteEmployeeById(id))
            {
                ViewBag.Message = "Failed to delete Employee! Please try again!";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
