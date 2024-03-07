using _20T1020441.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _20T1020441.Web.Models
{/// <summary>
/// timf kieems nhaf cung caaps duowis dang phan trang
/// </summary>
    public class SupplierSearchOutput: PaginationSearchOutput
    {/// <summary>
    /// danh sách nhà cung cấp
    /// </summary>
        public List<Supplier>Data { get; set; }

        
    }
}