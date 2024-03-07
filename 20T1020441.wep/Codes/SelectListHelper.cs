using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _20T1020441.BusinessLayers;

using _20T1020441.DomainModels;


namespace _20T1020441.Web
{
    /// <summary>
    /// một số tiện ích liên quan đến SelectList
    /// </summary>
    public static class SelectListHelper
    {
        public static List<SelectListItem> County() {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem {
                Value = "",
                Text = "----Chọn quốc gia----",
            });
            foreach (var item in CommonDataService.ListOfCountries())
            {
                list.Add(new SelectListItem(){
                    Value = item.CountryName,
                    Text = item.CountryName,
                });
            }
            return list;
        }
      
        public static List<SelectListItem> Category()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem
            {
                Value = "0",
                Text = "----Chọn Loại hàng----",
            });
            foreach (var item in CommonDataService.ListOfCategories(""))
            {
                list.Add(new SelectListItem()
                {
                    Value = item.CategoryID.ToString(),
                    Text = item.CategoryName,
                });
            }
            return list;
        }

        public static List<SelectListItem> Supplier()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem
            {
                Value = "0",
                Text = "----Chọn Loại hàng----",
            });
            foreach (var item in CommonDataService.ListOfSuppliers(""))
            {
                list.Add(new SelectListItem()
                {
                    Value = item.SupplierID.ToString(),
                    Text = item.SupplierName,
                });
            }
            return list;
        }

       

    }
}