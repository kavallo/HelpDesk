using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Web;
using System.Data;
using System.Web.Mvc;
using HelpDesk.Models;

namespace HelpDesk.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public AppHelpDeskEntities db = new AppHelpDeskEntities();

       
        public ActionResult Index()
        {
            Session["Autenticado"] = false;

           
            return View();
        }

        [HttpPost]
       
        public ActionResult Autherize(HelpDesk.Models.Usuario LoginModel)
        {
            using (AppHelpDeskEntities db = new AppHelpDeskEntities())
            {
               

                var userDetails = db.Usuarios.Where(x => x.Usuario1 == LoginModel.Usuario1 && x.Clave == LoginModel.Clave).FirstOrDefault();

                if (userDetails == null)
                {
                    return View("Index", LoginModel);
                }
                else
                { 
                    Session["Autenticado"] = true;

                    Session["Codigo"] = userDetails.Usuario1;

                    var oo = db.spultimoinicio(LoginModel.Usuario1);

                    return RedirectToAction("Index", "Home");


                }




            }

           
        }
      
        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        
       
    }
}