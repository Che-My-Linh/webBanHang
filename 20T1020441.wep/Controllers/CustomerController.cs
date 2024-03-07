using _20T1020441.BusinessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _20T1020441.DomainModels;
using _20T1020441.Web.Models;

namespace _20T1020441.Web.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        // GET: Customer
        private const int PAGE_SIZE = 5;
        private const string CUSTOMER_SEARCH = "SearchCustomerCondition";

        public ActionResult Index()
        {

            PaginationSearchInput Condition = Session[CUSTOMER_SEARCH] as PaginationSearchInput;
            if (Condition == null)
            {
                Condition = new PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    SearchValue = ""
                };

            }
            return View(Condition);
        }

        public ActionResult Search(PaginationSearchInput Condition)
        {
            int RowCount = 0;
            var data = CommonDataService.ListOfCustomers(Condition.Page, Condition.PageSize, Condition.SearchValue, out RowCount);
            var result = new CustomerSearchOutput()
            {
                Page = Condition.Page,
                PageSize = Condition.PageSize,
                SearchValue = Condition.SearchValue,
                Rowcount = RowCount,
                Data = data

            }; Session[CUSTOMER_SEARCH] = Condition;

            return View(result);
        }
        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id =0)
        {
            try
            {
                var data = CommonDataService.GetCustomer(id);
                if (data == null)
                    return RedirectToAction("Index");

                ViewBag.Title = "Cập nhật khách hàng";
                return View(data);
            }
            catch
            {
                //    ghi lại log lỗi
                return Content("Có lỗi xảy ra.");
            }
        }

       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
        public ActionResult create()
        {
            var data = new Customer()
            {
                CustomerID = 0
            };
            ViewBag.Title = "Bổ sung khách hàng";
            return View("Edit", data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer data)
        {
            try
            {
                // kiểm soát dữ liệu đầu vào
                if (string.IsNullOrWhiteSpace(data.CustomerName))
                    ModelState.AddModelError("CustomerName", "Tên không được để trống");
                if (string.IsNullOrWhiteSpace(data.ContactName))
                    ModelState.AddModelError("ContactName", "Tên giao dịch không được để trống");
                if (string.IsNullOrWhiteSpace(data.Country))
                    ModelState.AddModelError("Country", "Vui lòng không được để trống");
                if (string.IsNullOrWhiteSpace(data.Address))
                    ModelState.AddModelError("Address", "Vui lòng không được để trống");
                if (string.IsNullOrWhiteSpace(data.City))
                    ModelState.AddModelError("City", "Vui lòng không được để trống");
                if (string.IsNullOrWhiteSpace(data.Email))
                    ModelState.AddModelError("Email", "Vui lòng không được để trống");
                //if (string.IsNullOrWhiteSpace(data.PostalCode))
                //    ModelState.AddModelError("PostalCode", "Vui lòng không được để trống");
                //if (string.IsNullOrWhiteSpace(data.Password))
                //    ModelState.AddModelError("Password", "Vui lòng không được để trống");

                if (!ModelState.IsValid)
                {
                    ViewBag.Title = data.CustomerID == 0 ? "Bổ sung Khách hàng" : " cập nhật Khách hàng";
                    return View("Edit", data);
                }


                if (data.CustomerID == 0)
                {
                    CommonDataService.AddCustomer(data);
                }
                else
                {
                    CommonDataService.UpdateCustomer(data);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                // gọi lại log lỗi 
                return Content(" có lỗi xảy ra , vui lòng thử lại !");
            }


           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(string id)
        {
            try
            {
                int customerId = Convert.ToInt32(id);
                if (Request.HttpMethod == "GET")
                {
                    var data = CommonDataService.GetCustomer(customerId);
                    return View(data);
                }
                else
                {
                    CommonDataService.DeleteCustomer(customerId);
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}