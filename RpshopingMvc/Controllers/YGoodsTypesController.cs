using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RpshopingMvc.Models;
using RpshopingMvc.App_Start;

namespace RpshopingMvc.Controllers
{
    public class YGoodsTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void Sidebar(string name = "云购商品分类")
        {
            ViewBag.Sidebar = name;

        }

        // GET: YGoodsTypes
        public ActionResult Index(string filter, int page = 1)
        {
            Sidebar();
            var m = from a in db.YGoodsType
                    select new YGoodsTypeView
                    {
                        ID = a.ID,
                        Name = a.Name,
                        Icon = a.Icon,
                        Sort = a.Sort
                    };

            if (!string.IsNullOrWhiteSpace(filter))
            {
                m = m.Where(s => s.Name.Contains(filter));
            }
            var paged = m.OrderBy(s => s.ID).ToPagedList(page);
            return View(paged);
        }

        // GET: YGoodsTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YGoodsType yGoodsType = db.YGoodsType.Find(id);
            if (yGoodsType == null)
            {
                return HttpNotFound();
            }
            return View(yGoodsType);
        }

        // GET: YGoodsTypes/Create
        public ActionResult Create()
        {
            Sidebar();
            return View();
        }

        // POST: YGoodsTypes/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Sort,Icon")] YGoodsType yGoodsType)
        {
            if (ModelState.IsValid)
            {
                db.YGoodsType.Add(yGoodsType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(yGoodsType);
        }

        // GET: YGoodsTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YGoodsType yGoodsType = db.YGoodsType.Find(id);
            if (yGoodsType == null)
            {
                return HttpNotFound();
            }
            return View(yGoodsType);
        }

        // POST: YGoodsTypes/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Sort,Icon")] YGoodsType yGoodsType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(yGoodsType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(yGoodsType);
        }

        // GET: YGoodsTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YGoodsType yGoodsType = db.YGoodsType.Find(id);
            if (yGoodsType == null)
            {
                return HttpNotFound();
            }
            return View(yGoodsType);
        }

        // POST: YGoodsTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            YGoodsType yGoodsType = db.YGoodsType.Find(id);
            db.YGoodsType.Remove(yGoodsType);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //获取云购产品期数
        [HttpGet]
        [AllowCrossSiteJson]
        public ActionResult GetYGTypeoodsList()
        {
            try
            {
                string sqlstr = string.Empty;
                sqlstr = string.Format(@"SELECT * FROM dbo.YGoodsTypes");
                List<YGoodsType> data = db.Database.SqlQuery<YGoodsType>(sqlstr).ToList();
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
