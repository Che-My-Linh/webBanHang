using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _20T1020441.web.Controllers
{
    
    public class TestController : Controller
    {
        public ActionResult Input()
        {
            Person p = new Person()
            {
                
            };
            return View(p);

        }
        [HttpPost]
        public ActionResult Input(Person p)
        {
            var data = new
            {
                Name = p.Name,
                Birthday = string.Format("{0:dd/MM/YYYY}",p.Birthday),
                Salary = p.Salary
            };

            return Json(p, JsonRequestBehavior.AllowGet);

        }

        public string TestDate(string value)
        {
            string d = value;
            return String.Format("{0:dd/MM/yyyy}", d);
        }

    }

    public class Person
    {
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public float Salary { get; set; }
    }
}