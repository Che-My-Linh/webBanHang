using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _20T1020441.Web.Models
{/// <summary>
/// lấy cơ sở cho các lớp dùng để lưu trữ kết quả tìm kiếm dưới dang phân trang
/// </summary>
    public abstract class PaginationSearchOutput
    {
        /// <summary>
        /// 
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// 
        /// pa
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// gias trij ddang ddc timf kieems
        /// </summary>
        public string SearchValue { get; set; }
        /// <summary>
        /// số dòng tìm đc 
        /// ko t
        /// </summary>

        public int Rowcount { get; set; }
        /// <summary>
        /// Tong soos trang
        /// </summary>
        public int PageCount
        {
            get
            {
                if (PageSize == 0)
                    return 1;
                int p = Rowcount / PageSize;
                if (Rowcount % PageSize > 0) p += 1;
                return p;

            }
        }

        public class ProductSearchOutput : PaginationSearchOutput
        {
            public int CategoryID { get; set; }

            public int SupplierID { get; set; }
        }
    }
}
