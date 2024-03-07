using _20T1020441.BusinessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _20T1020441.DomainModels;
using _20T1020441.Web.Models;

namespace _20T1020411.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [RoutePrefix("product")]
    public class ProductController : Controller
    {
        private const int PAGE_SIZE = 10;
        private const string PRODUCT_SEARCH = "SearchProductCondition";
        /// <summary>
        /// Tìm kiếm, hiển thị mặt hàng dưới dạng phân trang
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {

            ProductSearchInphut condition = Session[PRODUCT_SEARCH] as ProductSearchInphut;

            if (condition == null)
            {
                condition = new ProductSearchInphut()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    SearchValue = "",
                    CategoryID = 0,
                    SupplierID = 0
                };
            }

            return View(condition);
        }
        public ActionResult Search(ProductSearchOutput condition)
        {

            //return Content(condition.PageSize.ToString());
            int rowCount = 0;
            var data = ProductDataService.ListProducts(condition.Page, condition.PageSize, condition.SearchValue,
                condition.SupplierID, condition.CategoryID, out rowCount);
            var result = new ProductSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                SupplierID = condition.SupplierID,
                CategoryID = condition.CategoryID,
                Rowcount = rowCount,
                Data = data
            };
            Session[PRODUCT_SEARCH] = condition;
            return View(result);
        }
        /// <summary>
        /// Tạo mặt hàng mới
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var data = new Product()
            {
                ProductID = 0
            };

            ViewBag.Title = "Bổ sung mặt hàng";
            return View("Create", data);
        }
        /// <summary>
        /// Cập nhật thông tin mặt hàng, 
        /// Hiển thị danh sách ảnh và thuộc tính của mặt hàng, điều hướng đến các chức năng
        /// quản lý ảnh và thuộc tính của mặt hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>        
        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Index");

            var data = ProductDataService.GetProduct(id);
            var model = new ProductModel()
            {
                ProductID = data.ProductID,
                ProductName = data.ProductName,
                SupplierID = data.SupplierID,
                CategoryID = data.CategoryID,
                Photo = data.Photo,
                Unit = data.Unit,
                Price = data.Price,
                Attributes  = ProductDataService.ListAttributes(id),
                Photos = ProductDataService.ListPhotos(id),
            };
            return View(model);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Product data, string price, HttpPostedFileBase uploadPhoto)
        {
            //try
            //{
            //decimal? d = Converter.StringToDecimal(price);
            //if (d == null)
            //    ModelState.AddModelError("Price", "Giá không hợp lệ");
            //else
            //    data.Price = d.Value;

            if (string.IsNullOrWhiteSpace(data.ProductName))
                ModelState.AddModelError("ProductName", "Tên mặt hàng không được để trống");
            if (data.SupplierID <= 0)
                ModelState.AddModelError("SupplierID", "Vui lòng chọn nhà cung cấp");
            if (data.CategoryID <= 0)
                ModelState.AddModelError("CategoryID", "Vui lòng chọn loại hàng");
            if (string.IsNullOrWhiteSpace(data.Unit))
                ModelState.AddModelError("Unit", "Đơn vị tính không được để trống");
            if (data.Price == 0)
                ModelState.AddModelError("Price", "Vui lòng nhập giá");

            if (string.IsNullOrWhiteSpace(data.Photo))
            {
                data.Photo = "";
            }

            if (uploadPhoto != null)
            {
                string path = Server.MapPath("~/Photo");
                string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                string filePath = System.IO.Path.Combine(path, fileName);
                uploadPhoto.SaveAs(filePath);
                data.Photo = fileName;
            }
            var model = new ProductModel()
            {
                ProductID = data.ProductID,
                ProductName = data.ProductName,
                SupplierID = data.SupplierID,
                CategoryID = data.CategoryID,
                Photo = data.Photo,
                Unit = data.Unit,
                Price = data.Price,
                Attributes = ProductDataService.ListAttributes(data.ProductID),
                Photos = ProductDataService.ListPhotos(data.ProductID)
            };
            if (!ModelState.IsValid)
            {
                if (data.ProductID == 0)
                    return View("Create", data);
                else
                    return View("Edit", model);
            }

            if (data.ProductID == 0)
            {
                ProductDataService.AddProduct(data);
            }
            else
                ProductDataService.UpdateProduct(data);
            return RedirectToAction("Index");

            //catch (Exception ex)
            //{
            //    //Ghi lại log lỗi
            //    return Content("Có lỗi xảy ra. Vui lòng thử lại sau!");
            //}
        }
        /// <summary>
        /// Xóa mặt hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
        public ActionResult Delete(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Index");
            if (Request.HttpMethod == "POST")
            {
                ProductDataService.DeleteProduct(id);
                return RedirectToAction("Index");
            }
            var data = ProductDataService.GetProduct(id);
            if (data == null)
                return RedirectToAction("Index");

            return View(data);
        }
        /// <summary>
        /// Lưu các thuộc tính của mặt  hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveAttribute(ProductAttribute data)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(data.AttributeName))
                    ModelState.AddModelError("AttributeName", "Tên thuộc tính không được để trống!");
                if (string.IsNullOrWhiteSpace(data.AttributeValue))
                    ModelState.AddModelError("AttributeValue", "Giá trị thuộc tính không được để trống!");
                if (string.IsNullOrWhiteSpace(data.DisplayOrder.ToString()))
                    ModelState.AddModelError("DisplayOrder", "Thêm thứ tự hiển thị!");
                //if (!ModelState.IsValid)
                //{
                //    return View("Edit", data);
                //}


                if (data.AttributeID == 0)
                {
                    ProductDataService.AddAttribute(data);
                }
                else
                {
                    ProductDataService.UpdateAttribute(data);
                }

                return RedirectToAction($"Edit/{data.ProductID}");
                //return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //Ghi lại log lỗi
                return Content("Có lỗi xảy ra.Vui lòng thử lại sau!" + ex.Message);
            }

        }

        /// <summary>
        /// Lưu ảnh của mặt hàng
        /// </summary>
        /// <param name="data"></param>
        /// <param name="uploadPhoto"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult SavePhoto(ProductPhoto data, HttpPostedFileBase uploadPhoto)
        {
            try
            {
                if (data.PhotoID == 0 && uploadPhoto == null)
                    ModelState.AddModelError("Photo", "Vui lòng thêm ảnh!");
                if (string.IsNullOrWhiteSpace(data.Description))
                    ModelState.AddModelError("Description", "Thêm mô tả ảnh");
                //if (string.IsNullOrWhiteSpace(data.IsHidden.ToString()))
                //    ModelState.AddModelError("IsHidden", "Thêm kiểu hiển thị!");
                if (string.IsNullOrWhiteSpace(data.DisplayOrder.ToString()))
                {
                    ModelState.AddModelError("DisplayOrder", "Vui lòng thêm thứ tự hiển thị ảnh!");
                }
                else if (data.DisplayOrder < 1)
                {
                    ModelState.AddModelError("DisplayOrder", "Thứ tự hiển thị phải là một số tự nhiên không âm");
                }
                else
                {
                    bool isUsedDisplayOrder = false;
                    // kiểm tra nếu DisplayOrder giống nhau
                    List<ProductPhoto> productPhotos = ProductDataService.ListPhotos(data.ProductID);
                    foreach (ProductPhoto item in productPhotos)
                    {
                        if (item.DisplayOrder == data.DisplayOrder && item.PhotoID != data.PhotoID)
                        {
                            isUsedDisplayOrder = true;
                            break;
                        }
                    }
                    if (isUsedDisplayOrder)
                    {
                        ModelState.AddModelError("DisplayOrder", $"Thứ tự hiển thị {data.DisplayOrder} đã được dùng");
                    }
                }
                data.IsHidden = Convert.ToBoolean(data.IsHidden.ToString());
                //if (!ModelState.IsValid)
                //{
                //    return View("");
                //}

                if (!ModelState.IsValid)
                {
                    ViewBag.Title = data.PhotoID == 0 ? "Bổ sung ảnh" : "Thay đổi ảnh";
                    return View("Photo", data);
                }
                if (uploadPhoto != null)
                {
                    string path = Server.MapPath("~/Photo");
                    string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                    string filePath = System.IO.Path.Combine(path, fileName);
                    uploadPhoto.SaveAs(filePath);
                    data.Photo = fileName;
                }

                if (data.PhotoID == 0)
                {
                    ProductDataService.AddPhoto(data);
                }
                else
                {
                    ProductDataService.UpdatePhoto(data);
                }
                return RedirectToAction($"Edit/{data.ProductID}");
            }
            catch (Exception ex)
            {
                //Ghi lại log lỗi
                return Content("Có lỗi xảy ra.Vui lòng thử lại sau!" + ex.Message);
            }

        }
        /// <summary>
        /// Các chức năng quản lý ảnh của mặt hàng
        /// </summary>
        /// <param name="method"></param>
        /// <param name="productID"></param>
        /// <param name="photoID"></param>
        /// <returns></returns>
        [Route("photo/{method?}/{productID?}/{photoID?}")]
        public ActionResult Photo(string method = "add", int productID = 0, long photoID = 0)
        {
            if (productID < 0)
            {
                return RedirectToAction("Index");
            }
            switch (method)
            {
                case "add":
                    var data = new ProductPhoto()
                    {
                        PhotoID = 0,
                        ProductID = productID
                    };
                    ViewBag.Title = "Bổ sung ảnh";
                    return View(data);
                case "edit":
                    if (photoID <= 0)
                        return RedirectToAction($"Edit/{productID}");

                    data = ProductDataService.GetPhoto(photoID);
                    if (data == null)
                        return RedirectToAction($"Edit/{productID}");
                    ViewBag.Title = "Thay đổi ảnh";
                    return View(data);
                case "delete":
                    ProductDataService.DeletePhoto(photoID);
                    return RedirectToAction($"Edit/{productID}"); //return RedirectToAction("Edit", new { productID = productID });
                default:
                    return RedirectToAction($"Edit/{productID}");
            }
        }
        //public ActionResult Photo(string method = "add", int productID = 0, long photoID = 0)
        //{
        //    switch (method)
        //    {
        //        case "add":
        //            ProductPhoto data = new ProductPhoto() { ProductID = productID };
        //            ViewBag.Title = "Bổ sung ảnh";
        //            return View(data);
        //        case "edit":
        //            ProductPhoto dataPhoto = ProductDataService.GetPhoto(photoID);
        //            ViewBag.Title = "Thay đổi ảnh";
        //            return View(dataPhoto);

        //        case "delete":
        //            ProductDataService.DeletePhoto(photoID);
        //            return RedirectToAction($"Edit/{productID}");
        //        //return RedirectToAction("Edit", new { productID = productID });
        //        default:
        //            return RedirectToAction("Index");
        //    }
        //}

        /// <summary>
        /// Các chức năng quản lý thuộc tính của mặt hàng
        /// </summary>
        /// <param name="method"></param>
        /// <param name="productID"></param>
        /// <param name="attributeID"></param>
        /// <returns></returns>
        [Route("attribute/{method?}/{productID}/{attributeID?}")]
        public ActionResult Attribute(string method = "add", int productID = 0, int attributeID = 0)
        {
            switch (method)
            {
                case "add":
                    ProductAttribute data = new ProductAttribute() { ProductID = productID };
                    ViewBag.Title = "Bổ sung thuộc tính";
                    return View(data);
                case "edit":
                    ProductAttribute dataAttribute = ProductDataService.GetAttribute(attributeID);
                    ViewBag.Title = "Thay đổi thuộc tính";
                    return View(dataAttribute);
                case "delete":
                    ProductDataService.DeleteAttribute(attributeID);
                    return RedirectToAction($"Edit/{productID}");
                //return RedirectToAction("Edit", new { productID = productID });
                default:
                    return RedirectToAction("Index");
            }
        }


    }
}