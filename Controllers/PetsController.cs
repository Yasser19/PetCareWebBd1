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
    public class PetsController : Controller
    {
        // GET: Pets
        public ActionResult petview()
        {
            PETCAREEntities db = new PETCAREEntities();
            List<pets> petList = db.pets.SqlQuery("select * from pets").ToList<pets>();
            List<customers> clientes = db.customers.SqlQuery("select * from customers").ToList<customers>();
            List<Object> DoubleViewModel = new List<Object>();
            DoubleViewModel.Add(petList);
            DoubleViewModel.Add(clientes);





            return View(DoubleViewModel);
           
        }

        [HttpPost]
        public ActionResult AddPet(String petName, String petType, String petColor , String petRace, int petOwner)
        {

            PETCAREEntities db = new PETCAREEntities();
            List<object> lista = new List<object>();
            lista.Add(petName);
            lista.Add(petType);
            lista.Add(petColor);
            lista.Add(petRace);

            lista.Add(petOwner);
            object[] arr = lista.ToArray();
            Debug.WriteLine(petOwner);
            int rowsAf = db.Database.ExecuteSqlCommand("insert into pets (petName,petType,petColor,petRace,customerOwnerId)values (@p0,@p1,@p2,@p3,@p4)", arr);
            if (rowsAf > 0)
            {
                Debug.WriteLine("Agregado");
                return Redirect("petview");
            }


            return Redirect("petview");
        }

        public ActionResult GetPetToEdit(int? id)
        {
            PETCAREEntities ef = new PETCAREEntities();
            pets pet = null;

            Debug.WriteLine(id);
            List<Object> AnotherDoubleModel = new List<Object>();
            List<customers> clientes = ef.customers.SqlQuery("select * from customers").ToList<customers>();
            List<pets> thefirstIs = ef.pets.SqlQuery("select * from pets").ToList<pets>();

            if (id != null)
            {
                pet = ef.pets.SqlQuery("select * from pets where petId = @id", new SqlParameter("id", id)).FirstOrDefault();
                thefirstIs.Insert(0, pet);

            }

            if (pet != null)
            {
                AnotherDoubleModel.Insert(0, thefirstIs);
                AnotherDoubleModel.Insert(1, clientes);
                Debug.WriteLine(pet.petName);

            }


            return View(AnotherDoubleModel);


        }

        [HttpPost]
        public ActionResult EditPet(int? petid, String petName, String petType, String petColor, String petRace, int petOwner)
        {
            PETCAREEntities ef = new PETCAREEntities();
            List<object> lista = new List<object>();
            lista.Add(petName);
            lista.Add(petType);
            lista.Add(petColor);
            lista.Add(petRace);
            lista.Add(petOwner);




            Debug.WriteLine(petOwner);
            lista.Add(petid);
            Debug.WriteLine("ID consumido " + petOwner);
            object[] arr = lista.ToArray();


            int rowAffected = ef.Database.ExecuteSqlCommand("update pets set petName=@p0, petType=@p1, petColor=@p2, petRace=@p3, customerOwnerId=@p4  where petId = @p5", arr);
            if (rowAffected > 0)
            {
                Debug.WriteLine("executed");
                return Redirect("petview");
            }
            return Redirect("petview");
        }

        public ActionResult Delete( int?id)
        {

            PETCAREEntities db = new PETCAREEntities();
            Debug.WriteLine(id);
            if (id != null)
            {

                db.Database.ExecuteSqlCommand("delete from pets where petId = @id", new SqlParameter("id", id));
                return RedirectToAction("petview", "Pets");
            }



            return View();
        }
    }
}