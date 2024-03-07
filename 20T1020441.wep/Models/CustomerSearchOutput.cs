using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _20T1020441.DomainModels;

namespace _20T1020441.Web.Models
{
    public class CustomerSearchOutput : PaginationSearchOutput
    {/// <summary>
    /// danh sách khách hàng
    /// </summary>
        public List<Customer> Data { get; set; } 
    }
}