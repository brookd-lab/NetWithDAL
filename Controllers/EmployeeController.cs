using DAL.Data;
using DAL.Model;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPNetMVCCrudWithDAL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _repo;
        private List<Employee> _employees;
        public EmployeeController()
        {
            _repo = new EmployeeRepository(new ApplicationDbContext());
        }
        public ActionResult Index()
        {
            _employees = _repo.GetAllEmployees();
            return View(_employees);
        }

        public Employee GetEmployeeById(int Id)
        {
            var employee = _repo.GetEmployeeById(Id);
            return employee;
        }

        public ActionResult Details(int Id)
        {
            var employee = GetEmployeeById(Id);

            return View(employee);
        }

        public ActionResult Update(int Id)
        {
            var employee = GetEmployeeById(Id);
            return View(employee);
        }

        [HttpPost]
        public ActionResult Update(Employee employee)
        {
            var data = GetEmployeeById(employee.Id);
            if (data != null)
            {
                data.Name = employee.Name;
                data.Salary = employee.Salary;
                _repo.UpdateEmployee(data);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int Id)
        {
            var employee = GetEmployeeById(Id);
            return View(employee);
        }

        [HttpPost]
        public ActionResult Delete(Employee employee)
        {
            _repo.DeleteEmployee(employee.Id);
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            var employee = new Employee();
            return View(employee);
        }

        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            _repo.InsertEmployee(employee);
            return RedirectToAction("Index");
        }


        public ActionResult Privacy()
        {
            return View();
        }
    }
}