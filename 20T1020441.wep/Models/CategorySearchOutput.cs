using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _20T1020441.DomainModels;
namespace _20T1020441.Web.Models
{
    public class CategorySearchOutput : PaginationSearchOutput
    {/// <summary>
    /// danh sách Category
    /// </summary>
        public List<Category> Data { get; set; }
    }
}