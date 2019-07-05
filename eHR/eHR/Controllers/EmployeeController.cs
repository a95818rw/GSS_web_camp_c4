using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eHR.Controllers
{
    public class EmployeeController : Controller
    {

        Models.CodeService codeService = new Models.CodeService();
        /// <summary>
        /// 員工資料查詢
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.JobTitleCodeData = this.codeService.GetCodeTable("TITLE");
            return View();
        }

        /// <summary>
        /// 員工資料查詢(查詢)
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
        public ActionResult Index(Models.EmployeeSearchArg arg)
        {
            Models.EmployeeService employeeService = new Models.EmployeeService();
            if (arg.HireDateEnd == null)
                arg.HireDateEnd = DateTime.Now.ToShortDateString();
            ViewBag.SearchResult = employeeService.GetEmployeeByCondtioin(arg);
            ViewBag.JobTitleCodeData = this.codeService.GetCodeTable("TITLE");
            return View("Index");
        }

        /// <summary>
        /// 新增員工畫面
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public ActionResult InsertEmployee()
        {
            ViewBag.JobTitleCodeData = this.codeService.GetCodeTable("TITLE");
            ViewBag.CountryCodeData = this.codeService.GetCodeTable("COUNTRY");
            ViewBag.CityCodeData = this.codeService.GetCodeTable("CITY");
            ViewBag.GenderCodeData = this.codeService.GetCodeTable("GENDER");
            ViewBag.EmpCodeData = this.codeService.GetEmployee("0");
            return View(new Models.Employees());
        }

        /// <summary>
        /// 新增員工
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost()]
        public ActionResult InsertEmployee(Models.Employees employee)
        {
            ViewBag.JobTitleCodeData = this.codeService.GetCodeTable("TITLE");
            ViewBag.CountryCodeData = this.codeService.GetCodeTable("COUNTRY");
            ViewBag.CityCodeData = this.codeService.GetCodeTable("CITY");
            ViewBag.GenderCodeData = this.codeService.GetCodeTable("GENDER");
            ViewBag.EmpCodeData = this.codeService.GetEmployee("0");
            if (ModelState.IsValid)
            {
                Models.EmployeeService employeeService = new Models.EmployeeService();
                if (employee.MonthlyPayment != null)
                    employee.MonthlyPayment = employee.MonthlyPayment.Replace(",", "");
                if (employee.YearlyPayment != null)
                    employee.YearlyPayment = employee.YearlyPayment.Replace(",", "");
                employeeService.InsertEmployee(employee);
                TempData["message"] = "存檔成功";
            }
            return View(employee);
        }

        /// <summary>
        /// 刪除員工
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpPost()]
        public JsonResult DeleteEmployee(string employeeId)
        {
            try
            {
                Models.EmployeeService EmployeeService = new Models.EmployeeService();
                EmployeeService.DeleteEmployeeById(employeeId);
                return this.Json(true);
            }

            catch (Exception ex)
            {
                return this.Json(false);
            }
        }

        /// <summary>
        /// 修改員工畫面
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public ActionResult UpdateEmployee(string employeeId)
        {
            ViewBag.aaa = employeeId;
            return View();
        }

    }
}