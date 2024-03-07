using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _20T1020441.DomainModels;

namespace _20T1020441.Web.Models
{
    public class ProductModel : Product
    {
        public List<ProductAttribute> Attributes { get; set; }
        public List<ProductPhoto> Photos { get; set; }

    }
}