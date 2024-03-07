using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _20T1020441.DomainModels;

namespace _20T1020441.DataLayers
{
/// <summary>
/// các chức năng sử lí dữ liệu
/// </summary>
    
    public interface ICountryDAL
    {
        /// <summary>
        /// lấy danh sách tất cả các quốc gia
        /// </summary>
        IList<Country> List();
        
    }
}
