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
    public class BrandsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void Sidebar(string name = "品牌")
        {
            ViewBag.Sidebar = name;
        }
        // GET: Brands
        public ActionResult Index(string filter, int page = 1)
        {
            Sidebar();
            var m = from e in db.Brand
                    select new BrandShow
                    {
                        ID = e.ID,
                        Name = e.Name,
                        Image = e.Image,
                        Sort = e.Sort
                    };

            if (!string.IsNullOrWhiteSpace(filter))
            {
                m = m.Where(s => s.Name.Contains(filter));
            }
            var paged = m.OrderBy(s => s.ID).ToPagedList(page);
            return View(paged);
        }

        // GET: Brands/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = db.Brand.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // GET: Brands/Create
        public ActionResult Create()
        {
            Sidebar();
            var model = new BrandView();
            return View(model);
        }

        // POST: Brands/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BrandView brand)
        {
            if (ModelState.IsValid)
            {

                var model = new Brand
                {
                    Name = brand.Name,
                    Image = string.Join(",", brand.Image.Images),
                    Sort=brand.Sort
                };
                db.Brand.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(brand);
        }

        // GET: Brands/Edit/5
        public ActionResult Edit(int? id)
        {
            Sidebar();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand model = db.Brand.Find(id);
            var models = new BrandView
            {
                ID = model.ID,
                Name = model.Name,
                Sort = model.Sort
            };
            models.Image.Images = model.Image?.Split(',') ?? new string[0];
            if (models == null)
            {
                return HttpNotFound();
            }
            return View(models);
        }

        // POST: Brands/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BrandView brand)
        {
            if (ModelState.IsValid)
            {
                var t = db.Brand.FirstOrDefault(s => s.ID == brand.ID);
                t.ID = brand.ID;
                t.Name = brand.Name;
                t.Image = string.Join(",", brand.Image.Images);
                t.Sort = brand.Sort;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(brand);
        }

        // GET: Brands/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = db.Brand.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // POST: Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Brand brand = db.Brand.Find(id);
            db.Brand.Remove(brand);
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
