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
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult customerview()
        {
            PETCAREEntities ef = new PETCAREEntities();
            List<customers> clientes = ef.customers.SqlQuery("select * from Customers").ToList<customers>();


            return View(clientes);
        }

        [HttpPost]
        public ActionResult AddCustomer(String customerName, String customerMail, String customerPhone)
        {
            PETCAREEntities ef = new PETCAREEntities();
            List<object> lista = new List<object>();
            lista.Add(customerName);
            lista.Add(customerMail);
            lista.Add(customerPhone);
          
            object[] arr = lista.ToArray();


            int rowAffected = ef.Database.ExecuteSqlCommand("insert into Customers (customerName,customerMail,customerPhone) values (@p0,@p1,@p2)", arr);
            if (rowAffected > 0)
            {
                Debug.WriteLine("executed");
                return Redirect("customerview");
            }
            return Redirect("customerview");

        }

        public ActionResult Delete(int ?id)
        {
            PETCAREEntities db = new PETCAREEntities();
            Debug.WriteLine(id);
            if (id != null)
            {

                db.Database.ExecuteSqlCommand("delete from customers where customerId = @id", new SqlParameter("id", id));
                return RedirectToAction("customerview", "Customer");
            }

            return View();
        }


        public ActionResult GetCustomerToEdit(int ?id)
        {
            PETCAREEntities ef = new PETCAREEntities();
            customers custom = null;
            Debug.WriteLine(id);
           


            if (id != null)
            {
                custom = ef.customers.SqlQuery("select * from Customers where customerId = @id", new SqlParameter("id", id)).FirstOrDefault();
            }

            if (custom != null)
            {
                Debug.WriteLine(custom.customerName);

            }


            return View(custom);
        }

        public ActionResult EditCustomer(int ?cus, String customerName, String customerMail, String CustomerPhone)
        {
            PETCAREEntities ef = new PETCAREEntities();
            List<object> lista = new List<object>();
            lista.Add(customerName);
            lista.Add(customerMail);
            lista.Add(CustomerPhone);
           


            Debug.WriteLine(cus);
            lista.Add(cus);
            Debug.WriteLine("ID consumido " + cus);
            object[] arr = lista.ToArray();


            int rowAffected = ef.Database.ExecuteSqlCommand("update Customers set customerName=@p0, customerMail=@p1, customerPhone=@p2 where customerId = @p3", arr);
            if (rowAffected > 0)
            {
                Debug.WriteLine("executed");
                return Redirect("customerview");
            }
            return Redirect("customerview");
        }

        public List<pets> GetPetByCustomer(int customerId)
        {
            List<pets> pets = new List<pets>();
            PETCAREEntities ef = new PETCAREEntities();
            object[] arr = pets.ToArray();
         //   pets = ef.pets.SqlQuery("select petName from pets join customers on customers.customerId = pets.customerOwnerId where customers.customerId=@p0",arr);


            return pets;
        }
    }
}