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
    public class TipoIncidenciaController : Controller
    {
        private AppHelpDeskEntities db = new AppHelpDeskEntities();

        // GET: TipoIncidencia
        public ActionResult Index()
        {
            var tipoIncidencias = db.TipoIncidencias.Include(t => t.Estatu);
            return View(tipoIncidencias.ToList());
        }

        // GET: TipoIncidencia/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoIncidencia tipoIncidencia = db.TipoIncidencias.Find(id);
            if (tipoIncidencia == null)
            {
                return HttpNotFound();
            }
            return View(tipoIncidencia);
        }

        // GET: TipoIncidencia/Create
        public ActionResult Create()
        {
            ViewBag.EstatusID = new SelectList(db.Estatus, "EstatusID", "Descripcion");
            return View();
        }

        // POST: TipoIncidencia/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TipoIncidenciaID,Descripcion,EstatusID")] TipoIncidencia tipoIncidencia)
        {
            if (ModelState.IsValid)
            {
                db.TipoIncidencias.Add(tipoIncidencia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EstatusID = new SelectList(db.Estatus, "EstatusID", "Descripcion", tipoIncidencia.EstatusID);
            return View(tipoIncidencia);
        }

        // GET: TipoIncidencia/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoIncidencia tipoIncidencia = db.TipoIncidencias.Find(id);
            if (tipoIncidencia == null)
            {
                return HttpNotFound();
            }
            ViewBag.EstatusID = new SelectList(db.Estatus, "EstatusID", "Descripcion", tipoIncidencia.EstatusID);
            return View(tipoIncidencia);
        }

        // POST: TipoIncidencia/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TipoIncidenciaID,Descripcion,EstatusID")] TipoIncidencia tipoIncidencia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoIncidencia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EstatusID = new SelectList(db.Estatus, "EstatusID", "Descripcion", tipoIncidencia.EstatusID);
            return View(tipoIncidencia);
        }

        // GET: TipoIncidencia/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoIncidencia tipoIncidencia = db.TipoIncidencias.Find(id);
            if (tipoIncidencia == null)
            {
                return HttpNotFound();
            }
            return View(tipoIncidencia);
        }

        // POST: TipoIncidencia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoIncidencia tipoIncidencia = db.TipoIncidencias.Find(id);
            db.TipoIncidencias.Remove(tipoIncidencia);
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
