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
    public class YGoodsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void Sidebar(string name = "云购商品")
        {
            ViewBag.Sidebar = name;

        }
        // GET: YGoods
        public ActionResult Index(string filter, int page = 1)
        {
            Sidebar();
            var m = from e in db.YGoods
                    from a in db.YGoodsType
                    where e.Type==a.ID
                    select new YGoodsShow
                    {
                        ID = e.ID,
                        GoodsName = e.GoodsName,
                        MainImage = e.MainImage,
                        Price = e.Price,
                        Stock = e.Stock,
                        TypeName = a.Name
                    };

            if (!string.IsNullOrWhiteSpace(filter))
            {
                m = m.Where(s => s.GoodsName.Contains(filter));
            }
            var paged = m.OrderBy(s => s.ID).ToPagedList(page);
            return View(paged);
            //return View(db.YGoods.ToList());
        }

        // GET: YGoods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YGoods yGoods = db.YGoods.Find(id);
            if (yGoods == null)
            {
                return HttpNotFound();
            }
            return View(yGoods);
        }

        // GET: YGoods/Create
        public ActionResult Create()
        {
            ViewBag.Type = new SelectList(db.YGoodsType, "ID", "Name");
            var model = new YGoodsView();
            return View(model);
        }

        // POST: YGoods/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(YGoodsView yGoods)
        {
            if (ModelState.IsValid)
            {
                var model = new YGoods
                {
                    GoodsName = yGoods.GoodsName,
                    Type = yGoods.Type,
                    Stock = yGoods.Stock,
                    SamllImage = string.Join(",", yGoods.SamllImage.Images),
                    Info = yGoods.Info,
                    MainImage = string.Join(",", yGoods.MainImage.Images),
                    Price = yGoods.Price
                };
                db.YGoods.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(yGoods);
        }

        // GET: YGoods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YGoods model = db.YGoods.Find(id);
            var models = new YGoodsView
            {
                ID = model.ID,
                Info = model.Info,
                Type = model.Type,
                GoodsName = model.GoodsName,
                //MainImage = model.MainImage,
                Price = model.Price,
                //SamllImage = model.SamllImage,
                Stock = model.Stock
            };
            models.MainImage.Images = model.MainImage?.Split(',') ?? new string[0];
            models.SamllImage.Images = model.SamllImage?.Split(',') ?? new string[0];
            ViewBag.Type = new SelectList(db.YGoodsType.OrderBy(s => s.ID), "ID", "Name", models.Type);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(models);
        }

        // POST: YGoods/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(YGoodsView goods)
        {
            if (ModelState.IsValid)
            {
                var t = db.YGoods.FirstOrDefault(s => s.ID == goods.ID);
                t.ID = goods.ID;
                t.GoodsName = goods.GoodsName;
                t.MainImage = string.Join(",", goods.MainImage.Images);
                t.Price = goods.Price;
                t.SamllImage = string.Join(",", goods.SamllImage.Images);
                t.Stock = goods.Stock;
                t.Type = goods.Type;
                t.Info = goods.Info;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(goods);
        }

        // GET: YGoods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YGoods yGoods = db.YGoods.Find(id);
            if (yGoods == null)
            {
                return HttpNotFound();
            }
            return View(yGoods);
        }

        // POST: YGoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            YGoods yGoods = db.YGoods.Find(id);
            db.YGoods.Remove(yGoods);
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
