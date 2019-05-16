using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RpshopingMvc.Models;

namespace RpshopingMvc.Controllers
{
    public class WithdrawcashesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Withdrawcashes
        public ActionResult Index()
        {
            return View(db.Withdrawcash.ToList());
        }

        // GET: Withdrawcashes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Withdrawcash withdrawcash = db.Withdrawcash.Find(id);
            if (withdrawcash == null)
            {
                return HttpNotFound();
            }
            return View(withdrawcash);
        }

        // GET: Withdrawcashes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Withdrawcashes/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,out_biz_no,order_id,pay_date,Userid,AliAccount,UserName,txamount,txmonth,signstr")] Withdrawcash withdrawcash)
        {
            if (ModelState.IsValid)
            {
                db.Withdrawcash.Add(withdrawcash);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(withdrawcash);
        }

        // GET: Withdrawcashes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Withdrawcash withdrawcash = db.Withdrawcash.Find(id);
            if (withdrawcash == null)
            {
                return HttpNotFound();
            }
            return View(withdrawcash);
        }

        // POST: Withdrawcashes/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,out_biz_no,order_id,pay_date,Userid,AliAccount,UserName,txamount,txmonth,signstr")] Withdrawcash withdrawcash)
        {
            if (ModelState.IsValid)
            {
                db.Entry(withdrawcash).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(withdrawcash);
        }

        // GET: Withdrawcashes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Withdrawcash withdrawcash = db.Withdrawcash.Find(id);
            if (withdrawcash == null)
            {
                return HttpNotFound();
            }
            return View(withdrawcash);
        }

        // POST: Withdrawcashes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Withdrawcash withdrawcash = db.Withdrawcash.Find(id);
            db.Withdrawcash.Remove(withdrawcash);
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
