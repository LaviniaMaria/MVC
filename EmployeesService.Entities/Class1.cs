using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeesManager.Entities;

namespace EmployeesService.Entities
{
    public class EmployeeService
    {
        public List<Employee> getAll()
        {
            List<Employee> employeesList = new List<Employee>();
            Employee e1 = new Employee(1, "e1", null, 2000);
            Employee e2 = new Employee(2, "e2", e1, 1000);
            Employee e3 = new Employee(3, "e3", e1, 500);

            employeesList.Add(e1);
            employeesList.Add(e2);
            employeesList.Add(e3);

            return employeesList;

        }

    }

}
