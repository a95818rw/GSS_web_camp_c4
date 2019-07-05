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
        /// 書籍資料查詢
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.JobTitleCodeData = this.codeService.GetCodeTable("TITLE");
            return View();
        }

        /// <summary>
        /// 書籍資料查詢(查詢)
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
        public ActionResult Index(Models.BookSearchArg arg)
        {
            Models.BookService bookService = new Models.BookService();
            if (arg.BoughtDateEnd == null)
                arg.BoughtDateEnd = DateTime.Now.ToShortDateString();
            ViewBag.SearchResult = bookService.GetBookByCondition(arg);
            ViewBag.JobTitleCodeData = this.codeService.GetCodeTable("TITLE");
            return View("Index");
        }

        /// <summary>
        /// 新增書籍畫面
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public ActionResult InsertBook()
        {
            ViewBag.JobTitleCodeData = this.codeService.GetCodeTable("TITLE");
            ViewBag.CountryCodeData = this.codeService.GetCodeTable("COUNTRY");
            ViewBag.CityCodeData = this.codeService.GetCodeTable("CITY");
            ViewBag.GenderCodeData = this.codeService.GetCodeTable("GENDER");
            ViewBag.EmpCodeData = this.codeService.GetBook("0");
            return View(new Models.Books());
        }

        /// <summary>
        /// 新增書籍??????????
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost()]
        public ActionResult InsertEmployee(Models.Books book)
        {
            ViewBag.JobTitleCodeData = this.codeService.GetCodeTable("TITLE");
            ViewBag.CountryCodeData = this.codeService.GetCodeTable("COUNTRY");
            ViewBag.CityCodeData = this.codeService.GetCodeTable("CITY");
            ViewBag.GenderCodeData = this.codeService.GetCodeTable("GENDER");
            ViewBag.EmpCodeData = this.codeService.GetBook("0");
            if (ModelState.IsValid)
            {
                Models.BookService bookService = new Models.BookService();
                bookService.InsertBook(book);
                TempData["message"] = "存檔成功";
            }
            return View(book);
        }

        /// <summary>
        /// 刪除員工
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        [HttpPost()]
        public JsonResult DeleteBook(string bookId)
        {
            try
            {
                Models.BookService BookService = new Models.BookService();
                BookService.DeleteBookById(bookId);
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
        /// <param name="bookId"></param>
        /// <returns></returns>
        public ActionResult UpdateBook(string bookId)
        {
            ViewBag.aaa = bookId;
            return View();
        }

    }
}