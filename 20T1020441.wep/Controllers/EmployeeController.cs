using _20T1020441.BusinessLayers;
using _20T1020441.DomainModels;
using _20T1020441.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _20T1020441.Web.Controllers
{
    public class EmployeeController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        private const int PAGE_SIZE = 6;
        private const string EMPLOYEE_SEARCH = "SearchEmployeeCondition";


        //   // GET: Employee
        //   public ActionResult Index(int page = 1, int pageSize = 6, string searchValue = "")
        //   {
        //       int rowCount = 0;
        //       var data = CommonDataService.ListOfEmployees(page, pageSize, searchValue, out rowCount);

        //       int pageCount = rowCount / pageSize;
        //       if (rowCount % pageSize > 0)
        //           pageCount += 1;


        //       ViewBag.Page = page;
        //       ViewBag.PageCount = pageCount;
        //       ViewBag.RowCount = rowCount;
        //       ViewBag.PageSize = pageSize;
        //       ViewBag.SearchValue = searchValue;

        //       return View(data);
        //   }
        public ActionResult Index()
        {
            PaginationSearchInput condition = Session[EMPLOYEE_SEARCH] as PaginationSearchInput;
            if (condition == null)
            {
                condition = new PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    SearchValue = ""
                };
            }
            return View(condition);
        }
        public ActionResult Search(PaginationSearchInput condition)
        {
            int rowcount = 0;
            var data = CommonDataService.ListOfEmployees(condition.Page, condition.PageSize, condition.SearchValue,
                out rowcount);
            var result = new EmployeeSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                Rowcount = rowcount,
                Data = data
            };
            Session[EMPLOYEE_SEARCH] = condition;

            return View(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var data = new Employee()
            {
                EmployeeID = 0
            };

            ViewBag.Title = "Bổ sung Nhân Viên";
            return View("Edit", data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Employee data, string birthday, HttpPostedFileBase uploadPhoto)
        {
            //try
            //{
            DateTime? d = Converter.DMYStringToDateTime(birthday);
            if (d == null)
                ModelState.AddModelError("BirthDate", $"Ngày {birthday} không hợp lệ. Vui lòng nhập theo định dạng dd/MM/yyyy");
            else
                data.BirthDate = d.Value;
            // kiểm soát đầu vào 
            if (string.IsNullOrWhiteSpace(data.LastName))
                ModelState.AddModelError("LastName", "Họ đệm không được để trống");
            ///
            if (string.IsNullOrWhiteSpace(data.FirstName))
                ModelState.AddModelError("FirstName", "Tên không được để trống");
            ///
            if (string.IsNullOrWhiteSpace(data.BirthDate.ToLongDateString()))
                ModelState.AddModelError("BirthDate", "Ngày sinh không được để trống");
            ///
            if (string.IsNullOrWhiteSpace(data.Photo))
                data.Photo = "";
            ///
            if (string.IsNullOrWhiteSpace(data.Email))
                ModelState.AddModelError("Email", "Email không được để trống");

            //
            if (!ModelState.IsValid)
            {
                ViewBag.Title = data.EmployeeID == 0 ? "Bổ sung nhân viên" : "Cập nhập nhân viên";
                return View("Edit", data);
            }

            if (uploadPhoto != null)
            {
                string path = Server.MapPath("~/Photo");
                string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                string filePath = System.IO.Path.Combine(path, fileName);
                uploadPhoto.SaveAs(filePath);
                data.Photo = fileName;
            }


            if (data.EmployeeID == 0)
            {
                CommonDataService.AddEmployee(data);
            }
            else
            {
                CommonDataService.UpdateEmployee(data);
            }
            return RedirectToAction("Index");
            //}
            //catch
            //{
            //    // ghi lại log lỗi 
            //    return Content("Có lỗi xảy ra, vui lòng thử lại sau!");
            //}

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
                /// cách cho null 
                ///return Content("id bằng 0");
                return RedirectToAction("index");

            var data = CommonDataService.GetEmployee(id);

            if (data == null)
                return RedirectToAction("index");

            ViewBag.Title = "Cập nhật Nhân Viên";
            return View(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(int id = 0)
        {
            if (id == 0)
                /// cách cho null 
                ///return Content("id bằng 0");
                return RedirectToAction("index");


            if (Request.HttpMethod == "GET")
            {
                var data = CommonDataService.GetEmployee(id);
                if (data == null)
                    return RedirectToAction("index");
                return View(data);
            }
            else
            {
                CommonDataService.DeleteEmployee(id);
                return RedirectToAction("Index");
            }
        }
    }
}
