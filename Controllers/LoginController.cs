using PetCareBd1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;

namespace PetCareBd1.Controllers
{
    public class LoginController : Controller
    {
      

            public ActionResult Index ()
        {
            PETCAREEntities x = new PETCAREEntities();
            var utypeList = x.userstype.SqlQuery("select * from Userstype").ToList<userstype>();
         

            return View(utypeList);
        }
          
        [HttpPost]
        public ActionResult TryLogin(String UserName, String Password, String utype)
        {
            
            PETCAREEntities x = new PETCAREEntities();
            var UserList = x.users.SqlQuery("select * from Users").ToList<users>();
            foreach (var e in UserList)
            {

                if (e.userName == UserName && e.userPassword == Password && e.userstype.userTypeName == utype)
                {
                    // Debug.WriteLine(e.userName);
                    return Redirect("welcomepage");
                }else
                {
               
                   // return Redirect("Index");
                }
            }
            return View(UserList);
        }
        public ActionResult WelcomePage ()
        {
            return View();

        }

        public ActionResult ErrorPage ()
        {
            return View();

        }
    }
}