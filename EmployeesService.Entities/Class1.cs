using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeesManager.Entities;
using System.Data;
using System.Data.SqlClient;

namespace EmployeesService.Entities
{
    public class EmployeeService
    {
        public List<Employee> getAll()
        {
            List<Employee> employeesList = new List<Employee>();
            /*
            Employee e1 = new Employee(1, "e1", null, 2000);
            Employee e2 = new Employee(2, "e2", e1, 1000);
            Employee e3 = new Employee(3, "e3", e1, 500);

            employeesList.Add(e1);
            employeesList.Add(e2);
            employeesList.Add(e3);

            return employeesList;*/

            SqlConnection conn = null;
            SqlDataReader rdr = null;

            try
            {
                // create and open a connection object
                conn = new
                    SqlConnection("Server=(local);DataBase=MyDatabase;Integrated Security=SSPI");
                conn.Open();

                // 1. create a command object identifying
                // the stored procedure
                SqlCommand cmd = new SqlCommand(
                    "populate", conn);

                // 2. set the command object so it knows
                // to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // execute the command
                rdr = cmd.ExecuteReader();

                Employee employee,manager;

                // iterate through results, printing each to console
                while (rdr.Read())
                {
                    if (rdr["mgrid"]==DBNull.Value)
                        manager = null;
                    else
                    {
                        int mgrId = (int)rdr["mgrid"];
                        manager = employeesList.Find(x => x.Id == mgrId);
                    }
                    
                    employee = new Employee((int)rdr["empid"],(string)rdr["ename"],manager,Convert.ToDouble(rdr["salary"]));
                    employeesList.Add(employee);
                }

                return employeesList;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
                if (rdr != null)
                {
                    rdr.Close();
                }
            }

        }

       
    }

}
