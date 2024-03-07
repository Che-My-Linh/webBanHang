using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _20T1020441.DomainModels;

namespace _20T1020441.DataLayers
{
    interface ICategoryDAL
    {/// <summary>
    /// Lấy danh sách loại hàng
    /// </summary>
    /// <returns></returns>
        IList<Category> List();

    }
}
