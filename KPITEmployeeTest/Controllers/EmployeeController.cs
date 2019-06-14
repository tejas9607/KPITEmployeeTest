using KPITEmployeeTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ViewModels;

namespace KPITEmployeeTest.Controllers
{

    public class EmployeeController : Controller
    {
        public ActionResult List()
        {
            return View();
        }

        public ActionResult GetEmployeeList()
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();

            CustomFilter customFilter = new CustomFilter();
            customFilter.streetSearchValue = Request.Form.GetValues("columns[5][search][value]").FirstOrDefault();
            customFilter.ageSearchValue = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault();
            customFilter.salarySearchValue = Request.Form.GetValues("columns[2][search][value]").FirstOrDefault();


            customFilter.pageSize = length != null ? Convert.ToInt32(length) : 0;
            customFilter.skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            var data = new BusinessLayer().GetEmployeeList(customFilter, out recordsTotal);
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
        }

        public ActionResult Index()
        {
            return View();
        }

        private List<SelectListItem> GetMaritalStatus()
        {
            var maritalStatusList = new BusinessLayer().GetMaritalStatusList();


            List<SelectListItem> maritalStatusSelectList = (from item in maritalStatusList
                                                            select new SelectListItem
                                                            {
                                                                Text = item.Status,
                                                                Value = item.MaritalStatusId.ToString()
                                                            }).ToList();
            return maritalStatusSelectList;
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

            if (new BusinessLayer().IsEmployeeExist(employeeViewModel.Name))
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
                var jsonFormat = Json(new { data = data }, JsonRequestBehavior.AllowGet);
                return jsonFormat;
                // return PartialView(data);
            }
            catch (Exception ex)
            {
                return PartialView("Error");
            }
        }

        [HttpGet]
        public bool Delete(int id)
        {
            return new BusinessLayer().Delete(id);
        }

    }
}