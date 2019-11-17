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
    public class zyactivitiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void Sidebar(string name = "商品活动")
        {
            ViewBag.Sidebar = name;
        }
        // GET: zyactivities
        public ActionResult Index(string filter, int page = 1)
        {
            Sidebar();
            var m = from e in db.zyactivity
                    select new zyactivityshow
                    {
                        ID = e.ID,
                        Name = e.Name,
                    };

            if (!string.IsNullOrWhiteSpace(filter))
            {
                m = m.Where(s => s.Name.Contains(filter));
            }
            var paged = m.OrderBy(s => s.ID).ToPagedList(page);
            return View(paged);
        }

        // GET: zyactivities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            zyactivity zyactivity = db.zyactivity.Find(id);
            if (zyactivity == null)
            {
                return HttpNotFound();
            }
            return View(zyactivity);
        }

        // GET: zyactivities/Create
        public ActionResult Create()
        {
            Sidebar();
            var model = new zyactivityview();
            return View(model);
        }

        // POST: zyactivities/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(zyactivityview zyactivity)
        {
            if (ModelState.IsValid)
            {
                var model = new zyactivity
                {
                    Name = zyactivity.Name,
                    GradeAsk = zyactivity.GradeAsk
                };
                db.zyactivity.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(zyactivity);
        }

        // GET: zyactivities/Edit/5
        public ActionResult Edit(int? id)
        {
            Sidebar();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            zyactivity model = db.zyactivity.Find(id);
            var models = new zyactivityview
            {
                ID = model.ID,
                Name = model.Name,
                GradeAsk = model.GradeAsk
            };
            if (models == null)
            {
                return HttpNotFound();
            }
            return View(models);
        }

        // POST: zyactivities/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(zyactivityview zyactivity)
        {
            if (ModelState.IsValid)
            {
                var t = db.zyactivity.FirstOrDefault(s => s.ID == zyactivity.ID);
                t.ID = zyactivity.ID;
                t.Name = zyactivity.Name;
                t.GradeAsk = zyactivity.GradeAsk;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(zyactivity);
        }

        // GET: zyactivities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            zyactivity zyactivity = db.zyactivity.Find(id);
            if (zyactivity == null)
            {
                return HttpNotFound();
            }
            return View(zyactivity);
        }

        // POST: zyactivities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            zyactivity zyactivity = db.zyactivity.Find(id);
            db.zyactivity.Remove(zyactivity);
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
