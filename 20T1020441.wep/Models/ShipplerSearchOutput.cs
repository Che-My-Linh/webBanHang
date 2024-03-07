using _20T1020441.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _20T1020441.Web.Models
{
    public class ShipplerSearchOutput : PaginationSearchOutput
    {/// <summary>
    /// danh sách người giao hàng
    /// </summary>
        public List<Shipper> Data { get; set; }
    }
}