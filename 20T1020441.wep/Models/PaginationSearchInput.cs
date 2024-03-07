using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _20T1020441.Web.Models
{
    /// <summary>
    /// Biểu diễn dữ liệu đàu vào để tìm kiếm phân trang
    /// </summary>
    public class PaginationSearchInput
    {/// <summary>
    /// trang caand hiển thị
    /// </summary>
        public int Page { get; set; }
/// <summary>
/// số dòng hiển thị trên mỗi trang
/// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// giá trị cần hiển thị
        /// </summary>
        public string SearchValue { get; set; }

        public class ProductSearchInput : PaginationSearchInput
        {
            public int CategoryID { get; set; }

            public int SupplierID { get; set; }
        }

    }
}