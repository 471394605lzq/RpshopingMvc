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
    public class goodstypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void Sidebar(string name = "自营商品分类")
        {
            ViewBag.Sidebar = name;
        }

        // GET: goodstypes
        public ActionResult Index(string filter, int page = 1)
        {
            Sidebar();
            var m = from e in db.goodstype
                    select new goodstypeshow
                    {
                        ID = e.ID,
                        ImagePath = e.ImagePath,
                        ParentID = e.ParentID,
                        Name = e.Name,
                        SortIndex = e.SortIndex
                    };

            if (!string.IsNullOrWhiteSpace(filter))
            {
                m = m.Where(s => s.Name.Contains(filter));
            }
            var paged = m.OrderBy(s => s.ID).ToPagedList(page);
            return View(paged);
        }

        // GET: goodstypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            goodstype goodstype = db.goodstype.Find(id);
            if (goodstype == null)
            {
                return HttpNotFound();
            }
            return View(goodstype);
        }

        // GET: goodstypes/Create
        public ActionResult Create()
        {
            ViewBag.ParentID = new SelectList(db.goodstype.Where(s => s.ParentID != s.ID), "ID", "Name");
            Sidebar();
            var model = new goodstypeview();
            return View(model);
        }

        // POST: goodstypes/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(goodstypeview goodstype)
        {
            if (ModelState.IsValid)
            {

                var model = new goodstype
                {
                    Name = goodstype.Name,
                    ImagePath = string.Join(",", goodstype.ImagePath.Images),
                    ParentID = goodstype.ParentID,
                    SortIndex = goodstype.SortIndex
                };
                db.goodstype.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(goodstype);
        }

        // GET: goodstypes/Edit/5
        public ActionResult Edit(int? id)
        {
            Sidebar();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            goodstype model = db.goodstype.Find(id);
            var models = new goodstypeview
            {
                ID = model.ID,
                Name = model.Name,
                ParentID = model.ParentID,
                SortIndex = model.SortIndex,
            };
            models.ImagePath.Images = model.ImagePath?.Split(',') ?? new string[0];
            ViewBag.ParentID = new SelectList(db.goodstype.Where(s => s.ParentID != s.ID), "ID", "Name", models.ParentID);

            if (models == null)
            {
                return HttpNotFound();
            }
            return View(models);
        }

        // POST: goodstypes/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(goodstypeview goodstype)
        {
            if (ModelState.IsValid)
            {
                var t = db.goodstype.FirstOrDefault(s => s.ID == goodstype.ID);
                t.ID = goodstype.ID;
                t.Name = goodstype.Name;
                t.ParentID = goodstype.ParentID;
                t.SortIndex = goodstype.SortIndex;
                t.ImagePath = string.Join(",", goodstype.ImagePath.Images);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(goodstype);
        }

        // GET: goodstypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            goodstype goodstype = db.goodstype.Find(id);
            if (goodstype == null)
            {
                return HttpNotFound();
            }
            return View(goodstype);
        }

        // POST: goodstypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            goodstype goodstype = db.goodstype.Find(id);
            db.goodstype.Remove(goodstype);
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
