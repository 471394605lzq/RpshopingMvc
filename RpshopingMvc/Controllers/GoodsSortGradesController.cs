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
    public class GoodsSortGradesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void Sidebar(string name = "商品分类等级")
        {
            ViewBag.Sidebar = name;

        }
        // GET: GoodsSortGrades
        public ActionResult Index()
        {
            Sidebar();
            return View(db.GoodsSortGrade.ToList());
        }

        // GET: GoodsSortGrades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GoodsSortGrade goodsSortGrade = db.GoodsSortGrade.Find(id);
            if (goodsSortGrade == null)
            {
                return HttpNotFound();
            }
            return View(goodsSortGrade);
        }

        // GET: GoodsSortGrades/Create
        public ActionResult Create()
        {
            Sidebar();
            return View();
        }

        // POST: GoodsSortGrades/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Index")] GoodsSortGrade goodsSortGrade)
        {
            if (ModelState.IsValid)
            {
                db.GoodsSortGrade.Add(goodsSortGrade);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(goodsSortGrade);
        }

        // GET: GoodsSortGrades/Edit/5
        public ActionResult Edit(int? id)
        {
            Sidebar();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GoodsSortGrade goodsSortGrade = db.GoodsSortGrade.Find(id);
            if (goodsSortGrade == null)
            {
                return HttpNotFound();
            }
            return View(goodsSortGrade);
        }

        // POST: GoodsSortGrades/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Index")] GoodsSortGrade goodsSortGrade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(goodsSortGrade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(goodsSortGrade);
        }

        // GET: GoodsSortGrades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GoodsSortGrade goodsSortGrade = db.GoodsSortGrade.Find(id);
            if (goodsSortGrade == null)
            {
                return HttpNotFound();
            }
            return View(goodsSortGrade);
        }

        // POST: GoodsSortGrades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GoodsSortGrade goodsSortGrade = db.GoodsSortGrade.Find(id);
            db.GoodsSortGrade.Remove(goodsSortGrade);
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
