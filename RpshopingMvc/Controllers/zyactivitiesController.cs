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

        //获取品牌商品
        [HttpGet]
        [AllowCrossSiteJson]
        public ActionResult GetActivite(int id, int? page = 1, int? pageSize = 20)
        {
            try
            {
                int starpagesize = page.Value * pageSize.Value - pageSize.Value;
                int endpagesize = page.Value * pageSize.Value;
                string sql = string.Format(@"SELECT DATEDIFF( Second, GETDATE(),EffectiveTime) as timestr FROM dbo.zyactivities WHERE ID={0}", id);
                List<time> data = db.Database.SqlQuery<time>(sql).ToList();
                string getprosql = string.Format(@"SELECT * FROM (SELECT CAST(ROW_NUMBER() over(order by COUNT(zg.ID) DESC) AS INTEGER) AS Ornumber,
                                        zg.ID,g.GoodsName,g.zkprice AS Price,zg.acrivityprice AS zkprice,g.ImagePath,zg.activenumber,zg.Postage,zg.surplusnumber FROM dbo.zyactivitygoods zg 
                                        INNER JOIN dbo.zyactivities zc ON zc.ID =zg.activityid
                                        INNER JOIN dbo.goods g ON g.ID=zg.goodsid  WHERE zg.activityid={0} 
                                        GROUP BY zg.ID,g.GoodsName,g.zkprice,zg.acrivityprice,g.ImagePath,zg.activenumber,zg.Postage,zg.surplusnumber
                                        ) t WHERE t.Ornumber > {1} AND t.Ornumber<={2}", id, starpagesize, endpagesize);
                List<ActiviteProduct> activeproductlist = db.Database.SqlQuery<ActiviteProduct>(getprosql).ToList();
                var returndata = new
                {
                    activedata = data,
                    activeprodata = activeproductlist
                };
                return Json(Comm.ToJsonResult("Success", "成功", returndata), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(Comm.ToJsonResult("Error", "获取失败"), JsonRequestBehavior.AllowGet);
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
    public class time {
        public int Timestr { get; set; }
    }

    public class ActiviteProduct
    {
        [Display(Name = "商品活动编号")]
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
        [Display(Name = "活动商品数量")]
        public int activenumber { get; set; }//活动商品数量
        [Display(Name = "邮费")]
        public int Postage { get; set; }
        [Display(Name = "活动剩余商品数量")]
        public int surplusnumber { get; set; }
    }

}
