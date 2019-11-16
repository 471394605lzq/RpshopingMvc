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
    public class zygoodservicesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void Sidebar(string name = "自营商品服务")
        {
            ViewBag.Sidebar = name;
        }
        // GET: zygoodservices
        public ActionResult Index(string filter, int page = 1)
        {
            Sidebar();
            var m = from e in db.zygoodservice
                    select new zygoodserviceshow
                    {
                        ID = e.ID,
                        Name = e.Name,
                        Explain = e.Explain,
                        Sort = e.Sort
                    };

            if (!string.IsNullOrWhiteSpace(filter))
            {
                m = m.Where(s => s.Name.Contains(filter));
            }
            var paged = m.OrderBy(s => s.ID).ToPagedList(page);
            return View(paged);
        }

        // GET: zygoodservices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            zygoodservice zygoodservice = db.zygoodservice.Find(id);
            if (zygoodservice == null)
            {
                return HttpNotFound();
            }
            return View(zygoodservice);
        }

        // GET: zygoodservices/Create
        public ActionResult Create()
        {
            Sidebar();
            var model = new zygoodserviceshow();
            return View(model);
        }

        // POST: zygoodservices/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(zygoodserviceshow zygoodservice)
        {
            if (ModelState.IsValid)
            {
                var model = new zygoodservice
                {
                    Sort = zygoodservice.Sort,
                    Explain = zygoodservice.Explain,
                    Name = zygoodservice.Name
                };
                db.zygoodservice.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(zygoodservice);
        }

        // GET: zygoodservices/Edit/5
        public ActionResult Edit(int? id)
        {
            Sidebar();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            zygoodservice model = db.zygoodservice.Find(id);
            var models = new zygoodserviceshow
            {
                ID = model.ID,
                Name = model.Name,
                Explain = model.Explain,
                Sort = model.Sort
            };
            if (models == null)
            {
                return HttpNotFound();
            }
            return View(models);
        }

        // POST: zygoodservices/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(zygoodserviceshow zygoodservice)
        {
            if (ModelState.IsValid)
            {
                var t = db.zygoodservice.FirstOrDefault(s => s.ID == zygoodservice.ID);
                t.ID = zygoodservice.ID;
                t.Name = zygoodservice.Name;
                t.Sort = zygoodservice.Sort;
                t.Explain = zygoodservice.Explain;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(zygoodservice);
        }

        // GET: zygoodservices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            zygoodservice zygoodservice = db.zygoodservice.Find(id);
            if (zygoodservice == null)
            {
                return HttpNotFound();
            }
            return View(zygoodservice);
        }

        // POST: zygoodservices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            zygoodservice zygoodservice = db.zygoodservice.Find(id);
            db.zygoodservice.Remove(zygoodservice);
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
