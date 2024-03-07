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
    public class ShipperController : Controller
    {
        // GET: Shipper
        private const int PAGE_SIZE = 5;
        private const string SHIPPER_SEARCH = "SearchShipperCondition";

        public ActionResult Index()
        {

            PaginationSearchInput Condition = Session[SHIPPER_SEARCH] as PaginationSearchInput;
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
            var data = CommonDataService.ListOfShippers(Condition.Page, Condition.PageSize, Condition.SearchValue, out RowCount);
            var result = new ShipplerSearchOutput()
            {
                Page = Condition.Page,
                PageSize = Condition.PageSize,
                SearchValue = Condition.SearchValue,
                Rowcount = RowCount,
                Data = data

            }; Session[SHIPPER_SEARCH] = Condition;

            return View(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        public ActionResult Create()
        {
            var data = new Shipper()
            {
                ShipperID = 0
            };
            ViewBag.Title = "Bổ sung người giao hàng";
            return View("Edit", data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id =0)
        {
            try
            {
                var data = CommonDataService.GetShipper(id);
                if (data == null)
                    return RedirectToAction("Index");

                ViewBag.Title = "Cập nhật Nguời giao hàng";
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
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Shipper data)
        {
            try
            {
                // kiểm soát dữ liệu đầu vào
                if (string.IsNullOrWhiteSpace(data.ShipperName))
                    ModelState.AddModelError("ShipperName", "Tên  người giao hàng không được để trống");
                if (string.IsNullOrWhiteSpace(data.Phone))
                    ModelState.AddModelError("Phone", "Số điện thoại không được để trống");
                

                if (!ModelState.IsValid)
                {
                    ViewBag.Title = data.ShipperID == 0 ? "Bổ sung nhà cung cấp" : " cập nhật nhà cung cấp";
                    return View("Edit", data);
                }


                if (data.ShipperID == 0)
                {
                    CommonDataService.AddShipper(data);
                }
                else
                {
                    CommonDataService.UpdateShipper(data);
                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                // gọi lại log lỗi 
                return Content(" có lỗi xảy ra , vui lòng thử lại !");
            }


            
        }
       
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(string id)
        {

            try
            {
                int shipperId = Convert.ToInt32(id);
                if (Request.HttpMethod == "GET")
                {
                    var data = CommonDataService.GetShipper(shipperId);
                    return View(data);
                }
                else
                {
                    CommonDataService.DeleteShipper(shipperId);
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