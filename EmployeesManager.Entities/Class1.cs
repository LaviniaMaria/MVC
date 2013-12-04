using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesManager.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Employee Manager { get; set; }
        public double Salary { get; set; }

        public Employee()
        {

        }
        public Employee(int id, string name, Employee manager, double salary)
        {
            this.Id = id;
            this.Name = name;
            this.Manager = manager;
            this.Salary = salary;
        }

    }
}
