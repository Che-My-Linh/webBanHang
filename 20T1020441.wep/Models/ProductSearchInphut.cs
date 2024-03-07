using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _20T1020441.Web.Models;

namespace _20T1020441.Web.Models
{
    public class ProductSearchInphut : PaginationSearchInput
    {
        public int CategoryID { get; set; } 

        public int SupplierID { get; set; } 
    }
}