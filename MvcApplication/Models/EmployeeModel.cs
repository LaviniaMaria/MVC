using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace MvcApplication.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IdManager { get; set; }
        public string NameManager { get; set; }
        public double Salary { get; set; }
        public List<SelectListItem> employeesList { get; set; }
    }
}