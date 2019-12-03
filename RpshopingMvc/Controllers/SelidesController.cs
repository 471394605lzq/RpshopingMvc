using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RpshopingMvc.App_Start;
using RpshopingMvc.Models;

namespace RpshopingMvc.Controllers
{
    public class SelidesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void Sidebar(string name = "轮播图")
        {
            ViewBag.Sidebar = name;
        }

        // GET: Selides
        public ActionResult Index(string filter, int page = 1)
        {
            Sidebar();
            var m = from e in db.Selide
                    select new SelideShow
                    {
                        ID = e.ID,
                        ImagePath = e.ImagePath,
                        Index = e.Index,
                        SelideType = e.SelideType,
                        Title = e.Title,
                        GoodsID = e.GoodsID,
                        GoodsName = e.GoodsName
                    };

            if (!string.IsNullOrWhiteSpace(filter))
            {
                m = m.Where(s => s.Title.Contains(filter));
            }
            var paged = m.OrderBy(s => s.ID).ToPagedList(page);
            return View(paged);
        }

        // GET: Selides/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Selide selide = db.Selide.Find(id);
            if (selide == null)
            {
                return HttpNotFound();
            }
            return View(selide);
        }

        // GET: Selides/Create
        public ActionResult Create()
        {
            Sidebar();
            var model = new SelideView();
            return View(model);
        }

        // POST: Selides/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SelideView selide)
        {

            if (ModelState.IsValid)
            {
                var model = new Selide
                {
                    Title = selide.Title,
                    ImagePath = string.Join(",", selide.ImagePath.Images),
                    SelideType = selide.SelideType,
                    Index = selide.Index,
                    GoodsID = selide.GoodsID,
                    GoodsName = selide.GoodsName
                };
                db.Selide.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(selide);
        }

        // GET: Selides/Edit/5
        public ActionResult Edit(int? id)
        {
            Sidebar();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Selide model = db.Selide.Find(id);
            var models = new SelideView
            {
                ID = model.ID,
                Index = model.Index,
                SelideType = model.SelideType,
                Title = model.Title,
                GoodsID = model.GoodsID,
                GoodsName = model.GoodsName
            };
            models.ImagePath.Images = model.ImagePath?.Split(',') ?? new string[0];
            return View(models);
        }

        // POST: Selides/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SelideView selide)
        {
            Sidebar();
            if (ModelState.IsValid)
            {
                var t = db.Selide.FirstOrDefault(s => s.ID == selide.ID);
                t.ID = selide.ID;
                t.Title = selide.Title;
                t.Index = selide.Index;
                t.SelideType = selide.SelideType;
                t.GoodsID = selide.GoodsID;
                t.GoodsName = selide.GoodsName;
                t.ImagePath = string.Join(",", selide.ImagePath.Images);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(selide);
        }

        // GET: Selides/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sidebar();
            Selide selide = db.Selide.Find(id);
            if (selide == null)
            {
                return HttpNotFound();
            }
            return View(selide);
        }

        // POST: Selides/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sidebar();
            Selide selide = db.Selide.Find(id);
            db.Selide.Remove(selide);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        //获取轮播图
        [HttpGet]
        [AllowCrossSiteJson]
        public ActionResult GetSelideList()
        {
            try
            {
                string sql = string.Format(@"SELECT * FROM Selides");
                List<SelideShow> data = db.Database.SqlQuery<SelideShow>(sql).ToList();
                return Json(Comm.ToJsonResult("Success", "成功", data), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
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
