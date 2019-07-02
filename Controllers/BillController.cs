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
    public class BillController : Controller
    {
        // GET: Bill
        public ActionResult billview()
        {
            PETCAREEntities db = new PETCAREEntities();
            List<t_services> serviceList = db.t_services.SqlQuery("select * from t_services").ToList<t_services>();
            List<employees> emps = db.employees.SqlQuery("select * from employees").ToList<employees>();
            List<customers> cust = db.customers.SqlQuery("select * from customers").ToList<customers>();
            List<bill> bill_list = db.bill.SqlQuery("select * from bill").ToList<bill>();
            List<Object> tripleViewModel = new List<Object>();
            tripleViewModel.Add(serviceList);
            tripleViewModel.Add(emps);
            tripleViewModel.Add(cust);
            tripleViewModel.Add(bill_list);

            return View(tripleViewModel);
        }

        [HttpPost]
        public ActionResult AddBill(  int customerId, int serviceId, int employeeId, Decimal total)

        {

            PETCAREEntities db = new PETCAREEntities();
            List<object> lista = new List<object>();
          DateTime  billDate = DateTime.Now;
            int userId = 1;
            lista.Add(billDate);

            lista.Add(userId);
            lista.Add(customerId);
            lista.Add(serviceId);

            lista.Add(employeeId);
            lista.Add(total);
            object[] arr = lista.ToArray();
            
            int rowsAf = db.Database.ExecuteSqlCommand("insert into bill (billDate,userId,customerId,serviceId,employeeId,total)values (@p0,@p1,@p2,@p3,@p4,@p5)", arr);
            if (rowsAf > 0)
            {
                Debug.WriteLine("Agregado");
                return Redirect("billview");
            }


            return Redirect("billview");
        

        }
    }
}