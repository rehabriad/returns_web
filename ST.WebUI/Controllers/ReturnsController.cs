using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ST.Entity;
using ST.WebUI.DataContext;
using System.Data.Entity.Infrastructure;

namespace ST.WebUI.Controllers
{
    public class ReturnsController : Controller
    {
        private STDbContext db = new STDbContext();

        // GET: Returns
        public async Task<ActionResult> Index()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var rin = identity.FindFirst(ClaimTypes.Sid).Value;
            return View(await db.Returns.Where(r=>r.rin==rin).ToListAsync());
        }

        // GET: Returns/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Returns returns = await db.Returns.FindAsync(id);
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
            //adding rin to the model
            var identity = (ClaimsIdentity)User.Identity;
            var rin = identity.FindFirst(ClaimTypes.Sid).Value;
            model.rin = rin;
            return View(model);
        }

        // POST: Returns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,officeid,rin,returncode,taxyrmo,transdate,saleltc,purctdt2,nettaxpy,targetoffid,docLocNumber,moddate,status,retsale,retpurch")] Returns returns)
        {

            if (ModelState.IsValid)
            {
                var result = db.Returns.FirstOrDefaultAsync(w => w.rin == returns.rin && w.taxyrmo == returns.taxyrmo);
                if (result.Result!= null)
                {
                    ModelState.AddModelError("", ST.Resource.ReturnsResource.rinError);
                }
                else
                {
                    returns.Id = Guid.NewGuid();
                    returns.moddate = DateTime.Now;

                    db.Returns.Add(returns);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }

            return View(returns);
        }

        // GET: Returns/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Returns returns = await db.Returns.FindAsync(id);
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
        public async Task<ActionResult> Edit([Bind(Include = "Id,officeid,rin,returncode,taxyrmo,transdate,saleltc,purctdt2,nettaxpy,targetoffid,moddate,status,docLocNumber,retsale,retpurch")] Returns returns)
        {
            if (ModelState.IsValid)
            {
                //var existing = await db.Returns.Include(r=>r.retsale).Include(r=>r.retpurch).FirstOrDefaultAsync(r=>r.Id==returns.Id);
                var existing = await db.Returns.FindAsync( returns.Id);
                if (existing == null)
                {
                    return RedirectToAction("Index");
                }
                //var oldrets = existing.retsale;
                //var oldPurch = existing.retpurch;

                ((IObjectContextAdapter)db).ObjectContext.Detach(existing);
                
                foreach (var retsale in returns.retsale)
                {
                    if (returns.Id != retsale.returnsid)
                    {
                        retsale.returnsid = returns.Id;
                        db.Entry(retsale).State = EntityState.Added;
                    }
                    else
                    {
                        //((IObjectContextAdapter) db).ObjectContext.Detach(retsale);
                        db.Entry(retsale).State = EntityState.Modified;
                    }

                }

                //foreach (var deleted in oldrets.Where(oldRow => !returns.retsale.Select(r => r.Id).Contains(oldRow.Id)))
                //{
                    
                //    //db.Retsales.Remove(deleted);
                //    db.Entry(deleted).State = EntityState.Deleted;
                //}

                foreach (var retpurch in returns.retpurch)
                {
                    if (returns.Id != retpurch.returnsid)
                    {
                        retpurch.returnsid = returns.Id;
                        db.Entry(retpurch).State = EntityState.Added;
                    }
                    else
                    {
                        //((IObjectContextAdapter) db).ObjectContext.Detach(retpurch);
                        db.Entry(retpurch).State = EntityState.Modified;
                    }

                }
                //foreach (var deleted in oldPurch.Where(oldRow => !returns.retpurch.Select(r => r.Id).Contains(oldRow.Id)))
                //{
                //    //db.Retpurches.Remove(deleted);
                //    db.Entry(deleted).State = EntityState.Deleted;
                //}
                db.Entry(returns).State = EntityState.Modified;
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(returns);
        }

        // GET: Returns/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Returns returns = await db.Returns.FindAsync(id);
            if (returns == null)
            {
                return HttpNotFound();
            }
            return View(returns);
        }

        // POST: Returns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Returns returns = await db.Returns.FindAsync(id);
            db.Returns.Remove(returns);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public ActionResult AddRetSale()
        {
            var model = new Retsale();
            return View("_RetSaleSingle", model);
        }
        public ActionResult AddPurchSale()
        {
            var model = new Retpurch();
            return View("_RetPurchSingle", model);
        }

        [HttpPost]
        public bool CheckExistingTaxyrmo(DateTime taxyrmo)
        {
            var existing = false;
            var identity = (ClaimsIdentity)User.Identity;
            var rin = identity.FindFirst(ClaimTypes.Sid).Value;
            
            var result = db.Returns.FirstOrDefaultAsync(w => w.rin == rin && w.taxyrmo == taxyrmo);
            if (result.Result != null)
                existing = true;
            return existing;
        }

        public ActionResult Send(Guid id)
        {
            Returns returns = db.Returns.Find(id);
            Random random = new Random();
            returns.docLocNumber = string.Format("rt{0}/{1}", DateTime.Now.ToString("ddMMyyyy"), random.Next(1000, 9999));
            returns.status = ReturnStatus.Colsed;
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
    }
}
