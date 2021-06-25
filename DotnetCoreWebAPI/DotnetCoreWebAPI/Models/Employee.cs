using System;
using System.Collections.Generic;

namespace DotnetCoreWebAPI.Models
{
    public partial class Employee
    {
        public Employee()
        {
        }

        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string EmailAddress { get; set; }
    }
}
