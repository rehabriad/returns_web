using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using returns_web.Models;

namespace returns_web.Controllers
{
    public class ReturnsController : Controller
    {
        private retContext db = new retContext();

        // GET: Returns
        public ActionResult Index()
        {
            return View(db.Returns.ToList());
        }

        // GET: Returns/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Returns returns = db.Returns.Find(id);
            if (returns == null)
            {
                return HttpNotFound();
            }
            return View(returns);
        }

        // GET: Returns/Create
        public ActionResult Create()
        {
            var model = new Returns();
            return View(model);
        }

        // POST: Returns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,officeid,rin,returncode,taxyrmo,transdate,saleltc,purctdt2,nettaxpy,targetoffid,doclocnum,moddate,retsale,retpurch")] Returns returns)
        {
            if (ModelState.IsValid)
            {
                returns.Id = Guid.NewGuid();
                returns.moddate = DateTime.Now;
                returns.returncode = 1;
                
                //returns.retsale.a(retsale);
                db.Returns.Add(returns);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(returns);
        }

        // GET: Returns/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Returns returns = db.Returns.Find(id);
            if (returns == null)
            {
                return HttpNotFound();
            }
            return View(returns);
        }

        // POST: Returns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,officeid,rin,returncode,taxyrmo,transdate,saleltc,purctdt2,nettaxpy,targetoffid,doclocnum,moddate,retsale,retpurch")] Returns returns)
        {
            if (ModelState.IsValid)
            {
                Returns existing = db.Returns.Find(returns.Id);
                if (existing == null)
                    return RedirectToAction("Index");
                ((IObjectContextAdapter)db).ObjectContext.Detach(existing);
                //db.Returns.AddOrUpdate(returns);
                
                foreach (var retsale in returns.retsale)
                {
                    if (returns.Id != retsale.returnsid)
                    {
                        retsale.returnsid = returns.Id;
                        db.Entry(retsale).State = EntityState.Added;
                    }
                    else
                        db.Entry(retsale).State = EntityState.Modified;

                }
                foreach (var retpurch in returns.retpurch)
                {
                    if (returns.Id != retpurch.returnsid)
                    {
                        retpurch.returnsid = returns.Id;
                        db.Entry(retpurch).State = EntityState.Added;
                    }
                    else
                        db.Entry(retpurch).State = EntityState.Modified;

                }
                db.Entry(returns).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(returns);
        }

        // GET: Returns/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Returns returns = db.Returns.Find(id);
            if (returns == null)
            {
                return HttpNotFound();
            }
            return View(returns);
        }

        // POST: Returns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Returns returns = db.Returns.Find(id);
            db.Returns.Remove(returns);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Send(Guid id)
        {
            Returns returns = db.Returns.Find(id);
            Random random = new Random();
            returns.docLocNumber = string.Format("rt{0}/{1}", DateTime.Now.ToString("ddMMyyyy"), random.Next(1000, 9999));
            returns.status=ReturnStatus.Colsed;
            db.Entry(returns).State = EntityState.Modified;
            db.SaveChanges();
            ViewBag.IsPrint = true;
            return View("Details", returns);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult AddRetSale(int rowCount=0)
        {
            var model = new Retsale();
            ViewBag.RowCount = rowCount;
            return View("_RetSaleSingle", model);
        }
        public ActionResult AddPurchSale(int prowCount = 0)
        {
            var model = new Retpurch();
            ViewBag.RowCount = prowCount;
            return View("_RetPurchSingle", model);
        }
    }
}
