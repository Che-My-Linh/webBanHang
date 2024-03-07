using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _20T1020441.DomainModels;
 

namespace _20T1020441.DataLayers
{/// <summary>
/// định nghĩa các phép chung cho các dữ liệu
/// đơn giản trên cac bảng 
/// </summary>
    public interface ICommonDAL<T> where T : class
    {
        
            /// <summary>
            /// tìm kiếm và lấy danh sách dưới dạng có phân trang
            /// </summary>
            /// <param name="page">trang mà chúng ta cần hiển thị</param>
            /// <param name="pageSize">số dòng hiển thị trên mỗi trang, pagesize =0 hiển thị không phân trang </param>
            /// <param name="seachValue">gia trị tìm kiếm</param>
            /// <returns></returns>
            IList<T> List(int page = 1, int pageSize = 0, string seachValue = "");
            /// <summary>
            /// đếm số nhà cung cấp cần tìm 
            /// </summary>
            /// <param name="seachValue">tìm kiếm theo tên </param>
            /// <returns></returns>
            int Count(string seachValue = "");
            /// <summary>
            /// Bổ sung thêm nhà cung cấp
            /// </summary>
            /// <param name="data"></param>
            /// <returns>nhà cung cấp được bổ sung</returns>
            int Add(T data);
            /// <summary>
            /// cập nhật thông tin nhà cung cấp
            /// 
            /// </summary>
            bool Update(T date);
            /// <summary>
            /// xoa thong tin cua mot nha cung cap
            /// </summary>
            /// <param name=""></param>
            /// <returns></returns>
            bool Delete(int id);
            /// <summary>
            /// 
            /// </summary>
            /// <param name="SupplierID"></param>
            /// <returns></returns>
            T Get(int id);
            /// <summary>
            /// kiểm tra dữ liệu này có hay khong
            /// </summary>
            bool InUsed(int id);
        }
    }

