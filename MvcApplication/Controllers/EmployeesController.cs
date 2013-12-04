using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeesService.Entities;
using EmployeesManager.Entities;
using MvcApplication.Models;

namespace MvcApplication.Controllers
{
    public class EmployeesController : Controller
    {
        
        public ActionResult Index()
        {
            EmployeeService empServ = new EmployeeService();
            List<Employee> employeesList;
            List<EmployeeModel> employeeModelList=new List<EmployeeModel>();

            if (Session["list"] == null)
            {
                employeesList = empServ.getAll();
                Session["list"] = employeesList;
            }

            else
                employeesList =(List<Employee>) Session["list"];

            foreach(var emp in employeesList) {

                EmployeeModel employeeModel = getEmployeeModel(emp);

                employeeModelList.Add(employeeModel);

            }
            return View(employeeModelList);
        }


        public EmployeeModel getEmployeeModel(Employee employee)
        {
            EmployeeModel employeeModel = new EmployeeModel();
            employeeModel.Id = employee.Id;
            employeeModel.Name = employee.Name;
            employeeModel.Salary = employee.Salary;

            Employee manager = employee.Manager;
            if (manager != null)
            {
                employeeModel.IdManager = manager.Id;
                employeeModel.NameManager = manager.Name;
            }

            return employeeModel;
        }

        public Employee getEmployeeById(int id)
        {
            List<Employee> listEmployees = (List<Employee>)Session["list"];
            Employee employee = listEmployees.Find(x => x.Id == id);
            return employee;
        }

        public List<SelectListItem> getDropDownList()
        {
            List<SelectListItem> myList = new List<SelectListItem>();
            List<Employee> employeesList = (List<Employee>)Session["list"];

            foreach (var emp in employeesList)
            {

                SelectListItem item = new SelectListItem();
                item.Text = emp.Name;
                item.Value = emp.Id.ToString();
                myList.Add(item);

            }

            return myList;

        }

        public ActionResult Create()
        {

            ViewBag.MyList = getDropDownList();
           
            return View();
        }

        //
        // POST: /Default1/Create

        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            try
            {
                List<Employee> l=(List<Employee>)Session["list"];
                employee.Id = l.Max(x => x.Id)+1;
                employee.Manager = getEmployeeById(employee.Manager.Id);
                l.Add(employee);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {

                List<Employee> listEmployees = (List<Employee>)Session["list"];
                Employee employeeToDelete = getEmployeeById(id);

                for (int i = 0; i < listEmployees.Count(); i++)
                {
                    Employee emp = listEmployees.ElementAt(i);
                    if (emp.Manager != null && emp.Manager.Id == employeeToDelete.Id)
                        emp.Manager = null;

                }

                listEmployees.Remove(employeeToDelete);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
                Employee employeeToEdit = getEmployeeById(id);        
                ViewBag.id = employeeToEdit.Manager.Id;
                ViewBag.MyList=getDropDownList();

                return View(employeeToEdit);
           
        }

        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            try
            {

                Employee employeeToEdit = getEmployeeById(employee.Id);
                Employee manager = getEmployeeById(employee.Manager.Id);

                employeeToEdit.Manager = manager;
                employeeToEdit.Name = employee.Name;
                employeeToEdit.Salary = employee.Salary;
               
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Details(int id)
        {

            Employee employee = getEmployeeById(id);
            EmployeeModel employeeModel = getEmployeeModel(employee);

            return View(employeeModel);
        }

    }
}
