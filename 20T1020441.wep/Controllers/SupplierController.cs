using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _20T1020441.DataLayers;
using _20T1020441.DomainModels;
using _20T1020441.BusinessLayers;
using _20T1020441.Web.Models;
using _20T1020411.Web;

namespace _20T1020411.Web.Controllers
{
    [Authorize]
    public class SupplierController : Controller
    {
        private const int PAGE_SIZE = 5;
        private const string SUPPLIER_SEARCH = "SearchSupplierCondition";
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //public ActionResult Index(int page =1, int pageSize = 5, string searchValue ="")
        //{
        //int rowCount = 0;
        //var data = CommonDataService.ListOfSuppliers(page, pageSize, searchValue, out rowCount);


        //int pageCount = rowCount / pageSize;
        //if (rowCount % pageSize > 0)
        //    pageCount += 1;
        //ViewBag.Page = page;
        //ViewBag.PageCount = pageCount;
        //ViewBag.PageSize = pageSize;
        //ViewBag.SearchValue = searchValue;
        //ViewBag.RowCount = rowCount;

        //return View(data);

        [HttpGet]
        public ActionResult login()
        {
            return View();                                                          
        }
        //}
        /// <summary>
        /// Giao diện để bổ sung nhà cung cấp mới 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {

            PaginationSearchInput condition = Session[SUPPLIER_SEARCH] as PaginationSearchInput;

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

            //return Content(condition.PageSize.ToString());
            int rowCount = 0;
            var data = CommonDataService.ListOfSuppliers(condition.Page, condition.PageSize, condition.SearchValue, out rowCount);
            var result = new SupplierSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                Rowcount = rowCount,
                Data = data
            };
            Session[SUPPLIER_SEARCH] = condition;
            return View(result);
        }

        public ActionResult Create()
        {
            var data = new Supplier()
            {
                SupplierID = 0
            };

            ViewBag.Title = "Bổ sung nhà cung cấp";
            return View("Edit", data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id = 0)
        {

            try
            {
                var data = CommonDataService.GetSupplier(id);
                if (data == null)
                    return RedirectToAction("Index");

                ViewBag.Title = "Cập nhật nhà cung cấp";
                return View(data);
            }
            catch
            {
                //    ghi lại log lỗi
                return Content("Có lỗi xảy ra.");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Supplier data)
        {
            try
            {
                // Kiểm soát đàu vào 
                if (string.IsNullOrWhiteSpace(data.SupplierName))
                    ModelState.AddModelError("SupplierName", "Tên không được để trống");
                if (string.IsNullOrWhiteSpace(data.ContactName))
                    ModelState.AddModelError("ContactName", "Tên giao dịch không được để trống");
                if (string.IsNullOrWhiteSpace(data.Country))
                    ModelState.AddModelError("Country", "Vui lòng chọn quốc gia");
                if (string.IsNullOrWhiteSpace(data.Address))
                    ModelState.AddModelError("Address", "Vui lòng nhập địa chỉ");
                if (string.IsNullOrWhiteSpace(data.City))
                    ModelState.AddModelError("City", "Vui lòng nhập thành phố");
                if (string.IsNullOrWhiteSpace(data.Phone))
                    ModelState.AddModelError("Phone", "Vui lòng nhập số điện thoại");
                if (string.IsNullOrWhiteSpace(data.PostalCode))
                    ModelState.AddModelError("PostalCode", "Mã bưu chính không được để trống");
                if (!ModelState.IsValid)
                {
                    ViewBag.Title = data.SupplierID == 0 ? "Bổ sung nhà cung cấp" : "Cập nhật nhà cung cấp";
                    return View("Edit", data);
                }
                if (data.SupplierID == 0)
                {
                    CommonDataService.AddSupplier(data);
                }
                else
                {
                    CommonDataService.UpdateSupplier(data);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //    ghi lại log lỗi
                return Content("Có lỗi xảy ra.");
            }
            //if (data.SupplierID == 0)
            //{
            //    CommonDataService.AddSupplier(data);
            //}
            //else
            //{
            //    CommonDataService.UpdateSupplier(data);
            //}
            //return RedirectToAction("Index");
        }
        public ActionResult Delete(int id = 0)
        {
            //try
            //{
            //    int supplierId = Convert.ToInt32(id);
            //    if (Request.HttpMethod == "GET")
            //    {
            //        var data = CommonDataService.GetSupplier(supplierId);
            //        return View(data);
            //    }
            //    else
            //    {
            //        CommonDataService.DeleteSupplier(supplierId);
            //        return RedirectToAction("Index");
            //    }
            //}
            //catch
            //{
            //    return RedirectToAction("Index");
            //}
            if (id == 0)
                return RedirectToAction("Index");

            if(Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteSupplier(id);
                return RedirectToAction("Index");

            }
            var data = CommonDataService.GetSupplier(id);
            if (data == null) return RedirectToAction("index");

            return View(data);
        }
    }
}