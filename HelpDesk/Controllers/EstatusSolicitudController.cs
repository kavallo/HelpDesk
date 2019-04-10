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
    public class EstatusSolicitudController : Controller
    {
        private AppHelpDeskEntities db = new AppHelpDeskEntities();

        // GET: EstatusSolicitud
        public ActionResult Index()
        {
            return View(db.EstatusSolicituds.ToList());
        }

        // GET: EstatusSolicitud/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstatusSolicitud estatusSolicitud = db.EstatusSolicituds.Find(id);
            if (estatusSolicitud == null)
            {
                return HttpNotFound();
            }
            return View(estatusSolicitud);
        }

        // GET: EstatusSolicitud/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstatusSolicitud/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EstatusSolicitudID,Descripcion")] EstatusSolicitud estatusSolicitud)
        {
            if (ModelState.IsValid)
            {
                db.EstatusSolicituds.Add(estatusSolicitud);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(estatusSolicitud);
        }

        // GET: EstatusSolicitud/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstatusSolicitud estatusSolicitud = db.EstatusSolicituds.Find(id);
            if (estatusSolicitud == null)
            {
                return HttpNotFound();
            }
            return View(estatusSolicitud);
        }

        // POST: EstatusSolicitud/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EstatusSolicitudID,Descripcion")] EstatusSolicitud estatusSolicitud)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estatusSolicitud).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estatusSolicitud);
        }

        // GET: EstatusSolicitud/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstatusSolicitud estatusSolicitud = db.EstatusSolicituds.Find(id);
            if (estatusSolicitud == null)
            {
                return HttpNotFound();
            }
            return View(estatusSolicitud);
        }

        // POST: EstatusSolicitud/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EstatusSolicitud estatusSolicitud = db.EstatusSolicituds.Find(id);
            db.EstatusSolicituds.Remove(estatusSolicitud);
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
