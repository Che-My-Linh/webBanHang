using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20T1020441.DomainModels
{
    /// <summary>
    /// Thuộc tính của mặt hàng
    /// </summary>
    public class ProductAttribute
    {
        ///<summary>
        ///
        ///</summary>
        public long AttributeID { get; set; }
        ///<summary>
        ///
        ///</summary>
        public int ProductID { get; set; }
        ///<summary>
        ///
        ///</summary>
        public string AttributeName { get; set; }
        ///<summary>
        ///
        ///</summary>
        public string AttributeValue { get; set; }
        ///<summary>
        ///
        ///</summary>
        public int DisplayOrder { get; set; }
    }

}
