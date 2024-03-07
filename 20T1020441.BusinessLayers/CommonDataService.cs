using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _20T1020441.DataLayers;
using _20T1020441.DomainModels;
using System.Configuration;
namespace _20T1020441.BusinessLayers
{
    /// <summary>
    /// 
    /// </summary>
    public static class CommonDataService
    {
        private static ICountryDAL countryDB;
        private static ICommonDAL<Supplier> supplierDB;
        private static ICommonDAL<Shipper> shipperDB;
        private static ICommonDAL<Employee> employeeDB;
        private static ICommonDAL<Customer> customerDB;
        private static ICommonDAL<Category> categoryDB;
        /// <summary>
        /// Ctor
        /// </summary>
        static CommonDataService()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            countryDB = new _20T1020441.DataLayers.SQLServer.CountryDAL(connectionString);
            supplierDB = new _20T1020441.DataLayers.SQLServer.SupplierDAL(connectionString);
            shipperDB = new _20T1020441.DataLayers.SQLServer.ShipperDAL(connectionString);
            employeeDB = new _20T1020441.DataLayers.SQLServer.EmployeeDAL(connectionString);
            customerDB = new _20T1020441.DataLayers.SQLServer.CustomerDAL(connectionString);
            categoryDB = new _20T1020441.DataLayers.SQLServer.CategoryDAL(connectionString);
        }
        #region Các nghiệp vụ liên quan đến quốc qia
        /// <summary>
        /// Lấy danh sách các quốc gia
        /// </summary>
        /// <returns></returns>
        public static List<Country> ListOfCountries()
        {
            return countryDB.List().ToList();
        }
        #endregion
        #region Các nghiệp vụ liên quan đến nhà cung cấp
        /// <summary>
        /// Tìm kiếm các NCC dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang càn xem</param>
        /// <param name="pageSize">Số dòng trên mỗi trang</param>
        /// <param name="sercchValue">Giá trị tìm kiếm </param>
        /// <param name="rowCount">Output: Tổng số dòng tièm được</param>
        /// <returns></returns>
        public static List<Supplier> ListOfSuppliers(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = supplierDB.Count(searchValue);
            return supplierDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// Tìm kiếm NCC dưới dạng không phân trang
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Supplier> ListOfSuppliers(string searchValue)
        {
            return supplierDB.List(1, 0, searchValue).ToList();
        }
        public static List<Supplier> ListOfSuppliers()
        {
            return supplierDB.List().ToList();
        }
        public static int AddSupplier(Supplier data)
        {
            return supplierDB.Add(data);
        }
        public static bool UpdateSupplier(Supplier data)
        {
            return supplierDB.Update(data);
        }
        public static bool DeleteSupplier(int supplierID)
        {
            return supplierDB.Delete(supplierID);
        }
        public static Supplier GetSupplier(int supplierID)
        {
            return supplierDB.Get(supplierID);
        }
        public static bool InUsedSupplier(int supplierID)
        {
            return supplierDB.InUsed(supplierID);
        }
        #endregion
        #region Các nghiệp vụ liên quan đến khách hàng
        /// <summary>
        /// Tìm kiếm khách hàng dưới dạng phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Customer> ListOfCustomers(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = customerDB.Count(searchValue);
            return customerDB.List(page, pageSize, searchValue).ToList();
        }
        public static List<Customer> ListOfCustomers(string searchValue)
        {
            return customerDB.List(1, 0, searchValue).ToList();
        }
        public static int AddCustomer(Customer data)
        {
            return customerDB.Add(data);
        }
        public static bool UpdateCustomer(Customer data)
        {
            return customerDB.Update(data);
        }
        public static bool DeleteCustomer(int customerId)
        {
            return customerDB.Delete(customerId);
        }
        public static Customer GetCustomer(int customerId)
        {
            return customerDB.Get(customerId);
        }
        public static bool InUsedCustomer(int customerId)
        {
            return customerDB.InUsed(customerId);
        }
        #endregion
        #region Các nghiệp vụ liên quan đến người giao hàng
        public static List<Shipper> ListOfShippers(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = shipperDB.Count(searchValue);
            return shipperDB.List(page, pageSize, searchValue).ToList();
        }
        public static List<Shipper> ListOfShippers(string searchValue)
        {
            return shipperDB.List(1, 0, searchValue).ToList();
        }
        public static int AddShipper(Shipper data)
        {
            return shipperDB.Add(data);
        }
        public static bool UpdateShipper(Shipper data)
        {
            return shipperDB.Update(data);
        }
        public static bool DeleteShipper(int shipperID)
        {
            return shipperDB.Delete(shipperID);
        }
        public static Shipper GetShipper(int shipperID)
        {
            return shipperDB.Get(shipperID);
        }
        public static bool InUsedShipper(int shipperID)
        {
            return shipperDB.InUsed(shipperID);
        }
        #endregion
        #region Các nghiệp vụ liên quan đến loại hàng
        public static List<Category> ListOfCategories(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = categoryDB.Count(searchValue);
            return categoryDB.List(page, pageSize, searchValue).ToList();
        }
        public static List<Category> ListOfCategories(string searchValue)
        {
            return categoryDB.List(1, 0, searchValue).ToList();
        }
        public static List<Category> ListOfCategories()
        {
            return categoryDB.List().ToList();
        }
        public static int AddCategory(Category data)
        {
            return categoryDB.Add(data);
        }
        public static bool UpdateCategory(Category data)
        {
            return categoryDB.Update(data);
        }
        public static bool DeleteCategory(int categoryId)
        {
            return categoryDB.Delete(categoryId);
        }
        public static Category GetCategory(int categoryId)
        {
            return categoryDB.Get(categoryId);
        }
        public static bool InUsedCategory(int categoryId)
        {
            return categoryDB.InUsed(categoryId);
        }
        #endregion
        #region Các nghiệp vụ liên quan đến nhân viên
        public static List<Employee> ListOfEmployees(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = employeeDB.Count(searchValue);
            return employeeDB.List(page, pageSize, searchValue).ToList();
        }
        public static List<Employee> ListOfEmployees(string searchValue)
        {
            return employeeDB.List(1, 0, searchValue).ToList();
        }
        public static int AddEmployee(Employee data)
        {
            return employeeDB.Add(data);
        }
        public static bool UpdateEmployee(Employee data)
        {
            return employeeDB.Update(data);
        }
        public static bool DeleteEmployee(int employeeID)
        {
            return employeeDB.Delete(employeeID);
        }
        public static Employee GetEmployee(int employeeID)
        {
            return employeeDB.Get(employeeID);
        }
        public static bool InUsedEmployee(int employeeID)
        {
            return employeeDB.InUsed(employeeID);
        }
        #endregion
    }
}
