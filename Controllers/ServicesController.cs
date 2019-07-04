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
    public class ServicesController : Controller
    {
        // GET: Services
        public ActionResult servicesview()
        {
            PETCAREEntities db = new PETCAREEntities();
            List<t_services> serviceList = db.t_services.SqlQuery("select * from t_services").ToList<t_services>();
            List<serviceType> types = db.serviceTypes.SqlQuery("select * from serviceType").ToList<serviceType>();
            List<Object> DoubleViewModel = new List<Object>();
            DoubleViewModel.Add(serviceList);
            DoubleViewModel.Add(types);


            return View(DoubleViewModel);
        }
        // Agregar nuevo Servicio!
        [HttpPost]
       public ActionResult AddServiceType(String serviceTypeName, String serviceTypeDescription)
        {

            PETCAREEntities d = new PETCAREEntities();
            List<Object> list = new List<Object>();
            list.Add(serviceTypeName);
            list.Add(serviceTypeDescription);
            object[] arr = list.ToArray();
          int n_row = d.Database.ExecuteSqlCommand("insert into serviceType (serviceTypeName,serviceTypeDescription) values (@p0,@p1)",arr);
            if (n_row > 0)
            {
                return RedirectToAction("servicesview","Services");
            }
            return RedirectToAction("servicesview","Services");
        }

        [HttpPost]
        public ActionResult AddService(String serviceName,String serviceDescription,Decimal serviceMonto, DateTime serviceDuration, int servtypes)
        {

            PETCAREEntities db = new PETCAREEntities();
            List<object> lista = new List<object>();
            lista.Add(serviceName);
            lista.Add(serviceDescription);
            lista.Add(serviceMonto);
            lista.Add(serviceDuration);
          
            lista.Add(servtypes);
            object[] arr = lista.ToArray();
            Debug.WriteLine(servtypes);
           int rowsAf = db.Database.ExecuteSqlCommand("insert into t_services (serviceName,serviceDescription,servicePrice,serviceDuration,serviceTypeId)values (@p0,@p1,@p2,@p3,@p4)",arr);
            if (rowsAf > 0)
            {
                Debug.WriteLine("Agregado");
                return Redirect("servicesview");
            }

           
            return Redirect("servicesview");
        }

        // Editar el servicio del sitema
        [HttpPost]
        public ActionResult EditService (int ?idserv, String serviceName, String serviceDescription, Decimal serviceMonto,DateTime serviceDuration,int servtypes)
        {
            PETCAREEntities ef = new PETCAREEntities();
            List<object> lista = new List<object>();
            lista.Add(serviceName);
            lista.Add(serviceDescription);
            lista.Add(serviceMonto);
            lista.Add(serviceDuration);
            lista.Add(servtypes);
          


            Debug.WriteLine(idserv);
            lista.Add(idserv);
            Debug.WriteLine("ID consumido " + idserv);
            object[] arr = lista.ToArray();


            int rowAffected = ef.Database.ExecuteSqlCommand("update t_services set serviceName=@p0, serviceDescription=@p1, servicePrice=@p2, serviceDuration=@p3,serviceTypeId=@p4 where serviceId = @p5", arr);
            if (rowAffected > 0)
            {
                Debug.WriteLine("executed");
                return Redirect("servicesview");
            }
            return Redirect("servicesview");
        }
        // obtiene los datos de la factura a editar
        public ActionResult GetServicesToEdit (int ?id)
        {
            PETCAREEntities ef = new PETCAREEntities();
            t_services serv = null;
           
            Debug.WriteLine(id);
            List<Object> AnotherDoubleModel = new List<Object>();
            List<serviceType> types = ef.serviceTypes.SqlQuery("select * from serviceType").ToList<serviceType>();
            List<t_services> thefirstIs = ef.t_services.SqlQuery("select * from t_services").ToList<t_services>();

            if (id != null)
            {
                serv = ef.t_services.SqlQuery("select * from t_services where serviceId = @id", new SqlParameter("id", id)).FirstOrDefault();
                thefirstIs.Insert(0,serv);

            }
           
            if (serv != null)
            {
                AnotherDoubleModel.Insert(0,thefirstIs);
                AnotherDoubleModel.Insert(1,types);
                Debug.WriteLine(serv.serviceName);

            }
         

            return View(AnotherDoubleModel);

            
        }
        // Elimina la factura deseada
        public ActionResult Delete(int ?id)
        {
            PETCAREEntities db = new PETCAREEntities();
            Debug.WriteLine(id);
            if (id != null)
            {

                db.Database.ExecuteSqlCommand("delete from t_services where serviceId = @id", new SqlParameter("id", id));
                return RedirectToAction("servicesview", "Services");
            }



            return View();
        }
    }
}