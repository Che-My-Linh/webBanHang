using _20T1020441.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20T1020441.DataLayers
{
    /// <summary>
    /// định nghĩa các phép xử lý liên quan ddeeens dữ liệu tài khoản của người dùng
    /// </summary>
    public interface IUserAccountDAL
    {
        /// <summary>
        /// kiểm tra tên đăng nhập và mật khẩu có hợp lệ hay không
        /// Nếu hợp lệ thì trả về thông tin của tài khoản, ngược lại trả về null
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        UserAccount Authorize(string userName, string password);

        /// <summary>
        /// Đổi mật khẩu 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        bool ChangePassword(string userName, string oldPassword, string newPassword);
    }
}