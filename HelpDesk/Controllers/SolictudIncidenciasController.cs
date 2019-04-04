using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HelpDesk.Models;

namespace HelpDesk.Controllers
{
    public class SolictudIncidenciasController : Controller
    {
        private AppHelpDeskEntities db = new AppHelpDeskEntities();

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
            ViewBag.PersonaID_Solicitud = new SelectList(db.Personas, "PersonaID", "Codigo");
            ViewBag.PersonaID_Tecnico = new SelectList(db.Personas, "PersonaID", "Codigo");
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
            if (ModelState.IsValid)
            {
                db.SolictudIncidencias.Add(solictudIncidencia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartamentoID_Solicitud = new SelectList(db.Departamentos, "DepartamentoID", "Descripcion", solictudIncidencia.DepartamentoID_Solicitud);
            ViewBag.EstatusID = new SelectList(db.Estatus, "EstatusID", "Descripcion", solictudIncidencia.EstatusID);
            ViewBag.EstatusSolicitudID = new SelectList(db.EstatusSolicituds, "EstatusSolicitudID", "Descripcion", solictudIncidencia.EstatusSolicitudID);
            ViewBag.PersonaID_Solicitud = new SelectList(db.Personas, "PersonaID", "Codigo", solictudIncidencia.PersonaID_Solicitud);
            ViewBag.PersonaID_Tecnico = new SelectList(db.Personas, "PersonaID", "Codigo", solictudIncidencia.PersonaID_Tecnico);
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
            if (solictudIncidencia == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartamentoID_Solicitud = new SelectList(db.Departamentos, "DepartamentoID", "Descripcion", solictudIncidencia.DepartamentoID_Solicitud);
            ViewBag.EstatusID = new SelectList(db.Estatus, "EstatusID", "Descripcion", solictudIncidencia.EstatusID);
            ViewBag.EstatusSolicitudID = new SelectList(db.EstatusSolicituds, "EstatusSolicitudID", "Descripcion", solictudIncidencia.EstatusSolicitudID);
            ViewBag.PersonaID_Solicitud = new SelectList(db.Personas, "PersonaID", "Codigo", solictudIncidencia.PersonaID_Solicitud);
            ViewBag.PersonaID_Tecnico = new SelectList(db.Personas, "PersonaID", "Codigo", solictudIncidencia.PersonaID_Tecnico);
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
            if (ModelState.IsValid)
            {
                db.Entry(solictudIncidencia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartamentoID_Solicitud = new SelectList(db.Departamentos, "DepartamentoID", "Descripcion", solictudIncidencia.DepartamentoID_Solicitud);
            ViewBag.EstatusID = new SelectList(db.Estatus, "EstatusID", "Descripcion", solictudIncidencia.EstatusID);
            ViewBag.EstatusSolicitudID = new SelectList(db.EstatusSolicituds, "EstatusSolicitudID", "Descripcion", solictudIncidencia.EstatusSolicitudID);
            ViewBag.PersonaID_Solicitud = new SelectList(db.Personas, "PersonaID", "Codigo", solictudIncidencia.PersonaID_Solicitud);
            ViewBag.PersonaID_Tecnico = new SelectList(db.Personas, "PersonaID", "Codigo", solictudIncidencia.PersonaID_Tecnico);
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
    }
}
