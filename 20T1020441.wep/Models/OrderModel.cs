using _20T1020441.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _20T1020441.Web.Models
{
    public class OrderModel : Order
    {
        public List<OrderDetail> Details { get; set; }

        //public List<OrderStatus> Status { get; set; }
    }
}