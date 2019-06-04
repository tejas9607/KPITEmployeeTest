using KPITEmployeeTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
namespace KPITEmployeeTest.Controllers
{

    public class EmployeeController : Controller
    {
        public ActionResult List()
        {
            //IEnumerable<EmployeeViewModel> empViewModels = new BusinessLayer().GetEmployeeList();
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        private List<SelectListItem> GetMaritalStatus()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{Text="Single",Value="Single"},
                new SelectListItem{Text="Married",Value="Married"},
                new SelectListItem{Text="Divorced",Value="Divorced"},
                new SelectListItem{Text="Widowed",Value="Widowed"},
            };
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.MaritalStatusList = GetMaritalStatus();
            return View();
        }

        [HttpPost]
        public ActionResult Create(EmployeeViewModel employeeViewModel)
        {
            ViewBag.MaritalStatusList = GetMaritalStatus();
            if (!ModelState.IsValid)
                return View(employeeViewModel);

            if(new BusinessLayer().IsEmployeeExist(employeeViewModel.Name))
            {
                ModelState.AddModelError("Name", "Employee name already exists");
                return View(employeeViewModel);
            }

            new BusinessLayer().AddEmployee(employeeViewModel);
            return RedirectToAction("List");
        }

        public ActionResult SearchEmployee(string keyword)
        {
            try
            {
                var data = new BusinessLayer().Search(keyword);
                return PartialView(data);
            }
            catch
            {
                 return PartialView("Error");
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            new BusinessLayer().Delete(id);
            return RedirectToAction("List");
        }

    }
}