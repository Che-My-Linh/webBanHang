using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _20T1020441.DomainModels;

namespace _20T1020441.DataLayers
{
    /// <summary>
    /// định nghĩa các phép dữ liệu trên nhà cung cấp
    /// (sủ dụng cách này dẫn đến viết lặp đi lặp lại các kiểu code giồng nhau
    /// cho các đối tượng dữ liệu tương tự như Customer, ...., ko dùng
    /// 
    /// </summary>
    public interface ISupplierDAL
    {
        /// <summary>
        /// tìm kiếm và lấy danh sách các nhà cung cấp dưới dạng có phân trang
        /// </summary>
        /// <param name="page">trang mà chúng ta cần hiển thị</param>
        /// <param name="pageSize">số dòng hiển thị trên mỗi trang, pagesize =0 hiển thị không phân trang </param>
        /// <param name="seachValue">Tên cần tìm kiếm , chuỗi rỗng nếu không tìm kiếm theo tên</param>
        /// <returns></returns>
        IList<Supplier> list(int page = 1, int pageSize = 0, string seachValue ="");
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
        int Add(Supplier data);
        /// <summary>
        /// cập nhật thông tin nhà cung cấp
        /// 
        /// </summary>
        bool Update(Supplier date);
            /// <summary>
            /// xoa thong tin cua mot nha cung cap
            /// </summary>
            /// <param name=""></param>
            /// <returns></returns>
        bool Delete(int SupplierID);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SupplierID"></param>
        /// <returns></returns>
        Supplier get(int SupplierID);
            /// <summary>
            /// kiểm tra dữ liệu này có hay khong
            /// </summary>
        bool InUsed(int SupplierID);

        IList<Category> List();
    }
}
