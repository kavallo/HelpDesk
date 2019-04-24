using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.IO;
using System.Threading;
using System.Globalization;
using System.Web.Mvc;
using HelpDesk.Models;
using System.Net.Mail;
using System.Text;

namespace HelpDesk.Controllers
{
    public class SolictudIncidenciasController : Controller
    {



        private AppHelpDeskEntities db = new AppHelpDeskEntities();


        public object TipoIncidencias { get; private set; }






        // GET: SolictudIncidencias
        public ActionResult Index()
        {
            var solictudIncidencias = db.SolictudIncidencias.Include(s => s.Departamento).Include(s => s.Estatu).Include(s => s.EstatusSolicitud).Include(s => s.Persona).Include(s => s.Persona1).Include(s => s.TipoIncidencia);


            return View(solictudIncidencias.ToList());
        }

        // GET: SolictudIncidencias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolictudIncidencia solictudIncidencia = db.SolictudIncidencias.Find(id);
            if (solictudIncidencia == null)
            {
                return HttpNotFound();
            }
            return View(solictudIncidencia);
        }

        // GET: SolictudIncidencias/Create
        public ActionResult Create()
        {
            ViewBag.DepartamentoID_Solicitud = new SelectList(db.Departamentos, "DepartamentoID", "Descripcion");
            ViewBag.EstatusID = new SelectList(db.Estatus, "EstatusID", "Descripcion");
            ViewBag.EstatusSolicitudID = new SelectList(db.EstatusSolicituds, "EstatusSolicitudID", "Descripcion");
            ViewBag.PersonaID_Solicitud = new SelectList(db.vPersonas, "PersonaID", "Nombrecompleto");
            ViewBag.PersonaID_Tecnico = new SelectList(db.vPersonaTecnicoes, "PersonaID", "Nombrecompleto");
            ViewBag.TipoIncidenciaID = new SelectList(db.TipoIncidencias, "TipoIncidenciaID", "Descripcion");
            return View();
        }

        // POST: SolictudIncidencias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Include = "SolicitudIncidenciaID,FechaIncidencia,DepartamentoID_Solicitud,PersonaID_Solicitud,TipoIncidenciaID,PersonaID_Tecnico,ComentariosSolicitud,EstatusSolicitudID,EstatusID")] SolictudIncidencia solictudIncidencia)
        {

            /////// consulta a la tablas para obtener el valor de Tipoincidencia y llenar el correo 
            solictudIncidencia.TipoIncidenciaID.ToString();
            TipoIncidencia tipoIncidencia = db.TipoIncidencias.ToList()
                                                //consulta del primer valor obtenido la bariable t almacena el que se registra pra comparar enla otra tabla 
                                                .Where(t => t.TipoIncidenciaID == solictudIncidencia.TipoIncidenciaID).FirstOrDefault();

            ////almacenar el valor de tipo incidencia correo-electronico en variables
            var correo = tipoIncidencia.CorreoElectronico;
            //// para agregar el tipo del tiket
            var incide = tipoIncidencia.Descripcion;
            var come = solictudIncidencia.ComentariosSolicitud;

            ////////

            if (ModelState.IsValid)
            {
                db.SolictudIncidencias.Add(solictudIncidencia);
                db.SaveChanges();

                SendEmail(correo, incide, "<p>hola tienes un tiket " + incide + "<br /><br /> resumen de la solicitud  <br /><br />" + come + " <br /><br /> Regars </p>");

                return RedirectToAction("Index");
            }

            ViewBag.DepartamentoID_Solicitud = new SelectList(db.Departamentos, "DepartamentoID", "Descripcion", solictudIncidencia.DepartamentoID_Solicitud);
            ViewBag.EstatusID = new SelectList(db.Estatus, "EstatusID", "Descripcion", solictudIncidencia.EstatusID);
            ViewBag.EstatusSolicitudID = new SelectList(db.EstatusSolicituds, "EstatusSolicitudID", "Descripcion", solictudIncidencia.EstatusSolicitudID);
            ViewBag.PersonaID_Solicitud = new SelectList(db.vPersonas, "PersonaID", "Nombrecompleto", solictudIncidencia.PersonaID_Solicitud);
            ViewBag.PersonaID_Tecnico = new SelectList(db.vPersonaTecnicoes, "PersonaID", "Nombrecompleto", solictudIncidencia.PersonaID_Tecnico);
            ViewBag.TipoIncidenciaID = new SelectList(db.TipoIncidencias, "TipoIncidenciaID", "Descripcion", solictudIncidencia.TipoIncidenciaID);
            return View(solictudIncidencia);
        }

