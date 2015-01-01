using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using returns_web.Models;

namespace returns_web.Controllers
{
    public class RetsalesController : Controller
    {
        private retContext db = new retContext();

        // GET: Retsales
        public ActionResult Index()
        {
            return View(db.Retsales.ToList());
        }

        // GET: Retsales/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Retsale retsale = db.Retsales.Find(id);
            if (retsale == null)
            {
                return HttpNotFound();
            }
            return View(retsale);
        }

        // GET: Retsales/Create

        public ActionResult Create()
        {
            return View();
        }

        // POST: Retsales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,officeid,returnsid,saleval,saletax,targetoffid,moddate")] Retsale retsale)
        {
            if (ModelState.IsValid)
            {
                retsale.Id = Guid.NewGuid();
                db.Retsales.Add(retsale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(retsale);
        }

        // GET: Retsales/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Retsale retsale = db.Retsales.Find(id);
            if (retsale == null)
            {
                return HttpNotFound();
            }
            return View(retsale);
        }

        // POST: Retsales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,officeid,returnsid,saleval,saletax,targetoffid,moddate")] Retsale retsale)
        {
            if (ModelState.IsValid)
            {
                db.Entry(retsale).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(retsale);
        }

        // GET: Retsales/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Retsale retsale = db.Retsales.Find(id);
            if (retsale == null)
            {
                return HttpNotFound();
            }
            return View(retsale);
        }

        // POST: Retsales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Retsale retsale = db.Retsales.Find(id);
            db.Retsales.Remove(retsale);
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
