using _20T1020441.BusinessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _20T1020441.DomainModels;
using _20T1020441.Web.Models;

namespace _20T1020441.Web.Controllers
{[Authorize]
    public class CategoryController : Controller
    {
        // GET: Category
        private const int PAGE_SIZE = 5;
        private const string CATEGORY_SEARCH = "SearchCategoryCondition";

        public ActionResult Index()
        {

            PaginationSearchInput Condition = Session[CATEGORY_SEARCH] as PaginationSearchInput;
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
            var data = CommonDataService.ListOfCategories(Condition.Page, Condition.PageSize, Condition.SearchValue, out RowCount);
            var result = new CategorySearchOutput()
            {
                Page = Condition.Page,
                PageSize = Condition.PageSize,
                SearchValue = Condition.SearchValue,
                Rowcount = RowCount,
                Data = data

            }; Session[CATEGORY_SEARCH] = Condition;

            return View(result);
        }

        public ActionResult Edit(int id = 0)
        {
            try
            {
                var data = CommonDataService.GetCategory(id);
                if (data == null)
                    return RedirectToAction("Index");

                ViewBag.Title = "Cập nhật loại hàng";
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
        public ActionResult Save(Category data)
        {

            try
            {
                // Kiểm soát đàu vào 
                if (string.IsNullOrWhiteSpace(data.CategoryName))
                    ModelState.AddModelError("CategoryName", "Tên không được để trống");
                if (string.IsNullOrWhiteSpace(data.Description))
                    ModelState.AddModelError("Description", "Tên giao dịch không được để trống");
                ;
                if (!ModelState.IsValid)
                {
                    ViewBag.Title = data.CategoryID == 0 ? "Bổ sung nhà cung cấp" : "Cập nhật nhà cung cấp";
                    return View("Edit", data);
                }
                if (data.CategoryID == 0)
                {
                    CommonDataService.AddCategory(data);
                }
                else
                {
                    CommonDataService.UpdateCategory(data);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //    ghi lại log lỗi
                return Content("Có lỗi xảy ra.");
            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var data = new Category()
            {
                CategoryID = 0
            };
            ViewBag.Title = "Bổ sung loại hàng";
            return View("Edit",data);
        }

        public ActionResult Delete(string id)
        {
            try
            {
                int categoryId = Convert.ToInt32(id);
                if (Request.HttpMethod == "GET")
                {
                    var data = CommonDataService.GetCategory(categoryId);
                    return View(data);
                }
                else
                {
                    CommonDataService.DeleteCategory(categoryId);
                    return RedirectToAction($"Index/{categoryId}");
                }
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

    }
}