        // GET: SolictudIncidencias/Edit/5
        public ActionResult Edit(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            SolictudIncidencia solictudIncidencia = db.SolictudIncidencias.Find(id);

            solictudIncidencia.EstatusSolicitudID.ToString();
            EstatusSolicitud EstatusSolicitud = db.EstatusSolicituds.ToList()
                                                    .Where(e => e.EstatusSolicitudID == solictudIncidencia.EstatusSolicitudID).FirstOrDefault();
            var completa = EstatusSolicitud.EstatusSolicitudID;
            if (completa == 5)
            {
                ViewBag.EstatusSolicitudID = new SelectList(db.EstatusSolicituds.Where(x => x.EstatusSolicitudID == 5), "EstatusSolicitudID", "Descripcion", solictudIncidencia.EstatusSolicitudID);
            }
            else
            {
                ViewBag.EstatusSolicitudID = new SelectList(db.EstatusSolicituds, "EstatusSolicitudID", "Descripcion", solictudIncidencia.EstatusSolicitudID);
            }
            if (solictudIncidencia == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartamentoID_Solicitud = new SelectList(db.Departamentos, "DepartamentoID", "Descripcion", solictudIncidencia.DepartamentoID_Solicitud);
            ViewBag.EstatusID = new SelectList(db.Estatus, "EstatusID", "Descripcion", solictudIncidencia.EstatusID);
            ViewBag.PersonaID_Solicitud = new SelectList(db.vPersonas, "PersonaID", "Nombrecompleto", solictudIncidencia.PersonaID_Solicitud);
            ViewBag.PersonaID_Tecnico = new SelectList(db.vPersonaTecnicoes, "PersonaID", "Nombrecompleto", solictudIncidencia.PersonaID_Tecnico);
            ViewBag.TipoIncidenciaID = new SelectList(db.TipoIncidencias, "TipoIncidenciaID", "Descripcion", solictudIncidencia.TipoIncidenciaID);
            return View(solictudIncidencia);
        }

        // POST: SolictudIncidencias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SolicitudIncidenciaID,FechaIncidencia,DepartamentoID_Solicitud,PersonaID_Solicitud,TipoIncidenciaID,PersonaID_Tecnico,ComentariosSolicitud,EstatusSolicitudID,EstatusID")] SolictudIncidencia solictudIncidencia)
        {

            /////// consulta a la tablas para obtener el valor de Tipoincidencia y llenar el correo 
            solictudIncidencia.TipoIncidenciaID.ToString();
            TipoIncidencia tipoIncidencia = db.TipoIncidencias.ToList()
                                                //consulta del primer valor obtenido la bariable t almacena el que se registra pra comparar enla otra tabla 
                                                .Where(t => t.TipoIncidenciaID == solictudIncidencia.TipoIncidenciaID).FirstOrDefault();

            solictudIncidencia.EstatusSolicitudID.ToString();
            EstatusSolicitud EstatusSolicitud = db.EstatusSolicituds.ToList()
                                                    .Where(e => e.EstatusSolicitudID == solictudIncidencia.EstatusSolicitudID).FirstOrDefault();

            ////almacenar el valor de tipo incidencia correo-electronico en variables
            var correo = tipoIncidencia.CorreoElectronico;
            //// para agregar el tipo del tiket
            var incide = tipoIncidencia.Descripcion;
            var come = solictudIncidencia.ComentariosSolicitud;
            var estado = EstatusSolicitud.Descripcion;



            var completa = EstatusSolicitud.EstatusSolicitudID;
            if (completa == 5)
            {

                return RedirectToAction("Index");
            }

            else
            {

                if (ModelState.IsValid)
                {
                    db.Entry(solictudIncidencia).State = EntityState.Modified;
                    db.SaveChanges();

                    SendEmail(correo, incide, "<p>hola tu tiket se actualizo " + incide + "<br /><br /> resumen de la solicitud  <br /><br />" + come + " <br /><br /> Estatus de la Solicitud </p>" + estado);

                    return RedirectToAction("Index");
                }

            }
            ViewBag.DepartamentoID_Solicitud = new SelectList(db.Departamentos, "DepartamentoID", "Descripcion", solictudIncidencia.DepartamentoID_Solicitud);
            ViewBag.EstatusID = new SelectList(db.Estatus, "EstatusID", "Descripcion", solictudIncidencia.EstatusID);
            ViewBag.EstatusSolicitudID = new SelectList(db.EstatusSolicituds, "EstatusSolicitudID", "Descripcion", solictudIncidencia.EstatusSolicitudID);
            ViewBag.PersonaID_Solicitud = new SelectList(db.vPersonas, "PersonaID", "Nombrecompleto", solictudIncidencia.PersonaID_Solicitud);
            ViewBag.PersonaID_Tecnico = new SelectList(db.vPersonaTecnicoes, "PersonaID", "Nombrecompleto", solictudIncidencia.PersonaID_Tecnico);
            ViewBag.TipoIncidenciaID = new SelectList(db.TipoIncidencias, "TipoIncidenciaID", "Descripcion", solictudIncidencia.TipoIncidenciaID);
            return View(solictudIncidencia);
        }

        // GET: SolictudIncidencias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolictudIncidencia solictudIncidencia = db.SolictudIncidencias.Find(id);
            if (solictudIncidencia == null)
            {
                return HttpNotFound();
            }
            return View(solictudIncidencia);
        }

        // POST: SolictudIncidencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SolictudIncidencia solictudIncidencia = db.SolictudIncidencias.Find(id);
            db.SolictudIncidencias.Remove(solictudIncidencia);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public JsonResult SendMailToUser()
        {
            bool result = false;

            result = SendEmail("gabriel.consultechdr@gmail.com", "hola", "     <p>hola mmmmmm <br /> es una prueba <br /> Regars </p>");
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        public bool SendEmail(string toEmail, string sudject, string emailBody)
        {
            try
            {
                string senderEmail = System.Configuration.ConfigurationManager.AppSettings["SenderEmail"].ToString();
                string senderPassword = System.Configuration.ConfigurationManager.AppSettings["SenderPassword"].ToString();

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Timeout = 100000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);

                MailMessage mailMessage = new MailMessage(senderEmail, toEmail, sudject, emailBody);

                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = UTF8Encoding.UTF8;

                client.Send(mailMessage);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
