using _20T1020441.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _20T1020441.Web.Models
{
    public class EmployeeSearchOutput : PaginationSearchOutput
    {/// <summary>
    /// danh sách nhân viên
    /// </summary>
        public List<Employee> Data { get; set; }
    }
}