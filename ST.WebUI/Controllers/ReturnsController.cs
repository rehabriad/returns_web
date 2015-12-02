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
        public async Task<ActionResult> Create([Bind(Include = "Id,officeid,rin,returncode,taxyrmo,transdate,saleltc,purctdt2,nettaxpy,targetoffid,docLocNumber,moddate,status,retsale,retpurch,retcapital,retreceit,retlocalpurch,retexpurch,noretsale,noretpurch")] Returns returns)
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
        public async Task<ActionResult> Edit([Bind(Include = "Id,officeid,rin,returncode,taxyrmo,transdate,saleltc,purctdt2,nettaxpy,targetoffid,moddate,status,docLocNumber,retsale,retpurch,retcapital,retreceit,retlocalpurch,retexpurch,noretsale,noretpurch")] Returns returns)
        {
            if (ModelState.IsValid)
            {
                var existing1 = await db.Returns.FindAsync(returns.Id);
                //var existing = await db.Returns.Include(r=>r.retsale).Include(r=>r.retpurch).FirstOrDefaultAsync(r=>r.Id==returns.Id);
                var existing = await db.Returns.FindAsync( returns.Id);
                if (existing == null)
                {
                    return RedirectToAction("Index");
                }
                //select the deleted ids of retsale and retpurch

                //var salesids = existing.retsale.Where(s=>!returns.retsale.Select(r=>r.Id).Contains(s.Id)).Select(s => s.Id).ToList();
                //var purchIds = existing.retpurch.Where(s => !returns.retpurch.Select(r => r.Id).Contains(s.Id)).Select(s => s.Id).ToList();
                ((IObjectContextAdapter)db).ObjectContext.Detach(existing);
                 if ((int)returns.returncode == 2)
                {
                  
                    db.Retsales.RemoveRange(db.Retsales.Where(x => x.returnsid == returns.Id));
                    db.Retpurches.RemoveRange(db.Retpurches.Where(x => x.returnsid == returns.Id));
                    db.Retcapitals.RemoveRange(db.Retcapitals.Where(x => x.returnsid == returns.Id));
                    db.RetReceits.RemoveRange(db.RetReceits.Where(x => x.returnsid == returns.Id));
                    db.RetLocalPurches.RemoveRange(db.RetLocalPurches.Where(x => x.returnsid == returns.Id));
                    db.RetExPurches.RemoveRange(db.RetExPurches.Where(x => x.returnsid == returns.Id));

                    returns.saleltc = 0;
                    returns.purctdt2 = 0;
                    returns.nettaxpy = 0;
                    await db.SaveChangesAsync();

                   
                }

                 if ((int)returns.returncode == 1)
                 {
                     var saleIds = new List<Guid>();
                     foreach (var retsale in returns.retsale)
                     {
                         
                         if (returns.Id != retsale.returnsid)
                         {
                             retsale.returnsid = returns.Id;
                             db.Entry(retsale).State = EntityState.Added;
                         }
                         else
                         {
                             db.Entry(retsale).State = EntityState.Modified;
                             saleIds.Add(retsale.Id);
                         }

                     }

                     var deletedSales = db.Retsales.Where(s => s.returnsid == returns.Id && saleIds.All(r => r != s.Id)).ToList();
                     if (deletedSales.Any())
                         db.Retsales.RemoveRange(deletedSales);

                     var purchIds = new List<Guid>();
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
                             purchIds.Add(retpurch.Id);
                             
                         }

                     }
                     //Remove deleted
                     var deletedPurch = db.Retpurches.Where(s => s.returnsid == returns.Id && purchIds.All(r => r != s.Id)).ToList();
                     if (deletedPurch.Any())
                         db.Retpurches.RemoveRange(deletedPurch);


                     var capitalIds = new List<Guid>();
                     foreach (var retcapital in returns.retcapital)
                     {
                         if (returns.Id != retcapital.returnsid)
                         {
                             retcapital.returnsid = returns.Id;
                             db.Entry(retcapital).State = EntityState.Added;
                         }
                         else
                         {
                             //((IObjectContextAdapter) db).ObjectContext.Detach(retpurch);
                             db.Entry(retcapital).State = EntityState.Modified;
                             capitalIds.Add(retcapital.Id);

                         }

                     }
                     //Remove deleted
                     var deletedcapital = db.Retcapitals.Where(s => s.returnsid == returns.Id && capitalIds.All(r => r != s.Id)).ToList();
                     if (deletedcapital.Any())
                         db.Retcapitals.RemoveRange(deletedcapital);


                     var receitIds = new List<Guid>();
                     foreach (var retreceit in returns.retreceit)
                     {
                         if (returns.Id != retreceit.returnsid)
                         {
                             retreceit.returnsid = returns.Id;
                             db.Entry(retreceit).State = EntityState.Added;
                         }
                         else
                         {
                             //((IObjectContextAdapter) db).ObjectContext.Detach(retpurch);
                             db.Entry(retreceit).State = EntityState.Modified;
                             receitIds.Add(retreceit.Id);

                         }

                     }
                     //Remove deleted
                     var deletedreceit = db.RetReceits.Where(s => s.returnsid == returns.Id && receitIds.All(r => r != s.Id)).ToList();
                     if (deletedreceit.Any())
                         db.RetReceits.RemoveRange(deletedreceit);


                     var localpurchIds = new List<Guid>();
                     foreach (var retlocalpurch in returns.retlocalpurch)
                     {
                         if (returns.Id != retlocalpurch.returnsid)
                         {
                             retlocalpurch.returnsid = returns.Id;
                             db.Entry(retlocalpurch).State = EntityState.Added;
                         }
                         else
                         {
                             //((IObjectContextAdapter) db).ObjectContext.Detach(retpurch);
                             db.Entry(retlocalpurch).State = EntityState.Modified;
                             localpurchIds.Add(retlocalpurch.Id);

                         }

                     }
                     //Remove deleted
                     var deletedlocalpurch = db.RetLocalPurches.Where(s => s.returnsid == returns.Id && localpurchIds.All(r => r != s.Id)).ToList();
                     if (deletedlocalpurch.Any())
                         db.RetLocalPurches.RemoveRange(deletedlocalpurch);


                     var expurchIds = new List<Guid>();
                     foreach (var retexpurch in returns.retexpurch)
                     {
                         if (returns.Id != retexpurch.returnsid)
                         {
                             retexpurch.returnsid = returns.Id;
                             db.Entry(retexpurch).State = EntityState.Added;
                         }
                         else
                         {
                             //((IObjectContextAdapter) db).ObjectContext.Detach(retpurch);
                             db.Entry(retexpurch).State = EntityState.Modified;
                             expurchIds.Add(retexpurch.Id);

                         }

                     }
                     //Remove deleted
                     var deletedexpurch = db.RetExPurches.Where(s => s.returnsid == returns.Id && expurchIds.All(r => r != s.Id)).ToList();
                     if (deletedexpurch.Any())
                         db.RetExPurches.RemoveRange(deletedexpurch);

                 }
               
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
            db.Retsales.RemoveRange(db.Retsales.Where(x => x.returnsid == returns.Id));
            db.Retpurches.RemoveRange(db.Retpurches.Where(x => x.returnsid == returns.Id));

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

        public ActionResult AddRetCapital()
        {
            var model = new RetCapital();
            return View("_RetCapitalSingle", model);
        }

        public ActionResult AddRetReceit()
        {
            var model = new RetReceit();
            return View("_RetReceitSingle", model);
        }
        public ActionResult AddRetLocalPurch()
        {
            var model = new RetLocalPurch();
            return View("_RetLocalPurchSingle", model);
        }

        public ActionResult AddRetExPurch()
        {
            var model = new RetExPurch();
            return View("_RetExPurchSingle", model);
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
