using PetCareBd1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.Data.SqlClient;

namespace PetCareBd1.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: Employees
        public ActionResult employeeview()
        {
            PETCAREEntities ef = new PETCAREEntities();
            List<employees> empleados = ef.employees.SqlQuery("select * from Employees").ToList<employees>();


            return View(empleados);
        }



        [HttpPost]
        public ActionResult AddEmployee (String employeeName, String employeeMail, Decimal employeeSalary, DateTime employeeBirthday, String employeePhone)
        {
            PETCAREEntities ef = new PETCAREEntities();
            List<object> lista = new List<object>();
            lista.Add(employeeName);
            lista.Add(employeeMail);
            lista.Add(employeeSalary);
            lista.Add(employeeBirthday);
            lista.Add(employeePhone);
            object[] arr = lista.ToArray();


            int rowAffected = ef.Database.ExecuteSqlCommand("insert into Employees (employeeName,employeeMail,employeeSalary,employeeBirthday,employeePhone) values (@p0,@p1,@p2,@p3,@p4)",arr);
            if (rowAffected > 0)
            {
                Debug.WriteLine("executed");
                return Redirect("employeeview");
            }
            return Redirect("employeeview");
        }

        public ActionResult DeleteEmployee(int ?id)
        {
            PETCAREEntities db = new PETCAREEntities();
            Debug.WriteLine(id);
            if (id != null)
            {

                db.Database.ExecuteSqlCommand("delete from employees where employeeId = @id", new SqlParameter("id", id));
                return RedirectToAction("employeeview","Employees");
            }

            return View();
        }

        public int ?id;
        [HttpPost]
        public ActionResult EditEmployee(int ?idemp ,String employeeName, String employeeMail, Decimal employeeSalary, DateTime employeeBirthday, String employeePhone)
        {
            PETCAREEntities ef = new PETCAREEntities();
            List<object> lista = new List<object>();
            lista.Add(employeeName);
            lista.Add(employeeMail);
            lista.Add(employeeSalary);
            lista.Add(employeeBirthday);
            lista.Add(employeePhone);
        

            Debug.WriteLine(idemp);
            lista.Add(idemp);
            Debug.WriteLine("ID consumido "+idemp);
            object[] arr = lista.ToArray();


            int rowAffected = ef.Database.ExecuteSqlCommand("update Employees set employeeName=@p0, employeeMail=@p1, employeeSalary=@p2, employeeBirthday=@p3,employeePhone=@p4 where employeeId = @p5", arr);
            if (rowAffected > 0)
            {
                Debug.WriteLine("executed");
                return Redirect("employeeview");
            }
            return Redirect("employeeview");
        }

        public ActionResult GetEmployeesToEdit (int ?id)
        {
            PETCAREEntities ef = new PETCAREEntities();
            employees emp = null;
            Debug.WriteLine(id);
            this.id = id;
            
            
            if (id!= null)
            {
                 emp = ef.employees.SqlQuery("select * from employees where employeeId = @id", new SqlParameter("id", id)).FirstOrDefault();
            }
           
            if (emp!= null)
            {
                Debug.WriteLine(emp.employeeName);
            
            }


            return View(emp);
        }

     
    }
}