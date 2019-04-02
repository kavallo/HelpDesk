using HelpDesk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using System.Data.Entity;

using System.Net;





namespace HelpDesk.Controllers
{
    public class InicioSesion : Controller
    {
        // GET: InicioSesion

         
        public ActionResult Index()
        {
            return View();
        }
    }
}