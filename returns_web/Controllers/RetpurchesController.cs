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
    public class RetpurchesController : Controller
    {
        private retContext db = new retContext();

        // GET: Retpurches
        public ActionResult Index()
        {
            return View(db.Retpurches.ToList());
        }

        // GET: Retpurches/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Retpurch retpurch = db.Retpurches.Find(id);
            if (retpurch == null)
            {
                return HttpNotFound();
            }
            return View(retpurch);
        }

        // GET: Retpurches/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Retpurches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,officeid,returnsid,purchval,purchtax,targetoffid,moddate")] Retpurch retpurch)
        {
            if (ModelState.IsValid)
            {
                retpurch.Id = Guid.NewGuid();
                db.Retpurches.Add(retpurch);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(retpurch);
        }

        // GET: Retpurches/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Retpurch retpurch = db.Retpurches.Find(id);
            if (retpurch == null)
            {
                return HttpNotFound();
            }
            return View(retpurch);
        }

        // POST: Retpurches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,officeid,returnsid,purchval,purchtax,targetoffid,moddate")] Retpurch retpurch)
        {
            if (ModelState.IsValid)
            {
                db.Entry(retpurch).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(retpurch);
        }

        // GET: Retpurches/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Retpurch retpurch = db.Retpurches.Find(id);
            if (retpurch == null)
            {
                return HttpNotFound();
            }
            return View(retpurch);
        }

        // POST: Retpurches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Retpurch retpurch = db.Retpurches.Find(id);
            db.Retpurches.Remove(retpurch);
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
