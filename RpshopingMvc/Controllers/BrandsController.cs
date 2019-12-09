using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
                        Sort = e.Sort,
                        Explain = e.Explain
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
                    Sort = brand.Sort,
                    Explain = brand.Explain
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
                Sort = model.Sort,
                Explain = model.Explain
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
                t.Explain = brand.Explain;
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
        //获取品牌
        [HttpGet]
        [AllowCrossSiteJson]
        public ActionResult GetBrand(int? page = 1, int? pageSize = 5)
        {
            try
            {
                int starpagesize = page.Value * pageSize.Value - pageSize.Value;
                int endpagesize = page.Value * pageSize.Value;
                string sql = string.Format(@"SELECT * FROM (SELECT CAST(ROW_NUMBER() over(order by COUNT(g.ID) DESC) AS INTEGER) AS Ornumber,g.*,(SELECT COUNT(1) FROM dbo.goods WHERE Brand=g.ID) AS pcount
                                        FROM dbo.Brands g WHERE g.Name<>'无' 
                                        GROUP BY g.ID,g.Name,g.Image,g.Sort,g.Explain
                                        ) t WHERE t.Ornumber > {0} AND t.Ornumber<={1}", starpagesize, endpagesize);
                List<Brandlist> data = db.Database.SqlQuery<Brandlist>(sql).ToList();
                if (data.Count > 0)
                {
                    //循环取出每个品牌下的3个商品
                    for (int i = 0; i < data.Count; i++)
                    {
                        string getbrandlistsql = string.Format(@"SELECT top 3 ID,GoodsName,Price,zkprice,ImagePath FROM dbo.goods WHERE Brand={0} ORDER BY ByIndex ASC", data[i].ID);
                        List<BrandProduct> productlist = db.Database.SqlQuery<BrandProduct>(getbrandlistsql).ToList();
                        data[i].BrandProducts = productlist;
                    }
                }
                return Json(Comm.ToJsonResult("Success", "成功", data), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(Comm.ToJsonResult("Error", "获取失败"), JsonRequestBehavior.AllowGet);
            }
        }
        //获取品牌商品
        [HttpGet]
        [AllowCrossSiteJson]
        public ActionResult GetBrandProduct(int brandid, int? page = 1, int? pageSize = 5)
        {
            try
            {
                int starpagesize = page.Value * pageSize.Value - pageSize.Value;
                int endpagesize = page.Value * pageSize.Value;
                string sql = string.Format(@"SELECT * FROM (SELECT CAST(ROW_NUMBER() over(order by COUNT(g.ID) DESC) AS INTEGER) AS Ornumber,g.ID,g.GoodsName,g.Price,g.zkprice,g.ImagePath,g.SalesVolume FROM dbo.goods g WHERE Brand={0} 
                                        GROUP BY g.ID,g.GoodsName,g.Price,g.zkprice,g.ImagePath,g.SalesVolume
                                        ) t WHERE t.Ornumber > {1} AND t.Ornumber<={2}", brandid, starpagesize, endpagesize);
                List<BrandProduct> data = db.Database.SqlQuery<BrandProduct>(sql).ToList();
                return Json(Comm.ToJsonResult("Success", "成功", data), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(Comm.ToJsonResult("Error", "获取失败"), JsonRequestBehavior.AllowGet);
            }
        }

        public class Brandlist
        {
            public int ID { get; set; }
            public string Name { get; set; }//品牌名称
            public string Image { get; set; }//品牌图片
            public string Explain { get; set; }//品牌说明
            public int pcount { get; set; }//品牌下产品数量
           
            public List<BrandProduct> BrandProducts { get; set; }//品牌商品
        }
        public class BrandProduct {
            [Display(Name = "商品编号")]
            public int ID { get; set; }
            [Display(Name = "商品名称")]
            [MaxLength(200)]
            public string GoodsName { get; set; }
            [Display(Name = "商品价格")]
            public decimal Price { get; set; }
            [Display(Name = "折扣价")]
            public decimal zkprice { get; set; }
            [Display(Name = "商品主图")]
            public string ImagePath { get; set; }
            [Display(Name = "销量")]
            public int SalesVolume { get; set; }//销量
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
