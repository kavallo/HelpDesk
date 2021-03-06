﻿using System;
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
    public class CierreSolicitudController : Controller
    {
        private AppHelpDeskEntities db = new AppHelpDeskEntities();

        // GET: CierreSolicitud
        public ActionResult Index()
        {
            var cierreSolicituds = db.CierreSolicituds.Include(c => c.SolictudIncidencia);
            return View(cierreSolicituds.ToList());
        }

        // GET: CierreSolicitud/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CierreSolicitud cierreSolicitud = db.CierreSolicituds.Find(id);
            if (cierreSolicitud == null)
            {
                return HttpNotFound();
            }
            return View(cierreSolicitud);
        }

        // GET: CierreSolicitud/Create
        public ActionResult Create()
        {


            ViewBag.SolicitudIncidenciaID = new SelectList(db.vIncidencias.Where(c => c.EstatusSolicitudID != 5), "SolicitudIncidenciaID", "Descripcion");
            return View();
        }

        public JsonResult LLamar(int ID)
        {

            //ViewBag.SolicitudIncidenciaID = new SelectList(db.vIncidencias, "SolicitudIncidenciaID", "Descripcion");

            //return Json(db.SolictudIncidencias.ToList().Select(t => t.SolicitudIncidenciaID = ID));

            var _incidencia = db.vIncidencias_Detalle.Where(t => t.SolicitudIncidenciaID == ID);

            //ViewBag.pp = "asdasdf";

            //return Json(db.vIncidencias.Where(t => t.SolicitudIncidenciaID == ID), "SolicitudIncidenciaID" + "Descripcion");
            return Json(_incidencia.ToList());
        }


        // POST: CierreSolicitud/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,SolicitudIncidenciaID,Cerrada,Comentarios,FechaCierre")] CierreSolicitud cierreSolicitud)
        {
            var completa = db.CierreSolicituds.Where(x => x.SolicitudIncidenciaID== cierreSolicitud.SolicitudIncidenciaID && x.Cerrada == cierreSolicitud.Cerrada).FirstOrDefault();


            if (ModelState.IsValid)
            {

                db.CierreSolicituds.Add(cierreSolicitud);
                db.SaveChanges();
                var oo = db.SpUpcerrar1(cierreSolicitud.SolicitudIncidenciaID);


                return RedirectToAction("Index");
            }

            ViewBag.SolicitudIncidenciaID = new SelectList(db.SolictudIncidencias, "SolicitudIncidenciaID", "ComentariosSolicitud", cierreSolicitud.SolicitudIncidenciaID);
            return View(cierreSolicitud);


        }

        // GET: CierreSolicitud/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CierreSolicitud cierreSolicitud = db.CierreSolicituds.Find(id);
            if (cierreSolicitud == null)
            {
                return HttpNotFound();
            }
            ViewBag.SolicitudIncidenciaID = new SelectList(db.vIncidencias.Where(c => c.SolicitudIncidenciaID == cierreSolicitud.SolicitudIncidenciaID  ), "SolicitudIncidenciaID", "Descripcion");
            return View(cierreSolicitud);
        }

        // POST: CierreSolicitud/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SolicitudIncidenciaID,Cerrada,Comentarios,FechaCierre")] CierreSolicitud cierreSolicitud)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cierreSolicitud).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SolicitudIncidenciaID = new SelectList(db.SolictudIncidencias, "SolicitudIncidenciaID", "ComentariosSolicitud", cierreSolicitud.SolicitudIncidenciaID);
            return View(cierreSolicitud);
        }

        // GET: CierreSolicitud/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CierreSolicitud cierreSolicitud = db.CierreSolicituds.Find(id);
            if (cierreSolicitud == null)
            {
                return HttpNotFound();
            }
            return View(cierreSolicitud);
        }

        // POST: CierreSolicitud/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CierreSolicitud cierreSolicitud = db.CierreSolicituds.Find(id);
            db.CierreSolicituds.Remove(cierreSolicitud);
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

        private JsonResult Json(Func<int, JsonResult> lLamar)
        {
            throw new NotImplementedException();
        }
    }


}

