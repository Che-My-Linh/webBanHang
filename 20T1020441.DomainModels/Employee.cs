﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20T1020441.DomainModels
{
    public class Employee
    {
        public int EmployeeID { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public DateTime BirthDate { get; set; }

        public string Photo { get; set; }

        public String Notes { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        
    }
}
